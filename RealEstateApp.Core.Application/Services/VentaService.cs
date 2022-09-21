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
using RealEstateApp.Core.Application.ViewModels.VentaCategory;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class VentaService : GenericService<SaveVentaVm, VentaVm, TipoVenta>, ITipoVentaService
    {
        private readonly ITipoVentasRepository _ventasRepository;
        private readonly IMapper _mapper;
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VentaService(ITipoVentasRepository ventasRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(ventasRepository, mapper)
        {
            _ventasRepository = ventasRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<List<VentaVm>> GetAllViewModelWithInclude()
        {
            var List = await _ventasRepository.GetAllWithIncludeAsync(new List<string> { "Propiedades" });

            return List.Select(x => new VentaVm
            {
                Name = x.Name,
                Description = x.Description,
                Id = x.Id,
                ProductsQuantity = x.Propiedades.Select(venta => venta.TipoVentaId).Count()
            }).ToList();
        }


    }
}
