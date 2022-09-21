using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;


namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITipoPropiedadService : IGenericService<SaveTipoPropiedadVm, TipoPropiedadVm, TipoPropiedad>
    {
        Task<List<TipoPropiedadVm>> GetAllViewModelWithInclude();
        
    }
}
