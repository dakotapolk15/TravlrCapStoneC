
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using travlr.Backend.Data;
using travlr.Backend.Models;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TripsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ GET: api/trips (Retrieve all trips)
    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        var trips = await _context.Trips.ToListAsync();
        return Ok(trips);
    }

    // ✅ GET: api/trips/{tripCode} (Retrieve a trip by code)
    [HttpGet("{tripCode}")]
    public async Task<IActionResult> GetTripByCode(string tripCode)
    {
        var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Code == tripCode);

        if (trip == null)
        {
            return NotFound(new { message = "Trip not found" });
        }

        return Ok(trip);
    }

    // ✅ POST: api/trips (Add a new trip)
    [HttpPost]
    public async Task<IActionResult> AddTrip([FromBody] Trip trip)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTripByCode), new { tripCode = trip.Code }, trip);
    }

    // ✅ PUT: api/trips/{tripCode} (Update an existing trip)
    [HttpPut("{tripCode}")]
    public async Task<IActionResult> UpdateTrip(string tripCode, [FromBody] Trip updatedTrip)
    {
        var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Code == tripCode);

        if (trip == null)
        {
            return NotFound(new { message = "Trip not found" });
        }

        trip.Name = updatedTrip.Name;
        trip.Length = updatedTrip.Length;
        trip.StartDate = updatedTrip.StartDate;
        trip.Resort = updatedTrip.Resort;
        trip.PerPerson = updatedTrip.PerPerson;
        trip.Image = updatedTrip.Image;
        trip.Description = updatedTrip.Description;

        await _context.SaveChangesAsync();
        return Ok(trip);
    }
}


