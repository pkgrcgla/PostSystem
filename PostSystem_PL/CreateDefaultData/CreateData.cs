using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PostSystem_BL.EmailSenderProcess;
using PostSystem_EL.IdentityModels;
using PostSystem_UL;
using Serilog.Core;

namespace PostSystem_PL.CreateDefaultData
{
    public class CreateData
    {
        private readonly Logger _logger;
        public CreateData(Logger logger)
        {
            _logger = logger;
        }

        public void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var emailManager = serviceProvider.GetRequiredService<IEmailManager>();

            //var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = serviceProvider.GetService<AddressbookContext>();
            var configuration = serviceProvider.GetService<IConfiguration>();// appsettings.json dosyasına ulaşmak için


            var allRoles = Enum.GetNames(typeof(AllRoles));


            foreach (string role in allRoles)
            {
                var result = roleManager.RoleExistsAsync(role).Result; //rolden var mı?
                if (!result) //rolden yok!
                {
                    AppRole r = new AppRole()
                    {
                        InsertedDate = DateTime.Now,
                        Name = role,
                        IsDeleted = false,
                        Description = $"Sistem tarafından oluşturuldu"
                    };
                    var roleResult = roleManager.CreateAsync(r).Result;

                    //roleresulta bakalım
                    if (!roleResult.Succeeded)
                    {
                        //log email
                    }
                }

            }



        }
    }
}
