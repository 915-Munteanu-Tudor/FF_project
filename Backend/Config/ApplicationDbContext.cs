using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Config
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<DataPoint> DataPointsDaily { get; set; } = null!;
        public DbSet<DataPointIntra> DataPointsIntraDay { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
};