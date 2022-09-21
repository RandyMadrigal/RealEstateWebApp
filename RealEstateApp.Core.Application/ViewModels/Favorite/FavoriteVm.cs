using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Favorite
{
    public class FavoriteVm
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string UserId { get; set; }

        public string Error { get; set; }
        public bool HasError { get; set; }
    }
}
