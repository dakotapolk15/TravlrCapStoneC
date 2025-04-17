
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace travlr.Backend.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Length { get; set; }
        [Required]
        public string Destination { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Resort { get; set; }

        [Required]
        public decimal PerPerson { get; set; }

        [Required]
        public string Image { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}

