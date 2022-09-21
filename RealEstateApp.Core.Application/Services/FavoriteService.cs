using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class FavoriteService : GenericService<SaveFavoriteVm, FavoriteVm, Favorite>, IFavoriteService
    {
        private readonly IPropiedadesRepository _propiedadesRepository;
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteService(IPropiedadesRepository propiedadesRepository,IFavoriteRepository favoriteRepository, 
            IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(favoriteRepository, mapper)
        {
            _propiedadesRepository = propiedadesRepository;
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public override async Task<SaveFavoriteVm> Add(SaveFavoriteVm vm)
        {

            vm.UserId = _userVm.Id;

            var fav = await _favoriteRepository.GetByStringIdAsync(_userVm.Id);

            for (int i = 0; i < fav.ToArray().Length; i++)
            {
                if (fav[i].Codigo == vm.Codigo)
                {
                    vm.HasError = true;
                    vm.Error = "Esta propiedad ya exite en tu lista de Favoritos";
                    return vm;
                }
            }

            vm.HasError = false;
            vm.Error = "Agregado con exito!";

            await base.Add(vm);

            return vm;
        }

        public async Task<SaveFavoriteVm> GetByCodeDelete(int Codigo)
        {

            var codeList = await _favoriteRepository.GetAllAsync();
                        

            foreach (Favorite item in codeList)
            {
                if (item.Codigo == Codigo && item.UserId == _userVm.Id)
                {
                    var delete = await _favoriteRepository.GetByIdAsync(item.Id);

                    SaveFavoriteVm entity = _mapper.Map<SaveFavoriteVm>(delete);

                    return entity;
                }
            }

                return null;
        }




        public async Task<List<PropiedadesVm>> GetAllViewModelWithInclude()
        {
            var favList = await _favoriteRepository.GetByStringIdAsync(_userVm.Id);

            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            return ListPropiedades.Where( x => favList.Any( i => i.Codigo == x.Codigo)).Select(x => new PropiedadesVm
            {
                Descripcion = x.Descripcion,
                Direccion = x.Direccion,
                Codigo = x.Codigo,
                Precio = x.Precio,
                CantidadHabitaciones = x.CantidadHabitaciones,
                CantidadBaños = x.CantidadBaños,
                Dimensiones = x.Dimensiones,
                ImgMain = x.ImgMain,
                Img2 = x.Img2,
                Img3 = x.Img3,
                Img4 = x.Img4,
                TipoPropiedadName = x._tipoPropiedad.Name,
                TipoVentaName = x._tipoVenta.Name,
                TipoMejorasName = x._tipoMejoras.Name,

            }).ToList();
        }

        #region Filter - Cliente Fav list propiedades
        public async Task<List<PropiedadesVm>> GetAllFavListFilter(FilterVm filtro)
        {
            var favList = await _favoriteRepository.GetByStringIdAsync(_userVm.Id);

            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            var list = ListPropiedades.Where(x => favList.Any(i => i.Codigo == x.Codigo)).Select(x => new PropiedadesVm
            {
                Descripcion = x.Descripcion,
                Direccion = x.Direccion,
                Codigo = x.Codigo,
                Precio = x.Precio,
                CantidadHabitaciones = x.CantidadHabitaciones,
                CantidadBaños = x.CantidadBaños,
                Dimensiones = x.Dimensiones,
                ImgMain = x.ImgMain,
                Img2 = x.Img2,
                Img3 = x.Img3,
                Img4 = x.Img4,
                TipoPropiedadName = x._tipoPropiedad.Name,
                TipoVentaName = x._tipoVenta.Name,
                TipoMejorasName = x._tipoMejoras.Name,

            }).ToList();

            if (filtro.TipoPropiedadName != null)
            {
                list = list.Where(x => x.TipoPropiedadName == filtro.TipoPropiedadName).ToList();

            }

            if (filtro.PrecioMin != null && filtro.PrecioMax != null)
            {
                list = list.Where(x => x.Precio >= filtro.PrecioMin.Value && x.Precio <= filtro.PrecioMax.Value).ToList();

            }

            if (filtro.CantidadHabitaciones != null)
            {
                list = list.Where(x => x.CantidadHabitaciones == filtro.CantidadHabitaciones.Value).ToList();

            }

            if (filtro.CantidadBaños != null)
            {
                list = list.Where(x => x.CantidadBaños == filtro.CantidadBaños.Value).ToList();
            }

            return list;




        }
        #endregion
    }
}
