using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;
using RealEstateApp.Core.Application.ViewModels.VentaCategory;
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory
{
    public class SaveTipoPropiedadVm
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
