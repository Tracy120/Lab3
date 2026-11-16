using Npgsql;

namespace Lab3.Data;

public static class Database
{
    public static void Initialize()
    {
        string adminConnection = "Host=localhost;Port=5433;Database=postgres;Username=postgres;Password=Tracy@120";
        using (var connection = new NpgsqlConnection(adminConnection))
        {
            connection.Open();
            using var command = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = 'tourism_booking'", connection);
            if (command.ExecuteScalar() is null)
            {
                using var create = new NpgsqlCommand("CREATE DATABASE tourism_booking", connection);
                create.ExecuteNonQuery();
            }
        }

        using var db = new NpgsqlConnection(DatabaseSettings.ConnectionString);
        db.Open();
        using var schema = new NpgsqlCommand(@"
CREATE TABLE IF NOT EXISTS users (
 user_id SERIAL PRIMARY KEY, full_name VARCHAR(120) NOT NULL, email VARCHAR(180) UNIQUE NOT NULL,
 phone VARCHAR(40) NOT NULL, password_hash TEXT NOT NULL, role VARCHAR(20) NOT NULL DEFAULT 'Customer', is_active BOOLEAN NOT NULL DEFAULT TRUE);
CREATE TABLE IF NOT EXISTS destinations (
 destination_id SERIAL PRIMARY KEY, name VARCHAR(120) NOT NULL, country VARCHAR(80) NOT NULL, description TEXT NOT NULL DEFAULT '');
CREATE TABLE IF NOT EXISTS tours (
 tour_id SERIAL PRIMARY KEY, name VARCHAR(160) NOT NULL, destination_id INT NOT NULL REFERENCES destinations(destination_id), description TEXT NOT NULL DEFAULT '',
 start_date DATE NOT NULL, end_date DATE NOT NULL, price_per_person NUMERIC(12,2) NOT NULL, capacity INT NOT NULL, available_seats INT NOT NULL, status VARCHAR(20) NOT NULL DEFAULT 'Open');
CREATE TABLE IF NOT EXISTS bookings (
 booking_id SERIAL PRIMARY KEY, reference VARCHAR(30) UNIQUE NOT NULL, user_id INT NOT NULL REFERENCES users(user_id), tour_id INT NOT NULL REFERENCES tours(tour_id), travellers INT NOT NULL,
 total_cost NUMERIC(12,2) NOT NULL, status VARCHAR(20) NOT NULL DEFAULT 'Pending', booking_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP);
CREATE TABLE IF NOT EXISTS payments (
 payment_id SERIAL PRIMARY KEY, booking_id INT NOT NULL REFERENCES bookings(booking_id), amount NUMERIC(12,2) NOT NULL, method VARCHAR(30) NOT NULL, payment_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP);
 INSERT INTO users (full_name,email,phone,password_hash,role,is_active) VALUES ('System Administrator','admin@tourism.local','0000000000',@hash,'Admin',TRUE)
 ON CONFLICT (email) DO UPDATE SET full_name=EXCLUDED.full_name, phone=EXCLUDED.phone, password_hash=EXCLUDED.password_hash, role='Admin', is_active=TRUE;
 INSERT INTO destinations (name,country,description)
 SELECT name,country,description FROM (VALUES
   ('Cape Coast','Ghana','Historic coastal destination'),
   ('Cape Town','South Africa','Mountain, coast and city attractions'),
   ('Karongi','Rwanda','Lake Kivu lakeside destination')
 ) AS seed(name,country,description)
 WHERE NOT EXISTS (SELECT 1 FROM destinations d WHERE lower(d.name)=lower(seed.name) AND lower(d.country)=lower(seed.country));", db);
        schema.Parameters.AddWithValue("hash", PasswordHasher.Hash("Admin@123"));
        schema.ExecuteNonQuery();
    }
}
