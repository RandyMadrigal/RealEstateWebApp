using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadesRepository : GenericRepository<Propiedades>, IPropiedadesRepository
    {

        private readonly ApplicationContext _dbContext;

        public PropiedadesRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //Find By Codigo de propiedad
        public async Task<Propiedades> GetByCodeAsync(int Codigo)
        {
            return await _dbContext.Set<Propiedades>()
                .FirstOrDefaultAsync(x => x.Codigo == Codigo);
        }

    }
}
