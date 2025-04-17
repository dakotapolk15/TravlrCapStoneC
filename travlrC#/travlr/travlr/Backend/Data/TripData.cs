
using System;
using System.Collections.Generic;

public static class TripData
{
    public static List<TripModel> Trips = new List<TripModel>
    {
        new TripModel
        {
            Code = "GALR210214",
            Name = "Gale Reef",
            Length = "4 nights / 5 days",
            Start = DateTime.Parse("2021-02-14T08:00:00Z"),
            Resort = "Emerald Bay, 3 stars",
            PerPerson = 799.00m.ToString(),
            Image = "reef1.jpg",
            Description = "<p>Gale Reef Sed et augue lorem. In sit amet placera</p>"
        },
        new TripModel
        {
            Code = "DAWR210315",
            Name = "Dawson's Reef",
            Length = "4 nights / 5 days",
            Start = DateTime.Parse("2021-03-15T08:00:00Z"),
            Resort = "Blue Lagoon, 4 stars",
            PerPerson = 1199.00m.ToString(),
            Image = "reef2.jpg",
            Description = "<p>Dawson's reef Integer magna leo, posuere et digni</p>"
        },
        new TripModel
        {
            Code = "CLAR210621",
            Name = "Claire's Reef",
            Length = "4 nights / 5 days",
            Start = DateTime.Parse("2021-06-21T08:00:00Z"),
            Resort = "Coral Sands, 5 stars",
            PerPerson = 1999.00m.ToString(),
            Image = "reef3.jpg",
            Description = "<p>Claire's Reef Donec sed felis risus. Nulla facili</p>"
        }
    };
}
