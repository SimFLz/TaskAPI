namespace TaskAPI.Entities;

public class UserEntitie
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
