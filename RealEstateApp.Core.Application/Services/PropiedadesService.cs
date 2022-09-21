using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadesService : GenericService<SavePropiedadesVm, PropiedadesVm, Propiedades>, IPropiedadesService
    {
        private readonly IPropiedadesRepository _propiedadesRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse userVm;

        public PropiedadesService(IPropiedadesRepository propiedadesRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(propiedadesRepository, mapper)
        {
            _propiedadesRepository = propiedadesRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public override async Task<SavePropiedadesVm> Add(SavePropiedadesVm vm)
        {
            
            vm.Codigo = CodeGenerator.Generate();           
            return await base.Add(vm);
        }

        public async Task<SavePropiedadesVm> GetByCode(int Codigo)       
        {          

            var code = await _propiedadesRepository.GetByCodeAsync(Codigo);

            SavePropiedadesVm savePropiedadesVm = _mapper.Map<SavePropiedadesVm>(code);

            return savePropiedadesVm;

        }

        //Obtener todas las propiedades del sistema (INT)
        public async Task<int> GetAllPropiedades()
        {
            var propiedadesList = await _propiedadesRepository.GetAllAsync();

            var result = 0;

            foreach (Propiedades item in propiedadesList)
            {
                result++;
            }
            return result;
        }

        #region Todas las propiedades creadas del Agente

        public async Task<List<PropiedadesVm>> GetAllViewModelWithInclude()
        {
            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            return ListPropiedades.Where(x => x.CreatedBy == userVm.UserName || x.LastModifiedBy == userVm.UserName).Select(x => new PropiedadesVm
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

        public async Task<List<PropiedadesVm>> GetAllViewModelWithInclude(string UserName)
        {
            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            return ListPropiedades.Where(x => x.CreatedBy == UserName || x.LastModifiedBy == UserName).Select(x => new PropiedadesVm
            {
                Id = x.Id,
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


        #endregion

      

        #region Obtener una sola propiedad 
        public async Task<List<PropiedadesVm>> GetPropiedadByCode(int Codigo)
        {
            var Propiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            return Propiedades.Where(x => x.Codigo == Codigo).Select(x => new PropiedadesVm
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

        #endregion

        #region Filter home/Index - Cliente /Index

        public async Task<List<PropiedadesVm>> GetAllWithFilter(FilterVm filtro)
        {
            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            var List = ListPropiedades.Select(x => new PropiedadesVm
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
                List = List.Where(x => x.TipoPropiedadName == filtro.TipoPropiedadName).ToList();

            }

            if (filtro.PrecioMin != null && filtro.PrecioMax != null)
            {
                List = List.Where(x => x.Precio >= filtro.PrecioMin.Value && x.Precio <= filtro.PrecioMax.Value).ToList();
                    
            }

            if (filtro.CantidadHabitaciones != null)
            {
                List = List.Where(x => x.CantidadHabitaciones == filtro.CantidadHabitaciones.Value).ToList();

            }

            if (filtro.CantidadBaños != null)
            {
                List = List.Where(x => x.CantidadBaños == filtro.CantidadBaños.Value).ToList();


            }

            return List;


        }



        #endregion

        #region Filter - Agente Index 
        public async Task<List<PropiedadesVm>> GetAllFilterAgente(FilterVm filtro)
        {
            var ListPropiedades = await _propiedadesRepository.GetAllWithIncludeAsync(new List<string> { "_tipoPropiedad", "_tipoVenta", "_tipoMejoras" });

            var list = ListPropiedades.Where(x => x.CreatedBy == userVm.UserName || x.LastModifiedBy == userVm.UserName).Select(x => new PropiedadesVm
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
