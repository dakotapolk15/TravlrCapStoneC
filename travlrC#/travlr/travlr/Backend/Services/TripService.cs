
using System.Net.Http.Json;
using travlr.Backend.Models;

public class TripService
{
    private readonly HttpClient _httpClient;

    public TripService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Trip>> GetTripsAsync()
    {
        try
        {
            var trips = await _httpClient.GetFromJsonAsync<List<Trip>>("http://localhost:7094");
            return trips ?? new List<Trip>();
        }
        catch (Exception)
        {
            return new List<Trip>(); // Return empty list on error
        }
    }
}
