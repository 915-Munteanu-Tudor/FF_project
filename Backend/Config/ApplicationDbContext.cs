using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Config
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<DataPoint> DataPointsDaily { get; set; } = null!;
        public virtual DbSet<DataPointIntra> DataPointsIntraDay { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
};