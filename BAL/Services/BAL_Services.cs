using BAL.Interface;
using BAL.ViewModel;
using DAL.Interface;
using DAL.Repositories;
using DAL.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.TeamFoundation.Dashboards.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.UserMapping;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class BAL_Services : BAL_Interface
    {
        public readonly DAL_Interface _dAL_Interface;
        public BAL_Services(DAL_Interface dAL_Interface)
        {
            _dAL_Interface = dAL_Interface;
        }

        public AddVehicleViewBAL AddVehicle(AddVehicleViewBAL addVehicleViewBAL)
        {
            var vehiclelist = new AddVehicleViewDAL()
            {
                VehicleNo = addVehicleViewBAL.VehicleNo,
                VehicleType = addVehicleViewBAL.VehicleType,
                VehiclePhoto = addVehicleViewBAL.VehiclePhoto,
                VehicleName = addVehicleViewBAL.VehicleName,
                NoOfVehicle = addVehicleViewBAL.NoOfVehicle,
            };
            var details = _dAL_Interface.AddVehicle(vehiclelist);
            var vehicledata = new AddVehicleViewBAL()
            {
                AdminId = details.AdminId,
                NoOfVehicle = details.NoOfVehicle,
                VehicleType = details.VehicleType,
                VehicleId = details.VehicleId,
                VehicleName = details.VehicleName,
                VehicleNo = details.VehicleNo,
            };
            return vehicledata;
        }

        public List<UserVehicleViewBAL> Adminvehicleview()
        {
            var vehiclelist = _dAL_Interface.AdminVehicleView();
            var vehicledata = new List<UserVehicleViewBAL>();
            foreach (var item in vehiclelist)
            {
                vehicledata.Add(new UserVehicleViewBAL()
                {
                    VehicleNo = item.VehicleNo,
                    VehicleType = item.VehicleType,
                    VehiclePhoto = item.VehiclePhoto,
                    VehicleName = item.VehicleName,
                    VehicleId = item.VehicleId,
                });
            }
            return vehicledata;
        }

        public BillViewBAL bill(int id)
        {
            var data = _dAL_Interface.bill(id);
            var list = new BillViewBAL()
            {
                TransactionAmount = data.TransactionAmount,
                Name = data.Name,
                VehicleName = data.VehicleName,
                TotalDays = data.TotalDays,
                BookingId = data.BookingId,
                TransactionId = data.TransactionId
            };
            return list;
        }

        public BookingListAdminViewBAL bookingList(int currentPageIndex, string searchtext)
        {
            var data = _dAL_Interface.bookingList(currentPageIndex, searchtext);
            BookingListAdminViewBAL bookingListAdmin = new BookingListAdminViewBAL();
            List<BAL.ViewModel.UserList> list = new List<BAL.ViewModel.UserList>();
            foreach (var item in data.userLists)
            {
                list.Add(new ViewModel.UserList()
                {
                    UserId = item.UserId,
                    MobileNo = item.MobileNo,
                    Email = item.Email,
                    Name = item.Name,
                    BookingId = item.BookingId,
                    DropDate = item.DropDate,
                    PickUpDate = item.PickUpDate,
                    VehicleName = item.VehicleName,
                    VehicleNo = item.VehicleNo,
                    IsActive = item.IsActive,
                });
            }
            bookingListAdmin.PageCount = data.PageCount;
            bookingListAdmin.CurrentPageIndex = data.CurrentPageIndex;
            bookingListAdmin.userList = list;
            return bookingListAdmin;
        }

        public void CancelBooking(int id)
        {
            _dAL_Interface.CancelBooking(id);
        }

        public BookingCancelViewBAL CancelReason(BookingCancelViewBAL bookingCancelViewBAL)
        {
            var bookingcancel = new BookingCancelViewDAL()
            {
                VehicleName = bookingCancelViewBAL.VehicleName,
                Reason = bookingCancelViewBAL.Reason,
                UserId = bookingCancelViewBAL.UserId,
                UserName = bookingCancelViewBAL.UserName,
            };
            var reason = _dAL_Interface.CancelReason(bookingcancel);
            var canceldetails = new BookingCancelViewBAL()
            {
                VehicleName = reason.VehicleName,
                Reason = reason.Reason,
                UserName = reason.UserName,
                UserId = reason.UserId,
            };
            return canceldetails;
        }

        public bool CheckMail(string email)
        {
            var mail = _dAL_Interface.CheckMail(email);
            return mail;
        }

        public void DeleteVehicle(int id)
        {
            _dAL_Interface.Deletevehicle(id);
        }

        public void EditUserData(UserUpdateViewBAL userList)
        {
            var userdata = new UserUpdateViewDAL()
            {
                UserId = userList.UserId,
                UserName = userList.UserName,
                Age = userList.Age,
                Gender = userList.Gender,
                MobileNo = userList.MobileNo,
                EmailId = userList.EmailId,
                State = userList.State,
                City = userList.City,
                PhotoName = userList.PhotoName,
                PhotoNames = userList.PhotoNames,
                PhotoId = userList.PhotoId,
                ProofName = userList.ProofName,
                ProofNames = userList.ProofNames,
                IdProof1 = userList.IdProof1,
                RollId = userList.RollId,
                Id = userList.Id,
                Password = userList.Password,
            };
            _dAL_Interface.EditUserData(userdata);
        }

        public ForgotPasswordViewBAL ForgotMail(string email)
        {
            var mailid = _dAL_Interface.ForgotMail(email);
            if (mailid == null)
            {
                return null;
            }
            var userdetail = new ForgotPasswordViewBAL()
            {
                UserId = mailid.UserId,
                UserName = mailid.UserName,
                Email = mailid.Email,
            };
            return userdetail;
        }

        public List<StateandCityViewBAL> GetCity(int id)
        {
            var city = _dAL_Interface.GetCity(id);
            var citylist = new List<StateandCityViewBAL>();
            foreach (var item in city)
            {
                citylist.Add(new BAL.ViewModel.StateandCityViewBAL
                {
                    CityId = item.CityId,
                    CityName = item.CityName,
                });
            }
            return citylist;
        }

        public List<StateandCityViewBAL> GetState()
        {
            var state = _dAL_Interface.GetState().ToList();
            var statelist = new List<StateandCityViewBAL>();
            foreach (var item in state)
            {
                statelist.Add(new BAL.ViewModel.StateandCityViewBAL
                {
                    StateId = item.StateId,
                    StateName = item.StateName,
                });
            }
            return statelist;
        }

        public LoginCheckViewBAL LoginCheck(LoginCheckViewBAL loginCheckViewBAL)
        {
            var loginid = new LoginCheckViewDAL()
            {
                EmailId = loginCheckViewBAL.EmailId,
                Password = loginCheckViewBAL.Password,
            };
            var logindata = _dAL_Interface.LoginCheck(loginid);
            if (logindata == null)
            {
                return null;
            }
            var login = new LoginCheckViewBAL()
            {
                EmailId = logindata.EmailId,
                Password = logindata.Password,
                UserName = logindata.UserName,
                RollId = logindata.RollId,
                UserId = logindata.UserId
            };
            return login;
        }

        public int PaymentCount()
        {
            var count = _dAL_Interface.PaymentCount();
            return count;
        }

        public PaymentViewBAL paymentid(int id)
        {
            var bookingdetails = _dAL_Interface.paymentid(id);
            var data = new PaymentViewBAL()
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

        public List<MoneyTransactionListViewBAL> paymentlist()
        {
            var list = _dAL_Interface.paymentlist();
            var datalist = new List<MoneyTransactionListViewBAL>();
            foreach (var item in list)
            {
                datalist.Add(new MoneyTransactionListViewBAL()
                {
                    UserId = item.UserId,
                    BookingId = item.BookingId,
                    TransactionAmount = item.TransactionAmount,
                    TotalDays = item.TotalDays,
                    TransactionId = item.TransactionId,
                    Name = item.Name,
                    VehicleName = item.VehicleName
                });
            }
            return datalist;
        }

        public void ResetPassword(ForgotPasswordViewBAL forgotPassword)
        {
            var resetpassword = new ForgotPasswordViewDAL()
            {
                UserId = forgotPassword.UserId,
                Password = forgotPassword.Password,
            };
            _dAL_Interface.ResetPassword(resetpassword);
        }

        public PaymentViewBAL transactiom(PaymentViewBAL payment)
        {
            var transaction = new PaymentViewDAL()
            {
                UserId = payment.UserId,
                BookingId = payment.BookingId,
                Name = payment.Name,
                MobileNo = payment.MobileNo,
                Email = payment.Email,
                TotalDays = payment.TotalDays,
                TransactionAmount = payment.TransactionAmount,
                VehicleName = payment.VehicleName,
            };
            var data = _dAL_Interface.transactiom(transaction);
            var transactions = new PaymentViewBAL()
            {
                UserId = data.UserId,
                BookingId = data.BookingId,
                Name = data.Name,
                MobileNo = data.MobileNo,
                Email = data.Email,
                TotalDays = data.TotalDays,
                TransactionAmount = data.TransactionAmount,
                VehicleName = data.VehicleName,
                TransactionId = data.TransactionId
            };
            return transactions;
        }

        public void UpdateVehicle(AddVehicleViewBAL updatevehicle)
        {
            var vehicle = new AddVehicleViewDAL()
            {
                VehicleId = updatevehicle.VehicleId,
                VehicleType = updatevehicle.VehicleType,
                VehiclePhoto = updatevehicle.VehiclePhoto,
                VehicleName = updatevehicle.VehicleName,
                VehicleNo = updatevehicle.VehicleNo,
                NoOfVehicle = updatevehicle.NoOfVehicle,
                AdminId = updatevehicle.AdminId,
                VehiclePhotos = updatevehicle.VehiclePhotos,
            };
            _dAL_Interface.UpdateVehicle(vehicle);
        }

        public UserBookingViewBAL UserBooking(UserBookingViewBAL userBookingViewBAL)
        {
            var bookinglist = new UserBookingViewDAL()
            {
                UserId = userBookingViewBAL.UserId,
                VehicleName = userBookingViewBAL.VehicleName,
                VehicleId = userBookingViewBAL.VehicleId,
                VehicleNo = userBookingViewBAL.VehicleNo,
                Name = userBookingViewBAL.Name,
                MobileNo = userBookingViewBAL.MobileNo,
                DropDate = userBookingViewBAL.DropDate,
                PickUpDate = userBookingViewBAL.PickUpDate,
                Email = userBookingViewBAL.Email,
                BookingId = userBookingViewBAL.BookingId,
            };

            var bookingdetails = _dAL_Interface.UserBooking(bookinglist);
            var bookingdata = new UserBookingViewBAL()
            {
                BookingId = bookingdetails.BookingId,
                UserId = bookingdetails.UserId,
                VehicleId = bookingdetails.VehicleId,
                VehicleName = bookingdetails.VehicleName,
                VehicleNo = bookingdetails.VehicleNo,
                DropDate = bookingdetails.DropDate,
                PickUpDate = bookingdetails.PickUpDate,
                Email = bookingdetails.Email,
                MobileNo = bookingdetails.MobileNo,
                Name = bookingdetails.Name,
            };
            return bookingdata;
        }

        public int UserBookingCount()
        {
            var bookingcount = _dAL_Interface.UserBookingCount();
            return bookingcount;
        }

        public UserVehicleViewBAL UserBookingView(int id)
        {
            var vehicleid = _dAL_Interface.UserBookingView(id);
            var vehicledata = new UserVehicleViewBAL()
            {
                VehicleName = vehicleid.VehicleName,
                VehicleId = vehicleid.VehicleId,
                VehicleNo = vehicleid.VehicleNo,
            };
            return vehicledata;
        }

        public int UserCount()
        {
            var usercount = _dAL_Interface.UserCount();
            return usercount;
        }

        public UserListViewBAL Userdata(int id)
        {
            var userdetails = _dAL_Interface.Userdata(id);
            var userdata = new UserListViewBAL()
            {
                UserId = userdetails.UserId,
                UserName = userdetails.UserName,
                Age = userdetails.Age,
                Gender = userdetails.Gender,
                State = userdetails.State,
                StateName = userdetails.StateName,
                City = userdetails.City,
                CityName = userdetails.CityName,
                EmailId = userdetails.EmailId,
                MobileNo = userdetails.MobileNo,
                PhotoId = userdetails.PhotoId.ToString(),
                PhotoName = userdetails.PhotoName,
                PhotoNames = userdetails.PhotoNames,
                IdProof1 = userdetails.IdProof1.ToString(),
                ProofName = userdetails.ProofName,
                ProofNames = userdetails.ProofNames,
                RollId = userdetails.RollId,
                Id = userdetails.Id,
                IsActive = userdetails.IsActive,
                Password = userdetails.Password,
            };
            return userdata;
        }

        public void UserdeleteAdmin(int id)
        {
            _dAL_Interface.UserdeleteAdmin(id);
        }

        public UserDetailsAdminViewBAL userDetails(int id)
        {
            var data = _dAL_Interface.userDetails(id);
            var userdata = new UserDetailsAdminViewBAL()
            {
                UserId = data.UserId,
                UserName = data.UserName,
                Age = data.Age,
                Gender = data.Gender,
                StateName = data.StateName,
                CityName = data.CityName,
                EmailId = data.EmailId,
                MobileNo = data.MobileNo,
                PhotoName = data.PhotoName,
                ProofName = data.ProofName,
                BookingId = data.BookingId,
                Name = data.Name,
                Email = data.Email,
                MobileNum = data.MobileNum,
                VehicleId = data.VehicleId,
                VehicleName = data.VehicleName,
                VehicleNo = data.VehicleNo,
                PickUpDate = data.PickUpDate,
                DropDate = data.DropDate,
            };
            return userdata;
        }

        public List<UserListViewBAL> UserList()
        {
            var UserList = _dAL_Interface.UserList();
            var Userdata = new List<UserListViewBAL>();
            foreach (var item in UserList)
            {
                Userdata.Add(new UserListViewBAL()
                {
                    UserId = item.UserId,
                    UserName = item.UserName,
                    Age = item.Age,
                    Gender = item.Gender,
                    State = item.State,
                    StateName = item.StateName,
                    City = item.City,
                    CityName = item.CityName,
                    EmailId = item.EmailId,
                    MobileNo = item.MobileNo,
                    IdProof1 = item.IdProof1,
                    ProofName = item.ProofName,
                    PhotoId = item.PhotoId,
                    PhotoName = item.PhotoName,
                });
            }
            return Userdata;
        }
        public void UserRegister(RegisterViewBAL registerViewBAL)
        {
            var Userregister = new RegisterViewDAL()
            {
                UserName = registerViewBAL.UserName,
                Gender = registerViewBAL.Gender,
                Age = registerViewBAL.Age,
                State = registerViewBAL.State,
                City = registerViewBAL.City,
                MobileNo = registerViewBAL.MobileNo,
                EmailId = registerViewBAL.EmailId,
                Password = registerViewBAL.Password,
                PhotoId = registerViewBAL.PhotoId,
                PhotoName = registerViewBAL.PhotoName,
                IdProof1 = registerViewBAL.IdProof1,
                ProofName = registerViewBAL.ProofName,
                Accept = registerViewBAL.Accept,
            };
            _dAL_Interface.UserRegister(Userregister);
        }

        public List<UserVehicleViewBAL> userVehicleViews()
        {
            var uservehicleview = _dAL_Interface.userVehicleViews().ToList();
            var vehicledata = new List<UserVehicleViewBAL>();
            foreach (var item in uservehicleview)
            {
                vehicledata.Add(new UserVehicleViewBAL()
                {
                    VehicleId = item.VehicleId,
                    VehicleName = item.VehicleName,
                    VehicleNo = item.VehicleNo,
                    VehiclePhoto = item.VehiclePhoto,
                    VehicleType = item.VehicleType,
                    NoOfVehicle = item.NoOfVehicle,
                });
            };
            return vehicledata;
        }

        public int VehicleCount()
        {
            var vehiclecount = _dAL_Interface.VehicleCount();
            return vehiclecount;
        }

        public UserVehicleViewBAL VehicleId(int id)
        {
            var editvehicle = _dAL_Interface.VehicleId(id);
            if (editvehicle == null)
            {
                return null;
            }
            var vehicledata = new UserVehicleViewBAL()
            {
                VehicleId = editvehicle.VehicleId,
                VehicleName = editvehicle.VehicleName,
                VehicleNo = editvehicle.VehicleNo,
                VehiclePhoto = editvehicle.VehiclePhoto,
                VehicleType = editvehicle.VehicleType,
                NoOfVehicle = editvehicle.NoOfVehicle,
                AdminId = editvehicle.AdminId,
                VehiclePhotos = editvehicle.VehiclePhotos,
            };
            return vehicledata;
        }
    }
}
