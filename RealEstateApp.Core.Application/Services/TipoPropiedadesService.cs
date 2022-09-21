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
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoPropiedadesService : GenericService<SaveTipoPropiedadVm, TipoPropiedadVm, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository _TipoPropiedadesRepository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TipoPropiedadesService(ITipoPropiedadRepository TipoPropiedadesRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(TipoPropiedadesRepository, mapper)
        {
            _TipoPropiedadesRepository = TipoPropiedadesRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<List<TipoPropiedadVm>> GetAllViewModelWithInclude()
        {
            var List = await _TipoPropiedadesRepository.GetAllWithIncludeAsync(new List<string> { "Propiedades" });

            return List.Select(x => new TipoPropiedadVm
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                ProductsQuantity = x.Propiedades.Select(propiedades => propiedades.TipoPropiedadId).Count()
            }).ToList();
        }


    }
}
