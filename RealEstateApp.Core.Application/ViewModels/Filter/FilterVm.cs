using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.ViewModels.Filter
{
    public class FilterVm
    {
        public string? TipoPropiedadName { get; set; }
        public double? PrecioMin { get; set; }
        public double? PrecioMax { get; set; }
        public int? CantidadHabitaciones { get; set; }
        public int? CantidadBaños { get; set; }
    }
}
