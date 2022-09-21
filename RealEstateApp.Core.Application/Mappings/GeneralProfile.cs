using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;
using RealEstateApp.Core.Application.ViewModels.VentaCategory;
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstateApp.Core.Application.ViewModels.Favorite;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();


            #region Randy


            #region Update User info
            CreateMap<RegisterRequest, EditUser>()

                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore());
            #endregion

            #region Update Admin info
            CreateMap<RegisterRequest, EditAdmin>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
                .ForMember(x => x.Url, opt => opt.Ignore());
            #endregion

            #region Propiedades

            CreateMap<Propiedades, SavePropiedadesVm>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ForMember(x => x.FileMain, opt => opt.Ignore())
               .ForMember(x => x.File2, opt => opt.Ignore())
               .ForMember(x => x.File3, opt => opt.Ignore())
               .ForMember(x => x.File4, opt => opt.Ignore())
               .ForMember(x => x.TiposPropiedades, opt => opt.Ignore())
               .ForMember(x => x.TiposVentas, opt => opt.Ignore())
               .ForMember(x => x.MejorasPropiedades, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x._tipoPropiedad, opt => opt.Ignore())
               .ForMember(x => x._tipoVenta, opt => opt.Ignore())
               .ForMember(x => x._tipoMejoras, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore());

            CreateMap<Propiedades, PropiedadesVm>()
               .ForMember(x => x.PrecioMin, opt => opt.Ignore())
               .ForMember(x => x.PrecioMax, opt => opt.Ignore())
              .ForMember(x => x.Total, opt => opt.Ignore())
              .ForMember(x => x.TipoPropiedadName, opt => opt.Ignore())
              .ForMember(x => x.TipoVentaName, opt => opt.Ignore())
              .ForMember(x => x.TipoMejorasName, opt => opt.Ignore())
              .ReverseMap()
              .ForMember(x => x._tipoPropiedad, opt => opt.Ignore())
               .ForMember(x => x._tipoVenta, opt => opt.Ignore())
               .ForMember(x => x._tipoMejoras, opt => opt.Ignore())
              .ForMember(x => x.Created, opt => opt.Ignore())
              .ForMember(x => x.CreatedBy, opt => opt.Ignore())
              .ForMember(x => x.LastModified, opt => opt.Ignore());
             

            #endregion

            #region Tipos de propiedades

              CreateMap<TipoPropiedad, SaveTipoPropiedadVm>()
               .ReverseMap()
               .ForMember(x => x.Propiedades, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoPropiedad, TipoPropiedadVm>()
              .ReverseMap()
               .ForMember(x => x.Propiedades, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Tipos de Ventas

            CreateMap<TipoVenta, SaveVentaVm>()
             .ReverseMap()
             .ForMember(x => x.Propiedades, opt => opt.Ignore())
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoVenta, VentaVm>()
              .ReverseMap()
               .ForMember(x => x.Propiedades, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Mejoreas de las propiedades

            CreateMap<TipoMejoras, SaveMejorasVm>()
             .ReverseMap()
             .ForMember(x => x.Propiedades, opt => opt.Ignore())
             .ForMember(x => x.Created, opt => opt.Ignore())
             .ForMember(x => x.CreatedBy, opt => opt.Ignore())
             .ForMember(x => x.LastModified, opt => opt.Ignore())
             .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<TipoMejoras, MejorasVm>()
              .ReverseMap()
               .ForMember(x => x.Propiedades, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            #endregion

            #region Favorite

            CreateMap<Favorite, SaveFavoriteVm>()
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Favorite, FavoriteVm>()
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<PropiedadesVm, SaveFavoriteVm>()
            .ForMember(x => x.UserId, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.PrecioMin, opt => opt.Ignore())
            .ForMember(x => x.PrecioMax, opt => opt.Ignore())
            .ForMember(x => x.Total, opt => opt.Ignore())
            .ForMember(x => x.Descripcion, opt => opt.Ignore())
            .ForMember(x => x.Direccion, opt => opt.Ignore())
            .ForMember(x => x.Precio, opt => opt.Ignore())
            .ForMember(x => x.CantidadHabitaciones, opt => opt.Ignore())
            .ForMember(x => x.CantidadBaños, opt => opt.Ignore())
            .ForMember(x => x.Dimensiones, opt => opt.Ignore())
            .ForMember(x => x.ImgMain, opt => opt.Ignore())
            .ForMember(x => x.Img2, opt => opt.Ignore())
            .ForMember(x => x.Img3, opt => opt.Ignore())
            .ForMember(x => x.Img4, opt => opt.Ignore())
            .ForMember(x => x.TipoPropiedadCategoryId, opt => opt.Ignore())
            .ForMember(x => x.VentaCategoryId, opt => opt.Ignore())
            .ForMember(x => x.MejoraCategoryId, opt => opt.Ignore())
            .ForMember(x => x.TipoPropiedadName, opt => opt.Ignore())
            .ForMember(x => x.TipoVentaName, opt => opt.Ignore())
            .ForMember(x => x.MejoraCategoryId, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<PropiedadesVm, FavoriteVm>()
            .ForMember(x => x.UserId, opt => opt.Ignore())
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.PrecioMin, opt => opt.Ignore())
            .ForMember(x => x.PrecioMax, opt => opt.Ignore())
            .ForMember(x => x.Total, opt => opt.Ignore())
            .ForMember(x => x.Descripcion, opt => opt.Ignore())
            .ForMember(x => x.Direccion, opt => opt.Ignore())
            .ForMember(x => x.Precio, opt => opt.Ignore())
            .ForMember(x => x.CantidadHabitaciones, opt => opt.Ignore())
            .ForMember(x => x.CantidadBaños, opt => opt.Ignore())
            .ForMember(x => x.Dimensiones, opt => opt.Ignore())
            .ForMember(x => x.ImgMain, opt => opt.Ignore())
            .ForMember(x => x.Img2, opt => opt.Ignore())
            .ForMember(x => x.Img3, opt => opt.Ignore())
            .ForMember(x => x.Img4, opt => opt.Ignore())
            .ForMember(x => x.TipoPropiedadCategoryId, opt => opt.Ignore())
            .ForMember(x => x.VentaCategoryId, opt => opt.Ignore())
            .ForMember(x => x.MejoraCategoryId, opt => opt.Ignore())
            .ForMember(x => x.TipoPropiedadName, opt => opt.Ignore())
            .ForMember(x => x.TipoVentaName, opt => opt.Ignore())
            .ForMember(x => x.MejoraCategoryId, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());


            #endregion



            #endregion
        }
    }
}
