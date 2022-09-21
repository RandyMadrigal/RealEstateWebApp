using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IFavoriteService : IGenericService<SaveFavoriteVm, FavoriteVm, Favorite>
    {

        Task<List<PropiedadesVm>> GetAllViewModelWithInclude();

        Task<SaveFavoriteVm> GetByCodeDelete(int Codigo);

        Task<List<PropiedadesVm>> GetAllFavListFilter(FilterVm filtro);
    }
}
