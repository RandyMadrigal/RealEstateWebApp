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
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class MejorasService : GenericService<SaveMejorasVm, MejorasVm, TipoMejoras>, ITipoMejoraService
    {
        private readonly ITipoMejorasRepository _mejorasRepository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MejorasService(ITipoMejorasRepository mejorasRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(mejorasRepository, mapper)
        {
            _mejorasRepository = mejorasRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<List<MejorasVm>> GetAllViewModelWithInclude()
        {
            var List = await _mejorasRepository.GetAllWithIncludeAsync(new List<string> { "Propiedades" });

            return List.Select(x => new MejorasVm
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                ProductsQuantity = x.Propiedades.Select(mejoras => mejoras.TipoMejoraId).Count()
            }).ToList();
        }


    }
}
