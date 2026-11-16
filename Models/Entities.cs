namespace Lab3.Models;

public enum UserRole { Customer, Admin }
public enum TourStatus { Open, Full, Cancelled, Completed, Inactive }
public enum BookingStatus { Pending, Confirmed, Cancelled }
public enum PaymentMethod { Cash, MobileMoney, Card }

public sealed class User
{
    public int UserId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
    public override string ToString() =>
        $"{FullName} | {Email} | {Role} | {(IsActive ? "Active" : "Inactive")}";
}

public sealed class Destination
{
    public int DestinationId { get; set; }
    public string Name { get; set; } = "";
    public string Country { get; set; } = "";
    public string Description { get; set; } = "";
    public override string ToString() => $"{Name}, {Country}";
}

public sealed class Tour
{
    public int TourId { get; set; }
    public string Name { get; set; } = "";
    public string Destination { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerPerson { get; set; }
    public int Capacity { get; set; }
    public int AvailableSeats { get; set; }
    public TourStatus Status { get; set; }
    public override string ToString() => $"{Name} | {Destination} | {StartDate:d} | {PricePerPerson:C} | {AvailableSeats} seats";
}

public sealed class Booking
{
    public int BookingId { get; set; }
    public string Reference { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string TourName { get; set; } = "";
    public int TourId { get; set; }
    public int UserId { get; set; }
    public int Travellers { get; set; }
    public decimal TotalCost { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime BookingDate { get; set; }
    public override string ToString() => $"{Reference} | {CustomerName} | {TourName} | {Status} | {TotalCost:C}";
}

public sealed class Payment
{
    public int PaymentId { get; set; }
    public int BookingId { get; set; }
    public string Reference { get; set; } = "";
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public DateTime PaymentDate { get; set; }
}

public sealed class ReportRow
{
    public string TourName { get; set; } = "";
    public int BookedSeats { get; set; }
    public int Capacity { get; set; }
    public decimal Revenue { get; set; }
}
