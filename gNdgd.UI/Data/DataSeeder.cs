﻿using gNdgd.UI.Constants;
using Microsoft.AspNetCore.Identity;

namespace gNdgd.UI.Data
{
    public class DataSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var isUserExists = await userMgr.FindByEmailAsync(admin.Email);
            if (isUserExists == null)
            {
                await userMgr.CreateAsync(admin, "Amin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
