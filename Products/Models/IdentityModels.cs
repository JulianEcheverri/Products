using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Products.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Products.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, IntUserLogin, IntUserRole, IntUserClaim>
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IntRole, int, IntUserLogin, IntUserRole, IntUserClaim>
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//CascadeDeleteConvention

            //set Columns Names properties
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId"); 
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.UserName).HasColumnName("Name");
            modelBuilder.Entity<IntRole>().ToTable("Roles").Property(p => p.Id).HasColumnName("RoleId");
            modelBuilder.Entity<IntRole>().ToTable("Roles").Property(p => p.Name).HasColumnName("Name");
            modelBuilder.Entity<IntUserRole>().ToTable("UsersRoles").Property(p => p.UserId).HasColumnName("UserId");
            modelBuilder.Entity<IntUserRole>().ToTable("UsersRoles").Property(p => p.RoleId).HasColumnName("RoleId");
            modelBuilder.Entity<IntUserLogin>().ToTable("UsersLogins").Property(p => p.UserId).HasColumnName("UserId");
            modelBuilder.Entity<IntUserClaim>().ToTable("UsersClaims").Property(p => p.UserId).HasColumnName("UserId");

            //set composed keys for userid and roleid
            modelBuilder.Entity<IntUserRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IntUserLogin>().HasKey(x => new { x.LoginProvider, x.ProviderKey, x.UserId });

            //set userid and roleid like a foreings keys 
            modelBuilder.Entity<IntUserLogin>().HasRequired(ul => ul.ApplicationUser).WithMany(u => u.Logins).HasForeignKey(ul => ul.UserId);
            modelBuilder.Entity<IntUserClaim>().HasRequired(uc => uc.ApplicationUser).WithMany(u => u.Claims).HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<IntUserRole>().HasRequired(ur => ur.ApplicationUser).WithMany(u => u.Roles).HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<IntUserRole>().HasRequired(ur => ur.Roles).WithMany(u => u.Users).HasForeignKey(ur => ur.RoleId);
        }
    }

    //Estas clases se usan para personalizar e implementar Identity 2.0, y asi poder guardar el id del usuario como un int en lugar de un string


    //Implementacion de Identity Con el campo UserId de tipo Int. Clases Personalizadas
    public class IntUserLogin : IdentityUserLogin<int>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class IntUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual IntRole Roles { get; set; }
    }

    public class IntUserClaim : IdentityUserClaim<int>
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
    }

    public class IntRole : IdentityRole<int, IntUserRole>
    {
        public IntRole() { }
        public IntRole(string name) { Name = name; }
    }


    public class IntUserStore : UserStore<ApplicationUser, IntRole, int, IntUserLogin, IntUserRole, IntUserClaim>
    {
        public IntUserStore(ApplicationDbContext context)
            : base(context)
        { }
    }

    public class IntRoleStore : RoleStore<IntRole, int, IntUserRole>
    {
        public IntRoleStore(ApplicationDbContext context)
            : base(context)
        { }
    }
}