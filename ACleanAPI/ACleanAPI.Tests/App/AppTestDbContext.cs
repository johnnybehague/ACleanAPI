using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Tests.App;

public partial class AppTestDbContext : DbContext
{
    public AppTestDbContext(DbContextOptions<AppTestDbContext> options)
            : base(options)
    {
    }

    public virtual DbSet<UserTestModel> Users { get; set; }
}
