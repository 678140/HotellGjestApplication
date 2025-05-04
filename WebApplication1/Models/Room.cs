namespace WebApplication1.Models;

public class Room
{
    public int RoomId { get; set; }
    public int NumberOfBeds { get; set; }
    public double Size { get; set; }
    public string Quality { get; set; }
    public bool IsAvailable { get; set; }
}
