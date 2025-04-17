using travlr.Backend.Models;

namespace travlr.Backend.Services
{
    public interface ITripService
    {
        Task<Trip?> GetTripByCode(string tripCode);
        Task<List<Trip>> GetAllTrips();
        Task AddTrip(Trip trip);
        Task UpdateTrip(Trip trip);
        Task DeleteTrip(int tripId);
    }
}
