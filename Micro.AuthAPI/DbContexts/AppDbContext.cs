using Micro.AuthAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Micro.AuthAPI.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
