using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropiedadesService: IGenericService<SavePropiedadesVm,PropiedadesVm,Propiedades>
    {
        Task<List<PropiedadesVm>> GetAllViewModelWithInclude();
        Task<List<PropiedadesVm>> GetAllViewModelWithInclude(string UserName);
        Task<List<PropiedadesVm>> GetPropiedadByCode(int Codigo);
        Task<SavePropiedadesVm> GetByCode(int Codigo);
        Task<int> GetAllPropiedades();
        Task<List<PropiedadesVm>> GetAllWithFilter(FilterVm filtro);
        Task<List<PropiedadesVm>> GetAllFilterAgente(FilterVm filtro);




    }
}
