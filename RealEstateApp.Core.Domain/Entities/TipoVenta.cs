using RealEstateApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{

   public class TipoVenta : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //navigation property
        public ICollection<Propiedades> Propiedades { get; set; }

    }
}


