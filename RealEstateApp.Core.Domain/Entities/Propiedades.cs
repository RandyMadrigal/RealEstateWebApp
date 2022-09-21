using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Propiedades : AuditableBaseEntity
    {       
        public string Descripcion { get; set; }

       // public string TipoPropiedad { get; set; }
        public string Direccion { get; set; }
        public int Codigo { get; set; }

      //  public string TipoVenta { get; set; }
        public double Precio { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public double Dimensiones { get; set; }
        public string ImgMain { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }

        //Fk
        public int TipoPropiedadId { get; set; }
        public int TipoVentaId { get; set; }
        public int TipoMejoraId { get; set; }

        //Navigation Property
        public TipoPropiedad _tipoPropiedad { get; set; }
        public TipoVenta _tipoVenta { get; set; }
        public TipoMejoras _tipoMejoras { get; set; }


    }
}
