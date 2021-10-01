using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store_Models;
using Store_Utility;
using System;
using System.Linq;

namespace Store_DataAccess.Initializer
{
    //класс создает первого админа при первом запуске приложения
    //после создания новой БД
    //вызывается в Startup.Configure
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            try 
            {
                if(_db.Database.GetPendingMigrations().Count() > 0) //проверка есть ли незавершенные миграции
                {
                    _db.Database.Migrate(); //выполнение незавершенных миграций
                }
            }
            catch (Exception ex)
            {

            }
            //создание первых ролей в чистой БД
            if (!_roleManager.RoleExistsAsync(WebConstants.AdminRole).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebConstants.AdminRole)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebConstants.CustomerRole)).GetAwaiter().GetResult();
            }
            else return;
            //создание первого админа
            _userManager.CreateAsync(new ApplicationUser { 
                UserName = "***@gmail.com",
                Email = "***@gmail.com",
                EmailConfirmed = true,
                FullName = "Admin User",
                PhoneNumber = "0000000"
            },"******").GetAwaiter().GetResult();

            ApplicationUser admin = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "***@gmail.com");
            //назначение роли админа
            _userManager.AddToRoleAsync(admin, WebConstants.AdminRole).GetAwaiter().GetResult();
        }
    }
}
