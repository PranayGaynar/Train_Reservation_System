using System.ComponentModel.DataAnnotations;


namespace TrainTicketReservationSystem.Models;

public class TrainRoute
{
    public int TrainRouteId { get; set; }
    public int TrainId { get; set; }
    public Train Train { get; set; }

    [Required]
    public string SourceStation { get; set; }

    [Required]
    public string DestinationStation { get; set; }

    [Required]
    public DateTime DepartureTime { get; set; }

    [Required]
    public DateTime ArrivalTime { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Available seats must be greater than 0.")]
    public int AvailableSeats { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Total seats must be greater than 0.")]
    public int TotalSeats { get; set; }
}
