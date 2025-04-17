
using travlr.Backend.Models;
using travlr.Backend.Service;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using Microsoft.EntityFrameworkCore;

public class TripDataService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApplicationDbContext _context;
    private const string ApiUrl = "http://localhost:3000/api/trips";

    public TripDataService(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public async Task<List<Trip>> GetTrips()
    {
        return await _httpClient.GetFromJsonAsync<List<Trip>>(ApiUrl);
    }

    public async Task<List<Trip>> GetTripsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Trip>>("api/trips");
    }
    public async Task<Trip> GetTrip(string tripCode)
    {
        return await _context.Trips.FirstOrDefaultAsync(t => t.Code == tripCode);
    }

    public async Task<Trip> AddTrip(Trip trip)
    {
        var response = await _httpClient.PostAsJsonAsync(ApiUrl, trip);
        return await response.Content.ReadFromJsonAsync<Trip>();
    }

    public async Task<Trip> UpdateTrip(Trip trip)
    {
        var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{trip.Code}", trip);
        return await response.Content.ReadFromJsonAsync<Trip>();
    }

    public async Task<AuthResponse> Login(User user)
    {
        return await MakeAuthApiCall("login", user);
    }

    public async Task<AuthResponse> Register(User user)
    {
        return await MakeAuthApiCall("register", user);
    }

    private async Task<AuthResponse> MakeAuthApiCall(string urlPath, User user)
    {
        var response = await _httpClient.PostAsJsonAsync($"http://localhost:3000/api/{urlPath}", user);
        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

        if (authResponse != null && !string.IsNullOrEmpty(authResponse.Token))
        {
            await _localStorage.SetItemAsync("travlr-token", authResponse.Token);
        }

        return authResponse;
    }
}
