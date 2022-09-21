using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Domain.Common;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationContext(DbContextOptions<ApplicationContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options) 
        {
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = _userVm.UserName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = _userVm.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        //Indicando al Context las Entities que se van a utilizar.
        public DbSet<Propiedades> Propiedades { get; set; }
        public DbSet<TipoPropiedad> _TipoPropiedad { get; set; }
        public DbSet<TipoVenta> _tipoVenta { get; set; }
        public DbSet<TipoMejoras> _tipoMejoras { get; set; }
        public DbSet<Favorite> _favorite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tablas
            modelBuilder.Entity<Propiedades>()
                .ToTable("Propiedades");

            modelBuilder.Entity<TipoPropiedad>()
                .ToTable("TipoDePropiedades");

            modelBuilder.Entity<TipoVenta>()
                .ToTable("TipoDeVentas");

            modelBuilder.Entity<TipoMejoras>()
                .ToTable("TipoDeMejoras");

            modelBuilder.Entity<Favorite>()
                .ToTable("Favorita");
            #endregion

            #region Primary keys
            modelBuilder.Entity<Propiedades>()
                .HasKey(propiedades => propiedades.Id);
                    

            modelBuilder.Entity<TipoPropiedad>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TipoVenta>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<TipoMejoras>()
            .HasKey(x => x.Id);

            modelBuilder.Entity<Favorite>()
            .HasKey(x => x.Id);

            #endregion

            #region Unique Index

            modelBuilder.Entity<TipoPropiedad>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<TipoVenta>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<TipoMejoras>()
                .HasIndex(x => x.Name)
                .IsUnique();

            #endregion

            #region Relationships

            modelBuilder.Entity<TipoPropiedad>()
            .HasMany<Propiedades>(TipoPropiedad => TipoPropiedad.Propiedades) //Navigation Property de TipoPropiedadCategory en Propiedades
            .WithOne(propiedades => propiedades._tipoPropiedad) //Navigation Property de propiedades en TipoPropiedadCategory
            .HasForeignKey(propiedades => propiedades.TipoPropiedadId) //ForeignKey
            .OnDelete(DeleteBehavior.Cascade); //Borrado

            modelBuilder.Entity<TipoVenta>()
           .HasMany<Propiedades>(venta => venta.Propiedades) //Navigation Property de VentaCategory en Propiedades
           .WithOne(propiedades => propiedades._tipoVenta) //Navigation Property de Propiedades en VentaCategory
           .HasForeignKey(propiedades => propiedades.TipoVentaId) //ForeignKey
           .OnDelete(DeleteBehavior.Cascade); //Borrado

            modelBuilder.Entity<TipoMejoras>()
          .HasMany<Propiedades>(mejoras => mejoras.Propiedades) //Navigation Property de mejoras en Propiedades
          .WithOne(propiedades => propiedades._tipoMejoras) //Navigation Property de Propiedades en mejoras
          .HasForeignKey(propiedades => propiedades.TipoMejoraId) //ForeignKey
          .OnDelete(DeleteBehavior.Cascade); 
                

            #endregion

            #region Contraints Propiedades

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.Descripcion)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.Direccion)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.Codigo)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.Precio)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.CantidadHabitaciones)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.CantidadBaños)
            .IsRequired();

            modelBuilder.Entity<Propiedades>().Property(propiedades => propiedades.Dimensiones)
            .IsRequired();

            #endregion

            #region Contraints TipoPropiedades

            modelBuilder.Entity<TipoPropiedad>().Property(x => x.Name)
            .IsRequired();

            #endregion

            #region Contraints VentasCategory

            modelBuilder.Entity<TipoVenta>().Property(x => x.Name)
            .IsRequired();

            #endregion

            #region Contraints MejorasCategory

            modelBuilder.Entity<TipoMejoras>().Property(x => x.Name)
            .IsRequired();

            #endregion

            #region Contraints Favorite

            modelBuilder.Entity<Favorite>().Property(x => x.Codigo)
            .IsRequired();

            modelBuilder.Entity<Favorite>().Property(x => x.UserId)
           .IsRequired();

            #endregion


        }

    }
}
