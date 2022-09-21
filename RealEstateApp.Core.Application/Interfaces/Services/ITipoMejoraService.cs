using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITipoMejoraService : IGenericService<SaveMejorasVm,MejorasVm,TipoMejoras>
    {
        Task<List<MejorasVm>> GetAllViewModelWithInclude();
    }
}
