using BAL.ViewModel;
using DAL.EntityModels;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface BAL_Interface
    {
        public void UserRegister(RegisterViewBAL registerViewBAL);
        public List<StateandCityViewBAL> GetState();
        public List<StateandCityViewBAL> GetCity(int id);
        public LoginCheckViewBAL LoginCheck(LoginCheckViewBAL loginCheckViewBAL);
        public bool CheckMail(string email);
        public AddVehicleViewBAL AddVehicle(AddVehicleViewBAL addVehicleViewBAL);
        public List<UserVehicleViewBAL> userVehicleViews();
        public List<UserVehicleViewBAL> Adminvehicleview();
        public UserVehicleViewBAL VehicleId(int id);
        public void DeleteVehicle(int id);
        public void UserdeleteAdmin(int id);
        public UserBookingViewBAL UserBooking(UserBookingViewBAL userBookingViewBAL);
        public UserVehicleViewBAL UserBookingView(int id);
        public List<UserListViewBAL> UserList();
        public List<MoneyTransactionListViewBAL> paymentlist();
        public void CancelBooking(int id);
        public BookingCancelViewBAL CancelReason(BookingCancelViewBAL bookingCancelViewBAL);
        public ForgotPasswordViewBAL ForgotMail(string email);
        public void ResetPassword(ForgotPasswordViewBAL forgotPassword);
        public UserListViewBAL Userdata(int id);
        public void EditUserData(UserUpdateViewBAL userList);
        public void UpdateVehicle(AddVehicleViewBAL updatevehicle);
        public UserDetailsAdminViewBAL userDetails(int id);
        public int UserCount();
        public int PaymentCount();
        public int UserBookingCount();
        public int VehicleCount();
        public BookingListAdminViewBAL bookingList(int currentPageIndex, string searchtext);
        public PaymentViewBAL paymentid(int id);
        public PaymentViewBAL transactiom(PaymentViewBAL payment);
        public BillViewBAL bill(int id);
    }
}
