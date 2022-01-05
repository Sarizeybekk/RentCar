using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.Models
{
     public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

    
        public string Adres { get; set; }

        public string Sehir { get; set; }

        
        public string Semt { get; set; }

        
        public string PostaKodu { get; set; }

        [NotMapped]
        //orm aracında karsılıgı olmayan  bir kolon oldugunu bildirmek
        public string Role { get; set; }












    }
}
