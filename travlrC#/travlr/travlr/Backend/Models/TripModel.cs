using Microsoft.Identity.Client;

namespace travlr.Backend.Models
{
    public class TripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        
        public string Resort { get; set; }

        public string Description { get; set; }
        public string Image { get; set; }
        public string Length { get; set; }

        public static TripModel FromTrip(Trip trip)
        {
            return new TripModel
            {
                Id = trip.Id,
                Name = trip.Name,
                Destination = trip.Destination,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Price = trip.Price
            };
        }
    }
}
