using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hospital.Infrastructure;
public class HospitalDesignTimeDbContextFactory : IDesignTimeDbContextFactory<HospitalDbContext>
{
    public HospitalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HospitalDbContext>();
        optionsBuilder
            .UseMySql("server=localhost;port=3306;database=dummydb;", new MySqlServerVersion(new Version(9, 5, 0)))
            .UseSnakeCaseNamingConvention();

        return new HospitalDbContext(optionsBuilder.Options, new Domain.DataSeeder());
    }
}
