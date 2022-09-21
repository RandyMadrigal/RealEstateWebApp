using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.ViewModels.VentaCategory;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITipoVentaService : IGenericService<SaveVentaVm, VentaVm, TipoVenta>
    {
        Task<List<VentaVm>> GetAllViewModelWithInclude();
    }
}
