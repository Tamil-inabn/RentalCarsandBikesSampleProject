using BAL.Interface;
using BAL.ViewModel;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentalCarsandBikes.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.FormInput;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using DAL.EntityModels;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using System;
using Microsoft.VisualStudio.Services.Account;

namespace RentalCarsandBikes.Controllers
{
    public class UserController : Controller
    {
        public readonly BAL_Interface _bAL_Interface;
        public UserController(BAL_Interface bAL_Interface)
        {
            _bAL_Interface = bAL_Interface;
        }

        public IActionResult Index()
        {
            var uservehicleview = _bAL_Interface.userVehicleViews();
            return View(uservehicleview);
        }
        public IActionResult userbooking(int vehicleid)
        {
            var vehicle = _bAL_Interface.UserBookingView(vehicleid);
            HttpContext.Session.SetInt32("vehicleid", vehicle.VehicleId);
            HttpContext.Session.SetString("vehicleno", vehicle.VehicleNo);
            HttpContext.Session.SetString("vehiclename", vehicle.VehicleName);
            return View(vehicle);
        }
        [HttpPost]
        public IActionResult userbooking(UserBookingViewPL userBookingViewPL)
        {
            var vehiclebooking = new UserBookingViewBAL()
            {
                UserId = HttpContext.Session.GetInt32("id"),
                VehicleName = userBookingViewPL.VehicleName,
                VehicleId = userBookingViewPL.VehicleId,
                VehicleNo = userBookingViewPL.VehicleNo,
                PickUpDate = userBookingViewPL.PickUpDate,
                DropDate = userBookingViewPL.DropDate,
                Name = userBookingViewPL.Name,
                MobileNo = userBookingViewPL.MobileNo,
                Email = userBookingViewPL.Email,
                BookingId = userBookingViewPL.BookingId,
            };
            var bookingdetails = _bAL_Interface.UserBooking(vehiclebooking);

            var username = bookingdetails.Name;
            string Body = "<p>Hi " + username + "</p>" + "<br/>" +
                          "<b>Your Registeration successfull</b> ";
            string FromEmail = "tamilinban.m@colanonline.com";
            string to = bookingdetails.Email;
            string subject = "Suucess Message ";
            MailMessage message = new MailMessage(FromEmail, to, subject, Body);
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.colanonline.com");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("tamilinban.m@colanonline.com", "P(bBHDT1");
            client.Send(message);
            //TempData["UserId"] = bookingdetails.UserId;
            //TempData["BookingId"] = bookingdetails.BookingId;
            //var totalday = bookingdetails.DropDate - bookingdetails.PickUpDate;
            //HttpContext.Session.SetString("TotalDays", totalday.ToString());
            //var Amount = 1500 * totalday;
            //HttpContext.Session.SetString("Amount", Amount.ToString());
            //TempData["Name"] = bookingdetails.Name;
            //TempData["Email"] = bookingdetails.Email;
            //TempData["MobileNo"] = bookingdetails.MobileNo.ToString();           
            HttpContext.Session.SetString("Name", bookingdetails.Name);

            return RedirectToAction("payment", new { id = bookingdetails.BookingId });
        }
        public IActionResult Payment(int id)
        {
            var bookingid = _bAL_Interface.paymentid(id);
            var totalday = bookingid.DropDate - bookingid.PickUpDate;
            HttpContext.Session.SetString("TotalDays", totalday.ToString());
            var Amount = 1500 * totalday;
            HttpContext.Session.SetString("Amount", Amount.ToString());
            return View(bookingid);
        }
        [HttpPost]
        public IActionResult Payment(PaymentViewPL paymentdetails)
        {
            var transaction = new PaymentViewBAL()
            {
                UserId = paymentdetails.UserId,
                BookingId = paymentdetails.BookingId,
                TotalDays = paymentdetails.TotalDays,
                TransactionAmount = paymentdetails.TransactionAmount,
                Name = paymentdetails.Name,
                Email = paymentdetails.Email,
                MobileNo = paymentdetails.MobileNo,
                VehicleName = paymentdetails.VehicleName,
            };
            var list = _bAL_Interface.transactiom(transaction);
            return RedirectToAction("Bill", new { id = list.TransactionId });
        }
        public IActionResult Bill(int id)
        {
            var billdata = _bAL_Interface.bill(id);
            return View(billdata);
        }
        public IActionResult CancelBooking(int id)
        {
            _bAL_Interface.CancelBooking(id);
            return RedirectToAction("CancelReason");
        }
        public IActionResult CancelReason()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OrderCancel(BookingCancelViewPL bookingCancelViewPL)
        {
            var ordercancel = new BookingCancelViewBAL()
            {
                UserId = HttpContext.Session.GetInt32("id"),
                UserName = bookingCancelViewPL.UserName,
                VehicleName = bookingCancelViewPL.VehicleName,
                Reason = bookingCancelViewPL.Reason,
            };
            _bAL_Interface.CancelReason(ordercancel);
            return RedirectToAction("Index");
        }
        public IActionResult ForgotMailcheck()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ForgotMailcheck(string email)
        {
            var mailid = _bAL_Interface.ForgotMail(email);
            if (mailid != null)
            {
                var username = mailid.UserName;
                var encryptuserid = Encrypt(mailid.UserId.ToString());
                var encryptcode = "http://localhost:7870/user/ResetPassword?UserId=" + encryptuserid;
                var decryptcode = Decrypt(encryptuserid);
                string Body = "<p>Hi " + username + "</p>" + "<br/>" +
                              "<b>Click the link to recover your password.</b> " +
                              "<br/>" +
                              " <a href=" + encryptcode + ">Click to reset Password</a>";
                string FromEmail = "tamilinban.m@colanonline.com";
                string to = email;
                string subject = "Password Changed ";
                MailMessage message = new MailMessage(FromEmail, to, subject, Body);
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.colanonline.com");
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("tamilinban.m@colanonline.com", "P(bBHDT1");
                client.Send(message);
                TempData["userid"] = decryptcode;
            }
            return Json(mailid);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            var userid = TempData["userid"].ToString();
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ForgotPasswordViewPL forgotPassword)
        {

            var resetpassword = new ForgotPasswordViewBAL()
            {
                UserId = forgotPassword.UserId,
                Password = forgotPassword.Password,
            };
            _bAL_Interface.ResetPassword(resetpassword);
            return Redirect("/Home/Privacy");
        }
        public static string Encrypt(string mail)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(mail);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    mail = Convert.ToBase64String(ms.ToArray());
                }
            }
            return mail;
        }
        public static string Decrypt(string mail)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(mail);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    mail = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return mail;
        }
        [HttpGet]
        public IActionResult userdata(int userid)
        {
            var userdetails = _bAL_Interface.Userdata(userid);
            ViewBag.state = _bAL_Interface.GetState();
            ViewBag.city = _bAL_Interface.GetCity((int)userdetails.State);
            return View(userdetails);
        }
        [HttpPost]
        public IActionResult UserUpdateData(UserUpdateViewPL userUpdate)
        {
            var userdata = new UserUpdateViewBAL()
            {
                UserId = userUpdate.UserId,
                UserName = userUpdate.UserName,
                Age = userUpdate.Age,
                Gender = userUpdate.Gender,
                MobileNo = userUpdate.MobileNo,
                EmailId = userUpdate.EmailId,
                State = userUpdate.State,
                City = userUpdate.City,
                PhotoName = userUpdate.PhotoName,
                PhotoNames = userUpdate.PhotoNames,
                PhotoId = userUpdate.PhotoId,
                ProofName = userUpdate.ProofName,
                ProofNames = userUpdate.ProofNames,
                IdProof1 = userUpdate.IdProof1,
                RollId = userUpdate.RollId,
                Id = userUpdate.Id,
                Password = userUpdate.Password,
            };
            _bAL_Interface.EditUserData(userdata);
            return RedirectToAction("Index");
        }
    }
}



