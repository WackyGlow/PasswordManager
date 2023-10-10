namespace PMan.Entity;

public class KeyEntity
{
    public int Id { get; set; }
    public Byte[] HashKey { get; set; }
    public Byte[] InitVect { get; set; }
}