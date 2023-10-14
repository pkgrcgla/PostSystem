using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostSystem_EL.IdentityModels;

namespace PostSystem_PL.Components
{
    public class SideMenuViewComponent : ViewComponent
    {
        public readonly UserManager<AppUser> _userManager;

        public SideMenuViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            try
            {
                var username = User.Identity.Name;

                if (username != null)
                {
                    var user = _userManager.FindByNameAsync(username).Result;
                    return View(user);
                }
                return View(new AppUser());
            }
            catch (Exception ex)
            {
                return View(new AppUser());
            }
        }
    }
}
