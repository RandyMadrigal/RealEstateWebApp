using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.VentaCategory
{
    public class SaveVentaVm
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe colocar el nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar la descripcion")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
