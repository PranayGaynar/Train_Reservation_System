namespace TrainTicketReservationSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TrainRouteId { get; set; }
        public TrainRoute TrainRoute { get; set; }
        public DateTime JourneyDate { get; set; }
        public string? SeatClass { get; set; } 
        public string PNR { get; set; }  
        public decimal Amount { get; set; } 
        public bool IsCancelled { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
