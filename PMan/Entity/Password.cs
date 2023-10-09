namespace PMan.Entity;

public class Password
{
    public int Id { get; set; }
    public string Website { get; set; }
    public string HashedLogin { get; set; }
    public string HashedPassword { get; set; }
    public string HashedMasterpassword { get; set; }
}