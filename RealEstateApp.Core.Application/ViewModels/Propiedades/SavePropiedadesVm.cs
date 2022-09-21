using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;
using RealEstateApp.Core.Application.ViewModels.VentaCategory;
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;

namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class SavePropiedadesVm 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar una Descripción")]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe colocar la Dirección")]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }

        public int Codigo { get; set; }

       
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar el precio de la propiedad")]
        [DataType(DataType.Currency)]
        public double Precio { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la Cantidad de habitaciones")]
        public int CantidadHabitaciones { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la Cantidad de Baños")]
        public int CantidadBaños { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe colocar la medida de la Propiedad")]
        public double Dimensiones { get; set; }

        #region Img
        public string ImgMain { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile FileMain { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File2 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File3 { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File4 { get; set; }

        #endregion

        #region Relationship
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe Seleccionar un tipo de Propiedad")]
        public int TipoPropiedadId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe Seleccionar un tipo de Venta")]
        public int TipoVentaId { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Debe Seleccionar una o varias mejoras ")]
        public int TipoMejoraId { get; set; }

        #region para obtener informacion de las relaciones y mostrar en los Formularios
        public List<TipoPropiedadVm> TiposPropiedades { get; set; }
        public List<VentaVm> TiposVentas { get; set; }
        public List<MejorasVm> MejorasPropiedades { get; set; }
        #endregion

        #endregion

        public bool HasError { get; set; }
        public string Error { get; set; }
        public string LastModifiedBy { get; set; }




    }
}
