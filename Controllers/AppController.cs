using Lab3.Data;
using Lab3.Models;

namespace Lab3.Controllers;

public sealed class AppController
{
    public Repository Repository { get; } = new();
    public User? CurrentUser { get; private set; }

    public void Start() => Database.Initialize();

    public bool Login(string email, string password)
    {
        CurrentUser = Repository.Login(email, password);
        return CurrentUser is not null;
    }

    public void Register(string name, string email, string phone, string password)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password)) throw new ArgumentException("All fields are required.");
        Repository.Register(name, email, phone, password);
    }

    public void Logout() => CurrentUser = null;
    public bool IsAdmin => CurrentUser?.Role == UserRole.Admin;
    public void RequireAdmin() { if (!IsAdmin) throw new UnauthorizedAccessException("Administrator access is required."); }
}
