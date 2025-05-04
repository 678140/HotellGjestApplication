﻿namespace WebApplication1.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string CustomerName { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}
