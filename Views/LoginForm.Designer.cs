namespace Lab3.Views;

public partial class LoginForm
{
    private System.ComponentModel.IContainer? components;
    private Label titleLabel = null!;
    private Label emailLabel = null!;
    private Label passwordLabel = null!;
    private Label demoLabel = null!;
    private TextBox email = null!;
    private TextBox password = null!;
    private Button loginButton = null!;
    private Button registerButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        titleLabel = new Label();
        emailLabel = new Label();
        passwordLabel = new Label();
        demoLabel = new Label();
        email = new TextBox();
        password = new TextBox();
        loginButton = new Button();
        registerButton = new Button();
        SuspendLayout();
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(31, 67, 110);
        titleLabel.Location = new Point(85, 30);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(294, 37);
        titleLabel.Text = "Tourism Booking System";
        emailLabel.AutoSize = true;
        emailLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        emailLabel.Location = new Point(55, 100);
        emailLabel.Name = "emailLabel";
        emailLabel.Text = "Email";
        email.Location = new Point(55, 125);
        email.Name = "email";
        email.Size = new Size(350, 23);
        passwordLabel.AutoSize = true;
        passwordLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        passwordLabel.Location = new Point(55, 170);
        passwordLabel.Name = "passwordLabel";
        passwordLabel.Text = "Password";
        password.Location = new Point(55, 195);
        password.Name = "password";
        password.Size = new Size(350, 23);
        password.UseSystemPasswordChar = true;
        loginButton.BackColor = Color.FromArgb(31, 67, 110);
        loginButton.FlatStyle = FlatStyle.Flat;
        loginButton.ForeColor = Color.White;
        loginButton.Location = new Point(55, 250);
        loginButton.Name = "loginButton";
        loginButton.Size = new Size(165, 38);
        loginButton.Text = "Sign in";
        loginButton.UseVisualStyleBackColor = false;
        loginButton.Click += Login;
        registerButton.BackColor = Color.FromArgb(31, 67, 110);
        registerButton.FlatStyle = FlatStyle.Flat;
        registerButton.ForeColor = Color.White;
        registerButton.Location = new Point(240, 250);
        registerButton.Name = "registerButton";
        registerButton.Size = new Size(165, 38);
        registerButton.Text = "Register as tourist";
        registerButton.UseVisualStyleBackColor = false;
        registerButton.Click += Register;
        demoLabel.AutoSize = true;
        demoLabel.ForeColor = Color.DimGray;
        demoLabel.Location = new Point(75, 315);
        demoLabel.Name = "demoLabel";
        demoLabel.Text = "Admin demo: admin@tourism.local / Admin@123";
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 246, 250);
        ClientSize = new Size(460, 360);
        Controls.Add(titleLabel);
        Controls.Add(emailLabel);
        Controls.Add(email);
        Controls.Add(passwordLabel);
        Controls.Add(password);
        Controls.Add(loginButton);
        Controls.Add(registerButton);
        Controls.Add(demoLabel);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "LoginForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Tourism Booking System - Login";
        ResumeLayout(false);
        PerformLayout();
    }
}
