using Lab3.Models;
using Npgsql;

namespace Lab3.Data;

public sealed class Repository
{
    private NpgsqlConnection Open() { var c = new NpgsqlConnection(DatabaseSettings.ConnectionString); c.Open(); return c; }

    public User? Login(string email, string password)
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("SELECT * FROM users WHERE lower(email)=lower(@email) AND is_active", c);
        cmd.Parameters.AddWithValue("email", email.Trim()); using var r = cmd.ExecuteReader();
        if (!r.Read() || !PasswordHasher.Verify(password, r.GetString(r.GetOrdinal("password_hash")))) return null;
        return UserFrom(r);
    }

    public void Register(string name, string email, string phone, string password)
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("INSERT INTO users(full_name,email,phone,password_hash) VALUES(@name,@email,@phone,@hash)", c);
        cmd.Parameters.AddWithValue("name", name); cmd.Parameters.AddWithValue("email", email); cmd.Parameters.AddWithValue("phone", phone); cmd.Parameters.AddWithValue("hash", PasswordHasher.Hash(password)); cmd.ExecuteNonQuery();
    }

    public List<Tour> GetTours(string search = "")
    {
        using var c = Open(); using var cmd = new NpgsqlCommand(@"SELECT t.*, d.name AS destination FROM tours t JOIN destinations d ON d.destination_id=t.destination_id
WHERE t.status='Open' AND t.available_seats > 0 AND (@search='' OR lower(t.name) LIKE lower(@like) OR lower(d.name) LIKE lower(@like)) ORDER BY t.start_date", c);
        cmd.Parameters.AddWithValue("search", search.Trim()); cmd.Parameters.AddWithValue("like", $"%{search.Trim()}%"); using var r = cmd.ExecuteReader(); var list = new List<Tour>(); while (r.Read()) list.Add(TourFrom(r)); return list;
    }

    public List<Tour> GetAllTours()
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("SELECT t.*, d.name AS destination FROM tours t JOIN destinations d ON d.destination_id=t.destination_id ORDER BY t.start_date", c); using var r = cmd.ExecuteReader(); var list = new List<Tour>(); while (r.Read()) list.Add(TourFrom(r)); return list;
    }

    public List<Destination> GetDestinations()
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("SELECT * FROM destinations ORDER BY name", c); using var r = cmd.ExecuteReader(); var list = new List<Destination>(); while (r.Read()) list.Add(new Destination { DestinationId = r.GetInt32(0), Name = r.GetString(1), Country = r.GetString(2), Description = r.GetString(3) }); return list;
    }

    public void AddDestination(string name, string country, string description)
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("INSERT INTO destinations(name,country,description) VALUES(@name,@country,@description)", c); cmd.Parameters.AddWithValue("name", name); cmd.Parameters.AddWithValue("country", country); cmd.Parameters.AddWithValue("description", description); cmd.ExecuteNonQuery();
    }

    public void AddTour(string name, int destinationId, string description, DateTime start, DateTime end, decimal price, int capacity)
    {
        if (end.Date < start.Date || price <= 0 || capacity <= 0) throw new ArgumentException("Check the dates, price and capacity.");
        using var c = Open(); using var cmd = new NpgsqlCommand("INSERT INTO tours(name,destination_id,description,start_date,end_date,price_per_person,capacity,available_seats) VALUES(@name,@destination,@description,@start,@end,@price,@capacity,@capacity)", c);
        cmd.Parameters.AddWithValue("name", name); cmd.Parameters.AddWithValue("destination", destinationId); cmd.Parameters.AddWithValue("description", description); cmd.Parameters.AddWithValue("start", start.Date); cmd.Parameters.AddWithValue("end", end.Date); cmd.Parameters.AddWithValue("price", price); cmd.Parameters.AddWithValue("capacity", capacity); cmd.ExecuteNonQuery();
    }

    public void DeleteTour(int tourId)
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("DELETE FROM tours WHERE tour_id=@id AND NOT EXISTS (SELECT 1 FROM bookings WHERE tour_id=@id AND status<>'Cancelled')", c); cmd.Parameters.AddWithValue("id", tourId); if (cmd.ExecuteNonQuery() == 0) throw new InvalidOperationException("A tour with active bookings cannot be deleted.");
    }

    public Booking CreateBooking(User user, Tour tour, int travellers)
    {
        if (travellers <= 0) throw new ArgumentException("Travellers must be greater than zero.");
        using var c = Open(); using var tx = c.BeginTransaction();
        using var check = new NpgsqlCommand("SELECT price_per_person,available_seats FROM tours WHERE tour_id=@id FOR UPDATE", c, tx); check.Parameters.AddWithValue("id", tour.TourId); using var r = check.ExecuteReader();
        if (!r.Read()) throw new InvalidOperationException("Tour not found."); decimal price = r.GetDecimal(0); int seats = r.GetInt32(1); r.Close(); if (travellers > seats) throw new InvalidOperationException("There are not enough available seats.");
        string reference = "BK-" + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant(); decimal total = price * travellers;
        using var add = new NpgsqlCommand("INSERT INTO bookings(reference,user_id,tour_id,travellers,total_cost) VALUES(@ref,@user,@tour,@travellers,@total) RETURNING booking_id,booking_date", c, tx); add.Parameters.AddWithValue("ref", reference); add.Parameters.AddWithValue("user", user.UserId); add.Parameters.AddWithValue("tour", tour.TourId); add.Parameters.AddWithValue("travellers", travellers); add.Parameters.AddWithValue("total", total); using var result = add.ExecuteReader(); result.Read(); int id = result.GetInt32(0); DateTime date = result.GetDateTime(1); result.Close();
        using var update = new NpgsqlCommand("UPDATE tours SET available_seats=available_seats-@count, status=CASE WHEN available_seats-@count=0 THEN 'Full' ELSE status END WHERE tour_id=@id", c, tx); update.Parameters.AddWithValue("count", travellers); update.Parameters.AddWithValue("id", tour.TourId); update.ExecuteNonQuery(); tx.Commit();
        return new Booking { BookingId = id, Reference = reference, CustomerName = user.FullName, TourName = tour.Name, TourId = tour.TourId, UserId = user.UserId, Travellers = travellers, TotalCost = total, Status = BookingStatus.Pending, BookingDate = date };
    }

    public List<Booking> GetBookings(int? userId = null)
    {
        using var c = Open(); using var cmd = new NpgsqlCommand(@"SELECT b.*,u.full_name,t.name AS tour_name FROM bookings b JOIN users u ON u.user_id=b.user_id JOIN tours t ON t.tour_id=b.tour_id WHERE (@user=0 OR b.user_id=@user) ORDER BY b.booking_date DESC", c); cmd.Parameters.AddWithValue("user", userId ?? 0); using var r = cmd.ExecuteReader(); var list = new List<Booking>(); while (r.Read()) list.Add(BookingFrom(r)); return list;
    }

    public void CancelBooking(int bookingId, int userId, bool admin)
    {
        using var c = Open(); using var tx = c.BeginTransaction(); using var cmd = new NpgsqlCommand("SELECT tour_id,travellers FROM bookings WHERE booking_id=@id AND (@admin OR user_id=@user) AND status<>'Cancelled' FOR UPDATE", c, tx); cmd.Parameters.AddWithValue("id", bookingId); cmd.Parameters.AddWithValue("user", userId); cmd.Parameters.AddWithValue("admin", admin); using var r = cmd.ExecuteReader(); if (!r.Read()) throw new InvalidOperationException("Booking cannot be cancelled."); int tour = r.GetInt32(0), travellers = r.GetInt32(1); r.Close(); using var cancel = new NpgsqlCommand("UPDATE bookings SET status='Cancelled' WHERE booking_id=@id; UPDATE tours SET available_seats=available_seats+@travellers,status=CASE WHEN status='Full' THEN 'Open' ELSE status END WHERE tour_id=@tour", c, tx); cancel.Parameters.AddWithValue("id", bookingId); cancel.Parameters.AddWithValue("travellers", travellers); cancel.Parameters.AddWithValue("tour", tour); cancel.ExecuteNonQuery(); tx.Commit();
    }

    public void AddPayment(int bookingId, decimal amount, PaymentMethod method)
    {
        using var c = Open(); using var tx = c.BeginTransaction(); using var cmd = new NpgsqlCommand("SELECT total_cost - COALESCE((SELECT SUM(amount) FROM payments WHERE booking_id=@id),0) FROM bookings WHERE booking_id=@id FOR UPDATE", c, tx); cmd.Parameters.AddWithValue("id", bookingId); decimal outstanding = Convert.ToDecimal(cmd.ExecuteScalar() ?? 0); if (amount <= 0 || amount > outstanding) throw new InvalidOperationException($"Payment must be greater than zero and no more than {outstanding:C}."); using var add = new NpgsqlCommand("INSERT INTO payments(booking_id,amount,method) VALUES(@id,@amount,@method)", c, tx); add.Parameters.AddWithValue("id", bookingId); add.Parameters.AddWithValue("amount", amount); add.Parameters.AddWithValue("method", method.ToString()); add.ExecuteNonQuery(); if (amount == outstanding) { using var confirm = new NpgsqlCommand("UPDATE bookings SET status='Confirmed' WHERE booking_id=@id", c, tx); confirm.Parameters.AddWithValue("id", bookingId); confirm.ExecuteNonQuery(); } tx.Commit();
    }

    public List<ReportRow> GetReport()
    {
        using var c = Open(); using var cmd = new NpgsqlCommand(@"
SELECT t.name, t.capacity,
       COALESCE((SELECT SUM(b.travellers) FROM bookings b WHERE b.tour_id=t.tour_id AND b.status<>'Cancelled'), 0),
       COALESCE((SELECT SUM(p.amount) FROM payments p JOIN bookings b2 ON b2.booking_id=p.booking_id WHERE b2.tour_id=t.tour_id AND b2.status<>'Cancelled'), 0)
FROM tours t ORDER BY t.name", c); using var r = cmd.ExecuteReader(); var rows = new List<ReportRow>(); while (r.Read()) rows.Add(new ReportRow { TourName = r.GetString(0), Capacity = r.GetInt32(1), BookedSeats = Convert.ToInt32(r.GetInt64(2)), Revenue = r.GetDecimal(3) }); return rows;
    }

    public List<User> GetUsers()
    {
        using var c = Open(); using var cmd = new NpgsqlCommand("SELECT * FROM users ORDER BY full_name", c); using var r = cmd.ExecuteReader(); var list = new List<User>(); while (r.Read()) list.Add(UserFrom(r)); return list;
    }

    public void SetUserActive(int id, bool active) { using var c = Open(); using var cmd = new NpgsqlCommand("UPDATE users SET is_active=@active WHERE user_id=@id", c); cmd.Parameters.AddWithValue("active", active); cmd.Parameters.AddWithValue("id", id); cmd.ExecuteNonQuery(); }
    public void SetUserRole(int id, UserRole role) { using var c = Open(); using var cmd = new NpgsqlCommand("UPDATE users SET role=@role WHERE user_id=@id", c); cmd.Parameters.AddWithValue("role", role.ToString()); cmd.Parameters.AddWithValue("id", id); cmd.ExecuteNonQuery(); }

    private static User UserFrom(NpgsqlDataReader r) => new() { UserId = r.GetInt32(r.GetOrdinal("user_id")), FullName = r.GetString(r.GetOrdinal("full_name")), Email = r.GetString(r.GetOrdinal("email")), Phone = r.GetString(r.GetOrdinal("phone")), PasswordHash = r.GetString(r.GetOrdinal("password_hash")), Role = Enum.Parse<UserRole>(r.GetString(r.GetOrdinal("role"))), IsActive = r.GetBoolean(r.GetOrdinal("is_active")) };
    private static Tour TourFrom(NpgsqlDataReader r) => new() { TourId = r.GetInt32(r.GetOrdinal("tour_id")), Name = r.GetString(r.GetOrdinal("name")), Destination = r.GetString(r.GetOrdinal("destination")), Description = r.GetString(r.GetOrdinal("description")), StartDate = r.GetDateTime(r.GetOrdinal("start_date")), EndDate = r.GetDateTime(r.GetOrdinal("end_date")), PricePerPerson = r.GetDecimal(r.GetOrdinal("price_per_person")), Capacity = r.GetInt32(r.GetOrdinal("capacity")), AvailableSeats = r.GetInt32(r.GetOrdinal("available_seats")), Status = Enum.Parse<TourStatus>(r.GetString(r.GetOrdinal("status"))) };
    private static Booking BookingFrom(NpgsqlDataReader r) => new() { BookingId = r.GetInt32(r.GetOrdinal("booking_id")), Reference = r.GetString(r.GetOrdinal("reference")), UserId = r.GetInt32(r.GetOrdinal("user_id")), CustomerName = r.GetString(r.GetOrdinal("full_name")), TourId = r.GetInt32(r.GetOrdinal("tour_id")), TourName = r.GetString(r.GetOrdinal("tour_name")), Travellers = r.GetInt32(r.GetOrdinal("travellers")), TotalCost = r.GetDecimal(r.GetOrdinal("total_cost")), Status = Enum.Parse<BookingStatus>(r.GetString(r.GetOrdinal("status"))), BookingDate = r.GetDateTime(r.GetOrdinal("booking_date")) };
}
