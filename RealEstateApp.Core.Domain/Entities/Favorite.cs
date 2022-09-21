using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string UserId { get; set; }
    }
}
