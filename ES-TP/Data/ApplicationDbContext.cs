using ES_TP.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ES_TP.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected readonly IHttpContextAccessor _httpAccessor;

        #region "ctor"
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpAccessor) : base(options)
        {
            _httpAccessor = httpAccessor;
        }
        #endregion

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            SetDefaultPropertiesOnEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetDefaultPropertiesOnEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetDefaultPropertiesOnEntities()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            if (entries != null)
            {
                var identityName = _httpAccessor.HttpContext?.User.Identity.Name;

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(prop => prop.RowVersion).IsModified = false;

                        entry.Entity.UpdatedAt = null;
                        entry.Entity.CreatedAt = DateTime.Now;
                        entry.Entity.CreatedBy = identityName ?? "unknown";
                    }

                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property(prop => prop.CreatedAt).IsModified = false;
                        entry.Property(prop => prop.CreatedBy).IsModified = false;
                        entry.Property(prop => prop.RowVersion).IsModified = false;

                        entry.Entity.UpdatedAt = DateTime.Now;
                        entry.Entity.UpdatedBy = identityName ?? "unknown";
                    }
                }
            }
        }
    }

}
