using BAL.Interface;
using BAL.ViewModel;
using DAL.EntityModels;
using DAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Services.Users;
using RentalCarsandBikes.Models;
using System.Collections.Generic;

namespace RentalCarsandBikes.Controllers
{
    public class AdminController : Controller
    {
        public readonly BAL_Interface _bAL_Interface;
        public AdminController(BAL_Interface bAL_Interface)
        {
            _bAL_Interface = bAL_Interface;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddVehicle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddVehicle(AddVehicleViewPL addVehicleViewPL)
        {
            var addvehicledata = new AddVehicleViewBAL()
            {
                VehicleName = addVehicleViewPL.VehicleName,
                VehicleNo = addVehicleViewPL.VehicleNo,
                VehiclePhoto = addVehicleViewPL.VehiclePhoto,
                VehicleType = addVehicleViewPL.VehicleType,
                NoOfVehicle = addVehicleViewPL.NoOfVehicle,
            };
            var vehicledatalist = _bAL_Interface.AddVehicle(addvehicledata);
            TempData["noofvehicle"] = vehicledatalist.NoOfVehicle;
            return Redirect("/Admin/AdminVehicleList");
        }
        [HttpGet]
        public IActionResult AdminVehicleList()
        {
            var vehiclelist = _bAL_Interface.Adminvehicleview();
            return PartialView("_AdminVehicleView", vehiclelist);
        }
        [HttpGet]
        public IActionResult VehicleId(int vehicleid)
        {
            var editvehicle = _bAL_Interface.VehicleId(vehicleid);
            return View(editvehicle);
        }
        [HttpPost]
        public IActionResult UpdateVehicle(AddVehicleViewPL updatevehicle)
        {
            var vehicledata = new AddVehicleViewBAL()
            {
                VehicleId = updatevehicle.VehicleId,
                VehicleName = updatevehicle.VehicleName,
                VehicleNo = updatevehicle.VehicleNo,
                VehiclePhoto = updatevehicle.VehiclePhoto,
                VehicleType = updatevehicle.VehicleType,
                NoOfVehicle = updatevehicle.NoOfVehicle,
                AdminId = updatevehicle.AdminId,
                VehiclePhotos = updatevehicle.VehiclePhotos,
            };
            _bAL_Interface.UpdateVehicle(vehicledata);
            return Redirect("/Admin/AdminVehicleList");
        }
        public IActionResult DeleteVehicle(int id)
        {
            _bAL_Interface.DeleteVehicle(id);
            return Redirect("/Admin/AdminVehicleList");
        }
        public IActionResult UserList()
        {
            var UserList = _bAL_Interface.UserList();
            List<UserListViewPL> pl = new List<UserListViewPL>();
            foreach (var item in UserList)
            {
                pl.Add(new UserListViewPL()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    MobileNo = item.MobileNo,
                    EmailId = item.EmailId,
                    PhotoName = item.PhotoName,
                });
            }
            return PartialView("_UserList", pl);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _bAL_Interface.UserdeleteAdmin(id);
            return Json(new { msg = "deleted sucessfully" });
        }
        [HttpPost]
        public IActionResult UserDetails(int id)
        {
            var list = _bAL_Interface.userDetails(id);
            return View(list);
        }
        public IActionResult UserCount()
        {
            var usercount = _bAL_Interface.UserCount();
            HttpContext.Session.SetInt32("usercount", usercount);
            return Redirect("/Admin/PaymentCount");
        }
        public IActionResult PaymentCount()
        {
            var paymentcount = _bAL_Interface.PaymentCount();
            HttpContext.Session.SetInt32("paymentcount", paymentcount);
            return Redirect("/Admin/UserBokkingCount");
        }
        public IActionResult UserBokkingCount()
        {
            var bookingcount = _bAL_Interface.UserBookingCount();
            HttpContext.Session.SetInt32("bookingcount", bookingcount);
            return Redirect("/Admin/VehicleCount");
        }
        public IActionResult VehicleCount()
        {
            var vehiclecount = _bAL_Interface.VehicleCount();
            HttpContext.Session.SetInt32("vehiclecount", vehiclecount);
            return Redirect("/Admin/Index");
        }
        [HttpGet]
        public IActionResult bookinglist(int currentPageIndex, string text)
        {
            var list = _bAL_Interface.bookingList(currentPageIndex, text);
            BookingListAdminViewPL bookingListAdmin = new BookingListAdminViewPL();
            List<RentalCarsandBikes.Models.UserList> userLists = new List<RentalCarsandBikes.Models.UserList>();
            foreach (var item in list.userList)
            {
                userLists.Add(new Models.UserList()
                {
                    UserId = item.UserId,
                    BookingId = item.BookingId,
                    Name = item.Name,
                    Email = item.Email,
                    MobileNo = item.MobileNo,
                    DropDate = item.DropDate,
                    PickUpDate = item.PickUpDate,
                    VehicleName = item.VehicleName,
                    VehicleNo = item.VehicleNo,
                });
            }
            bookingListAdmin.userLists = userLists;
            ViewBag.CurrentPageIndex = list.CurrentPageIndex;
            ViewBag.PageCount = list.PageCount;
            return PartialView("_View", bookingListAdmin);
        }
        [HttpGet]
        public IActionResult Transaction()
        {
            var list = _bAL_Interface.paymentlist();
            return PartialView("_PaymentList", list);
        }
    }
}
