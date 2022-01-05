using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.Models
{
   public class Vehicle
    {
        public readonly string CategoryName;

        [Key]
        public int Id { get; set; }
        [Required]
        public string  CarName{ get; set; }

        [Required]
        public int CarModel{ get; set; }
        [Required]
        public int DriverLisenseAge { get; set; }
        [Required]
        public int MinimumAgeLimit { get; set; }
        [Required]
        public int DailyKmLimit { get; set; }
        [Required]
        public int CurrentKm { get; set; }

        [Required]
        public bool AirBag{ get; set; }

        [Required]
        public int BaggageCapacity { get; set; }

        [Required]
        public int SeatCount { get; set; }

        [Required]
        public int DailyRentalPrice { get; set; }

        [Required]
        public int Quantity { get; set; }//adet

     
        public string İmageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId ")]
        public Category Category { get; set; }


    }
}
