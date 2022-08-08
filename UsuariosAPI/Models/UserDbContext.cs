using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Models
{
    //configurar o identity
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        //configura o secrets
        private IConfiguration _iconfiguration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration iconfiguration) : base(opt)
        {
            _iconfiguration = iconfiguration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //criando usuario admin
            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999,
            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

            //admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            //camuflando os dados com secrets
            admin.PasswordHash = hasher.HashPassword(admin, _iconfiguration.GetValue<string>("admininfo:password"));

            builder.Entity<CustomIdentityUser>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" });

            builder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int> { Id = 99997, Name = "regular", NormalizedName = "REGULAR" });


            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 });
        }
    }
}
