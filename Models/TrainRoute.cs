namespace TrainTicketReservationSystem.Models
{
    public class TrainRoute
    {
        public int TrainRouteId { get; set; }
        public int TrainId { get; set; }
        public Train Train { get; set; }
        public string SourceStation { get; set; }
        public string DestinationStation { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int AvailableSeats { get; set; }
        public int TotalSeats { get; set; }
    }
}
