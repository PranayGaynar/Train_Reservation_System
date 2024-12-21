namespace TrainTicketReservationSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; } 
    }
    public enum UserRole
    {
        Passenger,
        Admin,
        Operator
    }
}
