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
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private readonly ApplicationContext _dbContext;

        public FavoriteRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Find by User Id (Identity String) List
        public  async Task<List<Favorite>> GetByStringIdAsync(string Id)
        {
            return await _dbContext.Set<Favorite>()
                .Where( x => x.UserId == Id)
                .ToListAsync();
        }


        //Find By Codigo de propiedad
        public async Task<Favorite> GetByCodeAsync(int Codigo)
        {
            return await _dbContext.Set<Favorite>()
                .FirstOrDefaultAsync(x => x.Codigo == Codigo);
        }

    }
}
