using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.BLL.DTO;
using WebLibrary2.BLL.Infrastructure;
using WebLibrary2.BLL.Interfaces;
using WebLibrary2.WebUI.RoleModels;

namespace WebLibrary2.WebUI.Controllers.IdentityControllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                //DOESNT WORK HERE
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        private IAuthenticationManager AuthentificationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Account
        public ActionResult Login()
        {

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authentificate(userDTO);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthentificationManager.SignOut();
                    AuthentificationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            AuthentificationManager.SignOut();
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
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDTO = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDTO);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }



        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru",
                Password = "ad46D_ewr3",
                Name = "Семен Семенович Горбунков",
                Address = "ул. Спортивная, д.30, кв.75",
                Role = "admin",
            }, new List<string> {"user","admin"});
        }
    }
}