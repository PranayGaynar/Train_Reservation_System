using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TrainTicketReservationSystem.Models
{
    public class Train
    {
        public int TrainId { get; set; }
        public string TrainName { get; set; }
        public string TrainNumber { get; set; }
        public TrainType TrainType { get; set; }

        public ICollection<TrainRoute> Routes { get; set; }  // A train can have multiple routes
    }

    public enum TrainType
    {
        Express,
        Superfast,
        Mail,
        Local
    }
}
