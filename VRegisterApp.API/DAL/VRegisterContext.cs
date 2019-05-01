using Microsoft.EntityFrameworkCore;
using VRegisterApp.API.Models;

namespace VRegisterApp.API.DAL
{
    public class VRegisterContext : DbContext
    {
        public VRegisterContext(DbContextOptions<VRegisterContext> options) : base(options)
        {   
        }
        public DbSet<User> Users { get; set; }
    }
}