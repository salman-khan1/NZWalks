using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAPi.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext 
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "27908b91-1d77-4270-bcc9-2ace053c15ed";  
            var writerRoleId = "13971a17-7430-4c54-bf6a-ecf5b5aec56f";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleID,
                    ConcurrencyStamp=readerRoleID,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                },
                  new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
