
using System;
using System.ComponentModel.DataAnnotations;

public class TripModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Length { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public string Resort { get; set; }

    [Required]
    public string PerPerson { get; set; }

    [Required]
    public string Image { get; set; }

    [Required]
    public string Description { get; set; }
}
