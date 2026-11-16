using Lab3.Controllers;
using Lab3.Models;

namespace Lab3.Views;

public sealed partial class TouristForm : Form
{
    private readonly AppController? app;

    public TouristForm()
    {
        InitializeComponent();
    }

    public TouristForm(AppController controller)
    {
        app = controller;
        InitializeComponent();
        LoadTours();
        LoadBookings();
    }

    private void LoadTours()
    {
        if (app is null) return;
        try
        {
            tours.DataSource = null;
            tours.DataSource = app.Repository.GetTours(search.Text);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Tours", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadBookings()
    {
        if (app is null || app.CurrentUser is null) return;
        try
        {
            bookings.DataSource = null;
            bookings.DataSource = app.Repository.GetBookings(app.CurrentUser.UserId);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Bookings", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void Book(object? sender, EventArgs e)
    {
        try
        {
            if (app?.CurrentUser is null) throw new InvalidOperationException("You must be signed in.");
            if (tours.SelectedItem is not Tour tour) throw new InvalidOperationException("Select a tour first.");

            var booking = app.Repository.CreateBooking(app.CurrentUser, tour, (int)travellers.Value);
            MessageBox.Show($"Booking created: {booking.Reference}\nTotal: {booking.TotalCost:C}\nStatus: Pending\n\nPayment is recorded by the administrator after it is received.", "Booking created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadTours();
            LoadBookings();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Booking", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void Cancel(object? sender, EventArgs e)
    {
        try
        {
            if (app?.CurrentUser is null) throw new InvalidOperationException("You must be signed in.");
            if (bookings.SelectedItem is not Booking booking) throw new InvalidOperationException("Select a booking first.");

            app.Repository.CancelBooking(booking.BookingId, app.CurrentUser.UserId, false);
            MessageBox.Show("Booking cancelled and seats released.", "Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadTours();
            LoadBookings();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void SearchClick(object? sender, EventArgs e) => LoadTours();
    private void RefreshClick(object? sender, EventArgs e) { LoadTours(); LoadBookings(); }
    private void CloseClick(object? sender, EventArgs e) => Close();
}
