using BAL.Interface;
using BAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core.Infrastructure;
using RentalCarsandBikes.Models;
using System.Data.SqlTypes;

namespace RentalCarsandBikes.Controllers
{
    public class RegisterController : Controller
    {
        public readonly BAL_Interface _bal_interface;
        public RegisterController(BAL_Interface bal_interface)
        {
            _bal_interface = bal_interface;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRegister(RegisterViewPL registerViewPL)
        {
            if (registerViewPL != null)
            {
                var register = new RegisterViewBAL()
                {
                    UserName = registerViewPL.UserName,
                    Gender = registerViewPL.Gender,
                    Age = registerViewPL.Age,
                    State = registerViewPL.State,
                    City = registerViewPL.City,
                    MobileNo = registerViewPL.MobileNo,
                    EmailId = registerViewPL.EmailId,
                    Password = registerViewPL.Password,
                    PhotoId = registerViewPL.PhotoId,
                    PhotoName = registerViewPL.PhotoName,
                    ProofName = registerViewPL.ProofName,
                    IdProof1 = registerViewPL.IdProof1,
                    Accept = registerViewPL.Accept,
                };
                _bal_interface.UserRegister(register);
            }
            return Redirect("/Home/Privacy");
        }
        [HttpGet]
        public IActionResult GetState()
        {
            var state = _bal_interface.GetState();
            return Json(state);
        }
        [HttpPost]
        public IActionResult GetCity(int cityid)
        {
            var city = _bal_interface.GetCity(cityid);
            return Json(city);
        }
        [HttpPost]
        public IActionResult CheckLogin(LoginCheckViewPL loginCheckViewPL)
        {

            var liginid = new LoginCheckViewBAL()
            {
                EmailId = loginCheckViewPL.EmailId,
                Password = loginCheckViewPL.Password,
            };
            var logindata = _bal_interface.LoginCheck(liginid);
            if (logindata == null)
            {
                TempData["Error"] = "Invalid  User Name or Password";
                return Redirect("/Home/Privacy");
            }
            else if (logindata.RollId == (int)RollId.User)
            {
                HttpContext.Session.SetString("name", logindata.UserName);
                HttpContext.Session.SetInt32("id", logindata.UserId);
                return Redirect("/User/Index");
            }
            else if (logindata.RollId == (int)RollId.Admin)
            {
                HttpContext.Session.SetString("name", logindata.UserName);
                return Redirect("/Admin/UserCount");
            }
            else
            {
                return View();
            }

        }
        public IActionResult CheckMail(string Email)
        {
            var mail = _bal_interface.CheckMail(Email);
            return Json(mail);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/Privacy");
        }

    }
}
