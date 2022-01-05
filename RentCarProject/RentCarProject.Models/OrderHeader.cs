using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.Models
{
     public class OrderHeader
    {
        [Key]
        public int Id{ get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId ")]

        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public double OrderTotal { get; set; }
      
        public string OrderStatus { get; set; }
        //bunlar rezervasyonu sepete attıgında gerekli kısım şimdi ise kiralık bilgilerini alıcaz kullanıcıdan
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Adres { get; set; }

      

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentDateStart { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RentDateEnd { get; set; }

        public int FirstKM { get; set; }
        public int LastKm { get; set; }


        //[Required]
        //public string CartName { get; set; }
        //[Required]
        //public string CartNumber { get; set; }
        //[Required]
        //public string ExpirationMonth { get; set; }

        //[Required]
        //public string ExpirationYear { get; set; }
        //[Required]
        //public string Cvc { get; set; }







    }
}
