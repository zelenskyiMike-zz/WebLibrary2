using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.DTO;
using WebLibrary2.BusinessLogicLayer.Infrastructure;
using WebLibrary2.BusinessLogicLayer.Interfaces;
using WebLibrary2.WebUI.RoleModels;

namespace WebLibrary2.WebUI.Controllers.IdentityControllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserView userDto = new UserView { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            // await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserView userDto = new UserView
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    return RedirectToAction("RegisterSucceed", "Account");
                }
                if (!operationDetails.Succedeed)
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            return View(model);
        }

        public ActionResult RegisterSucceed()
        {
            return View();
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserView
            {
                Email = "zzTop@mail.ru",
                UserName = "zzAdmin",
                Password = "11root11",
                Name = "Zelenskiy M.",
                Address = "ул. Целиноградская, 58",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}