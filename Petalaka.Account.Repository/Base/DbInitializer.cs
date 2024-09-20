using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petalaka.Account.Repository.Base
{
    public class DbInitializer
    {
        private readonly PetalakaDbContext _dbContext;
        private readonly ILogger _logger;
        public DbInitializer(PetalakaDbContext dbContext, ILogger<DbInitializer> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            try
            {
                await _dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in initialize database ", ex);
                throw;
            }    
        }
    }
}
