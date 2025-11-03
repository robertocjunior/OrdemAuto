using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Infra.Contexts
{
    public class RuntimeAnalyzerDbContextFactory : IDesignTimeDbContextFactory<AnalyzerDbContext>
    {
        public AnalyzerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AnalyzerDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ORDEM_AUTO;Username=postgres;Password=1234");
            return new AnalyzerDbContext(optionsBuilder.Options);
        }
    }
}
