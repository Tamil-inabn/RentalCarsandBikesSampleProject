using Azure.Core;
using DAL.DbContexts;
using DAL.EntityModels;
using DAL.Interface;
using DAL.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Users;
using Microsoft.Win32;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace DAL.Repositories
{
    public class DAL_Repositories : DAL_Interface
    {
        public readonly RentalSystemContext _rentalSystem;
        public readonly IHostingEnvironment _enviroment;

        public DAL_Repositories(RentalSystemContext rentalSystem, IHostingEnvironment environment)
        {
            _rentalSystem = rentalSystem;
            _enviroment = environment;
        }

        public AddVehicleViewDAL AddVehicle(AddVehicleViewDAL addVehicleViewDAL)
        {
            string contentrootpath = _enviroment.ContentRootPath;
            string vehiclephotos = Path.GetFileName(addVehicleViewDAL.VehiclePhoto.FileName);
            string path = Path.Combine(contentrootpath, "wwwroot/Vehicle", vehiclephotos);
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream streams = new FileStream(path, FileMode.Create))
            {
                addVehicleViewDAL.VehiclePhoto.CopyTo(streams);
            }
            var adminid = _rentalSystem.Admins.OrderByDescending(m => m.AdminId).First().AdminId;
            VehicleDetail vehicleDetail = new VehicleDetail()
            {
                VehicleName = addVehicleViewDAL.VehicleName,
                AdminId = adminid,
                VehicleNo = addVehicleViewDAL.VehicleNo,
                VehiclePhoto = vehiclephotos,
                VehicleType = addVehicleViewDAL.VehicleType,
                NoOfVehicle = addVehicleViewDAL.NoOfVehicle,
            };
            _rentalSystem.VehicleDetails.Add(vehicleDetail);
            _rentalSystem.SaveChanges();
            var vehicledata = new AddVehicleViewDAL()
            {
                VehicleName = vehicleDetail.VehicleName,
                VehicleId = vehicleDetail.VehicleId,
                NoOfVehicle = vehicleDetail.NoOfVehicle,
                VehicleNo = vehicleDetail.VehicleNo,
                VehicleType = vehicleDetail.VehicleType,
                AdminId = vehicleDetail.AdminId,
            };
            return vehicledata;
        }
        public List<UserVehicleViewDAL> AdminVehicleView()
        {
            var vehicle = _rentalSystem.VehicleDetails.ToList().Where(m => m.IsActive == true).OrderBy(m => m.VehicleId);
            var vehicledata = new List<UserVehicleViewDAL>();
            foreach (var item in vehicle)
            {
                vehicledata.Add(new UserVehicleViewDAL()
                {
                    VehicleId = item.VehicleId,
                    VehicleName = item.VehicleName,
                    VehicleNo = item.VehicleNo,
                    VehiclePhoto = item.VehiclePhoto,
                    VehicleType = item.VehicleType,
                });
            }
            return vehicledata;
        }

        public BookingListAdminViewDAL bookingList(int currentPageIndex, string searchtext)
        {
            var list = GetBookingList(currentPageIndex, searchtext);
            return list;
        }
        private BookingListAdminViewDAL GetBookingList(int currentPage, string searchtext)
        {
            BookingListAdminViewDAL bookingListAdmin = new BookingListAdminViewDAL();
            var rows = 5;
            if (currentPage == 0 && searchtext == null)
            {
                currentPage = 1;
                bookingListAdmin.userLists = (from register in _rentalSystem.UserRegisters
                                              join booking in _rentalSystem.UserBookings on register.UserId equals booking.UserId
                                              where booking.IsActive == true
                                              select new UserList()
                                              {
                                                  UserId = register.UserId,
                                                  BookingId = booking.BookingId,
                                                  Name = booking.Name,
                                                  MobileNo = booking.MobileNo,
                                                  Email = booking.Email,
                                                  PickUpDate = booking.PickUpDate.ToString(),
                                                  DropDate = booking.DropDate.ToString(),
                                                  VehicleName = booking.VehicleName,
                                                  VehicleNo = booking.VehicleNo,
                                                  IsActive = booking.IsActive,
                                              }).OrderBy(m => m.BookingId).Skip((currentPage - 1) * rows).Take(rows).ToList();
                double pagecount = (double)((decimal)_rentalSystem.UserBookings.Where(m => m.IsActive == true).Count() / Convert.ToDecimal(rows));
                bookingListAdmin.PageCount = (int)Math.Ceiling(pagecount);
                bookingListAdmin.CurrentPageIndex = currentPage;
            }
            else if (currentPage != 0)
            {
                bookingListAdmin.userLists = (from register in _rentalSystem.UserRegisters
                                              join booking in _rentalSystem.UserBookings on register.UserId equals booking.UserId
                                              where booking.IsActive == true
                                              select new UserList()
                                              {
                                                  UserId = register.UserId,
                                                  BookingId = booking.BookingId,
                                                  Name = booking.Name,
                                                  MobileNo = booking.MobileNo,
                                                  Email = booking.Email,
                                                  PickUpDate = booking.PickUpDate.ToString(),
                                                  DropDate = booking.DropDate.ToString(),
                                                  VehicleName = booking.VehicleName,
                                                  VehicleNo = booking.VehicleNo,
                                                  IsActive = booking.IsActive,
                                              }).OrderBy(m => m.BookingId).Skip((currentPage - 1) * rows).Take(rows).ToList();
                double pagecount = (double)((decimal)_rentalSystem.UserBookings.Where(m => m.IsActive == true).Count() / Convert.ToDecimal(rows));
                bookingListAdmin.PageCount = (int)Math.Ceiling(pagecount);
                bookingListAdmin.CurrentPageIndex = currentPage;
            }
            else if (searchtext != null)
            {
                bookingListAdmin.userLists = (from register in _rentalSystem.UserRegisters
                                              join booking in _rentalSystem.UserBookings on register.UserId equals booking.UserId
                                              where booking.IsActive == true && booking.Name.StartsWith(searchtext)
                                              select new UserList()
                                              {
                                                  UserId = register.UserId,
                                                  BookingId = booking.BookingId,
                                                  Name = booking.Name,
                                                  MobileNo = booking.MobileNo,
                                                  Email = booking.Email,
                                                  PickUpDate = booking.PickUpDate.ToString(),
                                                  DropDate = booking.DropDate.ToString(),
                                                  VehicleName = booking.VehicleName,
                                                  VehicleNo = booking.VehicleNo,
                                                  IsActive = booking.IsActive,
                                              }).ToList();
                double pagecount = (double)((decimal)_rentalSystem.UserBookings.Where(m => m.IsActive == true && m.Name.StartsWith(searchtext)).Count() / Convert.ToDecimal(rows));
                bookingListAdmin.PageCount = (int)Math.Ceiling(pagecount);
                bookingListAdmin.CurrentPageIndex = currentPage;
            }
            return bookingListAdmin;
        }

        public void CancelBooking(int id)
        {
            if (id != null)
            {
                var bookingid = _rentalSystem.UserBookings.Where(m => m.BookingId == id).FirstOrDefault();
                bookingid.IsActive = false;
                _rentalSystem.UserBookings.Entry(bookingid);
                _rentalSystem.SaveChanges();
            }
        }

        public BookingCancelViewDAL CancelReason(BookingCancelViewDAL bookingCancelViewDAL)
        {
            var cancelreason = new BookingCancel()
            {
                UserId = bookingCancelViewDAL.UserId,
                UserName = bookingCancelViewDAL.UserName,
                VehicleName = bookingCancelViewDAL.VehicleName,
                Reason = bookingCancelViewDAL.Reason,
            };
            _rentalSystem.BookingCancels.Add(cancelreason);
            _rentalSystem.SaveChanges();
            var bookingcancel = new BookingCancelViewDAL()
            {
                UserName = cancelreason.UserName,
                VehicleName = cancelreason.VehicleName,
                UserId = cancelreason.UserId,
                Reason = cancelreason.Reason,
            };
            return bookingcancel;
        }

        public bool CheckMail(string email)
        {
            var mail = _rentalSystem.UserRegisters.Any(m => m.EmailId == email);
            return mail;
        }
        public void Deletevehicle(int id)
        {
            if (id != null)
            {
                var vehicleid = _rentalSystem.VehicleDetails.Where(o => o.VehicleId == id).FirstOrDefault();
                vehicleid.IsActive = false;
                _rentalSystem.VehicleDetails.Entry(vehicleid);
                _rentalSystem.SaveChanges();
            }
        }

        public void EditUserData(UserUpdateViewDAL userList)
        {
            if (userList.PhotoName != null)
            {
                string contentrootpath = _enviroment.ContentRootPath;
                string path = Path.Combine(contentrootpath + "/wwwroot/Photo/", userList.PhotoNames);
                System.IO.File.Delete(path);
                string filephoto = Path.GetFileName(userList.PhotoName.FileName);
                string imagepath = Path.Combine(contentrootpath + "/wwwroot/Photo/", filephoto);
                using (FileStream fileStream = new FileStream(imagepath, FileMode.Create))
                {
                    userList.PhotoName.CopyTo(fileStream);
                }
                UserRegister user = new UserRegister()
                {
                    UserId = userList.UserId,
                    UserName = userList.UserName,
                    Age = userList.Age,
                    Gender = userList.Gender,
                    MobileNo = userList.MobileNo,
                    EmailId = userList.EmailId,
                    State = userList.State,
                    City = userList.City,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    RollId = userList.RollId,
                    Accept = true,
                    Password = userList.Password,
                };
                _rentalSystem.UserRegisters.Update(user);
                _rentalSystem.SaveChanges();
                //
                IdProof idProof = new IdProof()
                {
                    PhotoName = filephoto,
                    PhotoId = userList.PhotoId,
                    ProofName = userList.ProofNames,
                    IdProof1 = userList.IdProof1,
                    UserId = userList.UserId,
                    Id = (int)userList.Id,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };

                _rentalSystem.IdProofs.Update(idProof);
                _rentalSystem.SaveChanges();
            }
            else
            {
                UserRegister userlist = new UserRegister()
                {

                    UserId = userList.UserId,
                    UserName = userList.UserName,
                    Age = userList.Age,
                    Gender = userList.Gender,
                    MobileNo = userList.MobileNo,
                    EmailId = userList.EmailId,
                    State = userList.State,
                    City = userList.City,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    RollId = userList.RollId,
                    Accept = true,
                    Password = userList.Password,
                };
                _rentalSystem.UserRegisters.Update(userlist);
                _rentalSystem.SaveChanges();
                IdProof proof = new IdProof()
                {
                    PhotoName = userList.PhotoNames,
                    PhotoId = userList.PhotoId,
                    ProofName = userList.ProofNames,
                    IdProof1 = userList.IdProof1,
                    UserId = userList.UserId,
                    Id = (int)userList.Id,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsActive = true
                };
                _rentalSystem.IdProofs.Update(proof);
                _rentalSystem.SaveChanges();
            }
        }

        public ForgotPasswordViewDAL ForgotMail(string email)
        {
            var mailid = _rentalSystem.UserRegisters.Where(m => m.EmailId == email).FirstOrDefault();
            if (mailid == null)
            {
                return null;
            }
            var userdetails = new ForgotPasswordViewDAL()
            {
                UserId = mailid.UserId,
                UserName = mailid.UserName,
                Email = mailid.EmailId
            };
            return userdetails;
        }

        public List<StateandCityViewDAL> GetCity(int id)
        {
            var city = _rentalSystem.Cities.Where(m => m.StateId == id).OrderBy(m => m.StateId);
            var citylist = new List<StateandCityViewDAL>();
            foreach (var item in city)
            {
                citylist.Add(new DAL.ViewModel.StateandCityViewDAL
                {
                    CityId = item.CityId,
                    CityName = item.CityName,
                });
            }
            return citylist;
        }

        public List<StateandCityViewDAL> GetState()
        {
            var statelist = _rentalSystem.States.Select(x=> new StateandCityViewDAL()
            {
                StateId = x.StateId,
                StateName = x.StateName,
            }).ToList();

            return statelist;
        }
        public LoginCheckViewDAL LoginCheck(LoginCheckViewDAL loginCheck)
        {
            var logindata = _rentalSystem.UserRegisters.Where(m => m.EmailId == loginCheck.EmailId && m.Password == loginCheck.Password).FirstOrDefault();
            if (logindata == null)
            {
                return null;
            }
            var loginlist = new LoginCheckViewDAL()
            {
                UserId = logindata.UserId,
                EmailId = logindata.EmailId,
                Password = logindata.Password,
                UserName = logindata.UserName,
                RollId = logindata.RollId,
            };
            return loginlist;
        }

        public void ResetPassword(ForgotPasswordViewDAL forgotPassword)
        {
            var resetpassowrd = _rentalSystem.UserRegisters.Where(m => m.UserId == forgotPassword.UserId).FirstOrDefault();
            resetpassowrd.Password = forgotPassword.Password;
            _rentalSystem.UserRegisters.Update(resetpassowrd);
            _rentalSystem.SaveChanges();
        }

        public void UpdateVehicle(AddVehicleViewDAL updatevehicle)
        {
            if (updatevehicle.VehiclePhoto != null)
            {
                string contentrootpath = _enviroment.ContentRootPath;
                string path = Path.Combine(contentrootpath + "/wwwroot/Vehicle/", updatevehicle.VehiclePhotos);
                System.IO.File.Delete(path);
                string filephoto = Path.GetFileName(updatevehicle.VehiclePhoto.FileName);
                string imagepath = Path.Combine(contentrootpath + "/wwwroot/Vehicle/", filephoto);
                using (FileStream fileStream = new FileStream(imagepath, FileMode.Create))
                {
                    updatevehicle.VehiclePhoto.CopyTo(fileStream);
                }
                VehicleDetail vehicleDetail = new VehicleDetail()
                {
                    VehicleId = updatevehicle.VehicleId,
                    AdminId = updatevehicle.AdminId,
                    VehiclePhoto = filephoto,
                    VehicleName = updatevehicle.VehicleName,
                    VehicleNo = updatevehicle.VehicleNo,
                    VehicleType = updatevehicle.VehicleType,
                    NoOfVehicle = updatevehicle.NoOfVehicle,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };
                _rentalSystem.VehicleDetails.Update(vehicleDetail);
                _rentalSystem.SaveChanges();
            }
            else
            {
                VehicleDetail vehicleDetaillist = new VehicleDetail()
                {
                    VehicleId = updatevehicle.VehicleId,
                    VehicleName = updatevehicle.VehicleName,
                    VehicleNo = updatevehicle.VehicleNo,
                    VehicleType = updatevehicle.VehicleType,
                    VehiclePhoto = updatevehicle.VehiclePhotos,
                    AdminId = updatevehicle.AdminId,
                    NoOfVehicle = updatevehicle.NoOfVehicle,
                    IsActive = true,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                };
                _rentalSystem.VehicleDetails.Update(vehicleDetaillist);
                _rentalSystem.SaveChanges();
            }
        }

        public UserBookingViewDAL UserBooking(UserBookingViewDAL userBookingViewDAL)
        {
            var bookingdata = new UserBooking()
            {
                UserId = userBookingViewDAL.UserId,
                VehicleName = userBookingViewDAL.VehicleName,
                VehicleId = userBookingViewDAL.VehicleId,
                VehicleNo = userBookingViewDAL.VehicleNo,
                DropDate = userBookingViewDAL.DropDate,
                PickUpDate = userBookingViewDAL.PickUpDate,
                Name = userBookingViewDAL.Name,
                MobileNo = userBookingViewDAL.MobileNo,
                Email = userBookingViewDAL.Email,
            };
            _rentalSystem.Add(bookingdata);
            _rentalSystem.SaveChanges();

            //var noVehicle = _rentalSystem.VehicleDetails.Where(m => m.VehicleId == userBookingViewDAL.VehicleId).FirstOrDefault();
            //userBookingViewDAL.VehicleId = noVehicle.VehicleId--;
            //_rentalSystem.VehicleDetails.Update(noVehicle);
            //_rentalSystem.SaveChanges();

            var bookingid = _rentalSystem.UserBookings.OrderByDescending(m => m.BookingId).First().BookingId;
            var bookingdetails = new UserBookingViewDAL()
            {
                UserId = bookingdata.UserId,
                Name = bookingdata.Name,
                MobileNo = bookingdata.MobileNo,
                Email = bookingdata.Email,
                DropDate = bookingdata.DropDate,
                PickUpDate = bookingdata.PickUpDate,
                BookingId = bookingdata.BookingId,
                VehicleId = bookingdata.VehicleId,
                VehicleName = bookingdata.VehicleName,
                VehicleNo = bookingdata.VehicleNo,
            };
            return bookingdetails;
        }

        public int UserBookingCount()
        {
            var userbookingcount = (from boookingcount in _rentalSystem.UserBookings
                                    where boookingcount.IsActive == true
                                    select boookingcount.BookingId).Count();
            return userbookingcount;
        }

        public UserVehicleViewDAL UserBookingView(int id)
        {
            var vehicleid = _rentalSystem.VehicleDetails.Find(id);

            var vehicledata = new UserVehicleViewDAL()
            {
                VehicleName = vehicleid.VehicleName,
                VehicleId = vehicleid.VehicleId,
                VehicleNo = vehicleid.VehicleNo,
            };
            return vehicledata;
        }

        public int UserCount()
        {
            var usercount = (from register in _rentalSystem.UserRegisters
                             where register.IsActive == true
                             select register.UserId).Count();
            return usercount;
        }

        public UserListViewDAL Userdata(int id)
        {
            var userdetails = (from register in _rentalSystem.UserRegisters
                               where (register.UserId == id)
                               join proof in _rentalSystem.IdProofs on register.UserId equals proof.UserId
                               join state in _rentalSystem.States on register.State equals state.StateId
                               join city in _rentalSystem.Cities on register.City equals city.CityId

                               select new UserListViewDAL()
                               {
                                   UserId = register.UserId,
                                   UserName = register.UserName,
                                   Age = register.Age,
                                   MobileNo = register.MobileNo,
                                   EmailId = register.EmailId,
                                   State = register.State,
                                   StateName = state.StateName,
                                   City = register.City,
                                   CityName = city.CityName,
                                   Gender = register.Gender,
                                   PhotoId = proof.PhotoId,
                                   PhotoName = proof.PhotoName,
                                   PhotoNames = proof.PhotoName,
                                   IdProof1 = proof.IdProof1,
                                   ProofName = proof.ProofName,
                                   ProofNames = proof.ProofName,
                                   RollId = register.RollId,
                                   IsActive = register.IsActive,
                                   Id = proof.Id,
                                   Password = register.Password
                               }).FirstOrDefault();
            return userdetails;
        }

        public void UserdeleteAdmin(int id)
        {
            var delete = _rentalSystem.UserRegisters.Where(m => m.UserId == id).FirstOrDefault();
            delete.IsActive = false;
            _rentalSystem.UserRegisters.Entry(delete);
            _rentalSystem.SaveChanges();
        }

        public UserDetailsAdminViewDAL userDetails(int id)
        {
            var details = (from register in _rentalSystem.UserRegisters
                           where (register.UserId == id)
                           join proof in _rentalSystem.IdProofs on register.UserId equals proof.UserId
                           join State in _rentalSystem.States on register.State equals State.StateId
                           join city in _rentalSystem.Cities on register.City equals city.CityId
                           join userbooking in _rentalSystem.UserBookings on register.UserId equals userbooking.UserId
                           select new UserDetailsAdminViewDAL()
                           {
                               UserId = register.UserId,
                               UserName = register.UserName,
                               Age = register.Age,
                               Gender = register.Gender,
                               StateName = State.StateName,
                               CityName = city.CityName,
                               MobileNo = register.MobileNo,
                               EmailId = register.EmailId,
                               PhotoName = proof.PhotoName,
                               ProofName = proof.ProofName,
                               Name = userbooking.Name,
                               Email = userbooking.Email,
                               MobileNum = userbooking.MobileNo,
                               BookingId = userbooking.BookingId,
                               VehicleId = userbooking.VehicleId,
                               VehicleName = userbooking.VehicleName,
                               VehicleNo = userbooking.VehicleNo,
                               PickUpDate = userbooking.PickUpDate.ToString(),
                               DropDate = userbooking.DropDate.ToString(),
                           }).FirstOrDefault();
            return details;
        }

        public List<UserListViewDAL> UserList()
        {
            var UserList = (from register in _rentalSystem.UserRegisters
                            where (register.IsActive == true)
                            join proof in _rentalSystem.IdProofs on register.UserId equals proof.UserId
                            join state in _rentalSystem.States on register.State equals state.StateId
                            join city in _rentalSystem.Cities on register.City equals city.CityId
                            select new UserListViewDAL
                            {
                                UserId = register.UserId,
                                UserName = register.UserName,
                                Age = register.Age,
                                Gender = register.Gender,
                                State = register.State,
                                StateName = state.StateName,
                                City = register.City,
                                CityName = city.CityName,
                                MobileNo = register.MobileNo,
                                EmailId = register.EmailId,
                                IdProof1 = proof.IdProof1,
                                PhotoId = proof.PhotoId,
                                PhotoName = proof.PhotoName,
                                ProofName = proof.ProofName,
                            }).ToList();
            return UserList;
        }
        public void UserRegister(RegisterViewDAL registerViewDAL)
        {
            string conntentrootpath = _enviroment.ContentRootPath;
            string photoname = Path.GetFileName(registerViewDAL.PhotoName.FileName);
            string photoid = Guid.NewGuid() + Path.GetExtension(registerViewDAL.PhotoName.FileName);
            string path = Path.Combine(conntentrootpath, "wwwroot/Photo", photoname);
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                registerViewDAL.PhotoName.CopyTo(file);
            }
            UserRegister userdata = new UserRegister()
            {
                UserName = registerViewDAL.UserName,
                Gender = registerViewDAL.Gender,
                Age = registerViewDAL.Age,
                State = registerViewDAL.State,
                City = registerViewDAL.City,
                MobileNo = registerViewDAL.MobileNo,
                EmailId = registerViewDAL.EmailId,
                Password = registerViewDAL.Password,
                CreatedBy = registerViewDAL.UserName,
                Accept = registerViewDAL.Accept
            };
            _rentalSystem.UserRegisters.Add(userdata);
            _rentalSystem.SaveChanges();
            //
            string Conternrootpath = _enviroment.ContentRootPath;
            string proofname = Path.GetFileName(registerViewDAL.ProofName.FileName);
            string proofid = Guid.NewGuid() + Path.GetExtension(registerViewDAL.ProofName.FileName);
            string pathproof = Path.Combine(Conternrootpath, "wwwroot/Idproof", proofname);
            if (Directory.Exists(pathproof))
            {
                Directory.CreateDirectory(pathproof);
            }
            using (FileStream stream = new FileStream(pathproof, FileMode.Create))
            {
                registerViewDAL.ProofName.CopyTo(stream);
            }
            var userid = _rentalSystem.UserRegisters.OrderByDescending(m => m.UserId).First().UserId;
            IdProof idProof = new IdProof()
            {
                PhotoId = photoid,
                PhotoName = photoname,
                ProofName = proofname,
                IdProof1 = proofid,
                UserId = userid,
            };
            _rentalSystem.IdProofs.Add(idProof);
            _rentalSystem.SaveChanges();
        }

        public List<UserVehicleViewDAL> userVehicleViews()
        {
            var uservehicleview = _rentalSystem.VehicleDetails.Where(m => m.IsActive == true).ToList();
            var vehicledata = new List<UserVehicleViewDAL>();
            foreach (var item in uservehicleview)
            {
                vehicledata.Add(new UserVehicleViewDAL()
                {
                    VehicleId = item.VehicleId,
                    VehicleName = item.VehicleName,
                    VehicleNo = item.VehicleNo,
                    VehiclePhoto = item.VehiclePhoto,
                    VehicleType = item.VehicleType,
                    NoOfVehicle = item.NoOfVehicle,
                });
            }
            return vehicledata;
        }

        public int VehicleCount()
        {
            var vehiclecount = (from vechicle in _rentalSystem.VehicleDetails
                                where vechicle.IsActive == true
                                select vechicle.VehicleId).Count();
            return vehiclecount;
        }

        public UserVehicleViewDAL VehicleId(int id)
        {
            var editvehicle = _rentalSystem.VehicleDetails.Where(m => m.VehicleId == id).FirstOrDefault();
            if (editvehicle == null)
            {
                return null;
            }
            var editvehicledata = new UserVehicleViewDAL()
            {
                VehicleId = editvehicle.VehicleId,
                VehicleName = editvehicle.VehicleName,
                VehicleNo = editvehicle.VehicleNo,
                VehiclePhoto = editvehicle.VehiclePhoto,
                VehicleType = editvehicle.VehicleType,
                NoOfVehicle = editvehicle.NoOfVehicle,
                AdminId = editvehicle.AdminId,
                VehiclePhotos = editvehicle.VehiclePhoto
            };
            return editvehicledata;
        }

        public PaymentViewDAL paymentid(int id)
        {
            var bookingdetails = _rentalSystem.UserBookings.Where(m => m.BookingId == id).FirstOrDefault();
            var data = new PaymentViewDAL()
            {
                BookingId = bookingdetails.BookingId,
                Name = bookingdetails.Name,
                MobileNo = bookingdetails.MobileNo,
                Email = bookingdetails.Email,
                VehicleName = bookingdetails.VehicleName,
                UserId = (int)bookingdetails.UserId,
                PickUpDate = bookingdetails.PickUpDate,
                DropDate = bookingdetails.DropDate
            };
            return data;
        }

        public PaymentViewDAL transactiom(PaymentViewDAL payment)
        {
            var data = new MoneyTransaction()
            {
                TransactionAmount = payment.TransactionAmount,
                BookingId = payment.BookingId,
                UserName = payment.Name,
                TotalDays = payment.TotalDays,
                UserId = payment.UserId,
                VehicleName = payment.VehicleName,
                CreatedBy = payment.UserId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
            };

            _rentalSystem.MoneyTransactions.Add(data);
            _rentalSystem.SaveChanges();

            var usrtlist = new PaymentViewDAL()
            {
                TotalDays = data.TotalDays.ToString(),
                UserId = (int)data.UserId,
                TransactionAmount = data.UserName,
                BookingId = data.BookingId,
                VehicleName = data.VehicleName,
                Name = data.UserName,
                TransactionId = data.TransactionId,
            };
            return usrtlist;
        }

        public BillViewDAL bill(int id)
        {
            var data = _rentalSystem.MoneyTransactions.Where(m => m.TransactionId == id).FirstOrDefault();
            var list = new BillViewDAL()
            {
                TransactionId = data.TransactionId,
                Name = data.UserName,
                TotalDays = data.TotalDays,
                TransactionAmount = data.TransactionAmount,
                BookingId = data.BookingId,
                VehicleName = data.VehicleName,
            };
            return list;
        }

        public List<MoneyTransactionListViewDAL> paymentlist()
        {
            var datalist = (from money in _rentalSystem.MoneyTransactions
                            where money.IsActive == true
                            select new MoneyTransactionListViewDAL()
                            {
                                TotalDays = money.TotalDays,
                                TransactionAmount = money.TransactionAmount,
                                TransactionId = money.TransactionId,
                                Name = money.UserName,
                                UserId = money.UserId,
                                BookingId = money.BookingId,
                                VehicleName = money.VehicleName,
                            }).ToList();
            return datalist;
        }

        public int PaymentCount()
        {
            var usercount = (from register in _rentalSystem.MoneyTransactions
                             where register.IsActive == true
                             select register.UserId).Count();
            return usercount;
        }
    }
}

