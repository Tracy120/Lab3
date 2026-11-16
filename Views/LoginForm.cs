using Lab3.Controllers;
using System.Drawing;

namespace Lab3.Views;

public partial class LoginForm : Form
{
    private readonly AppController? app;

    public LoginForm()
    {
        InitializeComponent();
    }

    public LoginForm(AppController controller)
    {
        app = controller;
        InitializeComponent();
    }

    private void Login(object? sender, EventArgs e)
    {
        try
        {
            if (app is null) return;
            if (!app.Login(email.Text, password.Text)) { MessageBox.Show("Invalid email or password.", "Sign in", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            Hide(); using Form form = app.IsAdmin ? new AdminForm(app) : new TouristForm(app); form.ShowDialog(); app.Logout(); Show(); password.Clear();
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }

    private void Register(object? sender, EventArgs e)
    {
        if (app is null) return;
        using var form = new RegisterForm(app); form.ShowDialog();
    }

}

internal sealed class RegisterForm : Form
{
    private readonly AppController app; private readonly TextBox name = new(), email = new(), phone = new(), password = new();
    public RegisterForm(AppController controller)
    {
        app = controller; Text = "Tourist registration"; StartPosition = FormStartPosition.CenterParent; ClientSize = new Size(420, 390); FormBorderStyle = FormBorderStyle.FixedDialog; MaximizeBox = false;
        Add("Full name", name, 25); Add("Email", email, 90); Add("Phone", phone, 155); Add("Password", password, 220); password.UseSystemPasswordChar = true;
        var save = new Button { Text = "Create account", Location = new Point(25, 300), Width = 350, Height = 40 }; save.Click += Save; Controls.Add(save);
    }
    private void Add(string label, TextBox box, int y) { Controls.Add(new Label { Text = label, Location = new Point(25, y), AutoSize = true }); box.Location = new Point(25, y + 22); box.Width = 350; Controls.Add(box); }
    private void Save(object? sender, EventArgs e) { try { app.Register(name.Text, email.Text, phone.Text, password.Text); MessageBox.Show("Registration successful. You can now sign in."); Close(); } catch (Exception ex) { MessageBox.Show(ex.Message, "Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning); } }
}
