using Lab3.Controllers;
using Lab3.Models;

namespace Lab3.Views;

public sealed partial class AdminForm : Form
{
    private readonly AppController? app;

    public AdminForm()
    {
        InitializeComponent();
    }

    private void ActivateUser(object? sender, EventArgs e) => SetActive(true);
    private void DeactivateUser(object? sender, EventArgs e) => SetActive(false);

    public AdminForm(AppController controller)
    {
        app = controller;
        InitializeComponent();
        LoadData();
    }

    private void LoadData()
    {
        if (app is null) return;
        try
        {
            destinations.DataSource = app.Repository.GetDestinations();
            tours.DataSource = app.Repository.GetAllTours();
            bookings.DataSource = app.Repository.GetBookings();
            users.DataSource = app.Repository.GetUsers();
            LoadReport();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void AddDestination(object? sender, EventArgs e)
    {
        try { app?.Repository.AddDestination(destinationName.Text.Trim(), country.Text.Trim(), ""); LoadData(); }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Destination", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void AddTour(object? sender, EventArgs e)
    {
        try
        {
            if (app is null || destinations.SelectedItem is not Destination destination) throw new InvalidOperationException("Select a destination.");
            app.Repository.AddTour(tourName.Text.Trim(), destination.DestinationId, description.Text.Trim(), start.Value, end.Value, price.Value, (int)capacity.Value);
            LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Tour", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void DeleteTour(object? sender, EventArgs e)
    {
        try
        {
            if (app is null || tours.SelectedItem is not Tour tour) throw new InvalidOperationException("Select a tour.");
            app.Repository.DeleteTour(tour.TourId); LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Tour", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void AddPayment(object? sender, EventArgs e)
    {
        try
        {
            if (app is null || bookings.SelectedItem is not Booking booking) throw new InvalidOperationException("Select a booking.");
            app.Repository.AddPayment(booking.BookingId, payment.Value, (PaymentMethod)methods.SelectedItem!);
            MessageBox.Show("Payment recorded. The booking is confirmed when fully paid.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void CancelBooking(object? sender, EventArgs e)
    {
        try
        {
            if (app is null || bookings.SelectedItem is not Booking booking || app.CurrentUser is null) throw new InvalidOperationException("Select a booking.");
            app.Repository.CancelBooking(booking.BookingId, app.CurrentUser.UserId, true); LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Booking", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void SetActive(bool active)
    {
        try
        {
            if (app is null || users.SelectedItem is not User user) throw new InvalidOperationException("Select a user.");
            app.Repository.SetUserActive(user.UserId, active); LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "User", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void ToggleRole(object? sender, EventArgs e)
    {
        try
        {
            if (app is null || users.SelectedItem is not User user) throw new InvalidOperationException("Select a user.");
            app.Repository.SetUserRole(user.UserId, user.Role == UserRole.Admin ? UserRole.Customer : UserRole.Admin); LoadData();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "User", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
    }

    private void LoadReport()
    {
        if (app is null) return;
        report.DataSource = app.Repository.GetReport().Select(row => $"{row.TourName} | Booked: {row.BookedSeats}/{row.Capacity} | Revenue: {row.Revenue:C}").ToList();
    }

    private void RefreshReport(object? sender, EventArgs e) => LoadReport();

    private void Export(object? sender, EventArgs e)
    {
        if (app is null) return;
        using var dialog = new SaveFileDialog { Filter = "CSV files|*.csv", FileName = "tour-report.csv" };
        if (dialog.ShowDialog() != DialogResult.OK) return;
        var rows = app.Repository.GetReport();
        File.WriteAllLines(dialog.FileName, new[] { "Tour,BookedSeats,Capacity,Revenue" }.Concat(rows.Select(row => $"{row.TourName},{row.BookedSeats},{row.Capacity},{row.Revenue}")));
        MessageBox.Show("Report exported.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void users_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
