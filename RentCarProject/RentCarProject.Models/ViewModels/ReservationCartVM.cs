using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarProject.Models.ViewModels
{
    public class ReservationCartVM
    {
        public IEnumerable <ReservationCart> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }




    }
}
