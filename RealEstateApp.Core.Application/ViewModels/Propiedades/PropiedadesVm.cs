using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class PropiedadesVm 
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int Codigo { get; set; }       
        public double Precio { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public double Dimensiones { get; set; }
        public string ImgMain { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }


        public int TipoPropiedadCategoryId { get; set; }
        public int VentaCategoryId { get; set; }
        public int MejoraCategoryId { get; set; }


        public string TipoPropiedadName { get; set; }
        public string TipoVentaName { get; set; }
        public string TipoMejorasName { get; set; }

        public string LastModifiedBy { get; set; }

        public int Total { get; set; }

        #region Filtros
        public double? PrecioMin { get; set; }
        public double? PrecioMax { get; set; }

        #endregion


    }
}
