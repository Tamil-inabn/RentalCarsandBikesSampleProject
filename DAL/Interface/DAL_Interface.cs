using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface DAL_Interface
    {
        public void UserRegister(RegisterViewDAL registerViewDAL);
        public List<StateandCityViewDAL> GetState();
        public List<StateandCityViewDAL> GetCity(int id);
        public LoginCheckViewDAL LoginCheck(LoginCheckViewDAL loginCheck);
        public bool CheckMail(string email);
        public AddVehicleViewDAL AddVehicle(AddVehicleViewDAL addVehicleViewDAL);
        public List<UserVehicleViewDAL> userVehicleViews();
        public List<UserVehicleViewDAL> AdminVehicleView();
        public UserVehicleViewDAL VehicleId(int id);
        public void Deletevehicle(int id);
        public void UserdeleteAdmin(int id);
        public UserBookingViewDAL UserBooking(UserBookingViewDAL userBookingViewDAL);
        public UserVehicleViewDAL UserBookingView(int id);
        public List<UserListViewDAL> UserList();
        public List<MoneyTransactionListViewDAL> paymentlist();
        public void CancelBooking(int id);
        public BookingCancelViewDAL CancelReason(BookingCancelViewDAL bookingCancelViewDAL);
        public ForgotPasswordViewDAL ForgotMail(string email);
        public void ResetPassword(ForgotPasswordViewDAL forgotPassword);
        public UserListViewDAL Userdata(int id);
        public void EditUserData(UserUpdateViewDAL userList);
        public void UpdateVehicle(AddVehicleViewDAL updatevehicle);
        public UserDetailsAdminViewDAL userDetails(int id);
        public int UserCount();
        public int PaymentCount();
        public int UserBookingCount();
        public int VehicleCount();
        public BookingListAdminViewDAL bookingList(int currentPageIndex, string searchtext);
        public PaymentViewDAL paymentid(int id);
        public PaymentViewDAL transactiom(PaymentViewDAL payment);
        public BillViewDAL bill(int id);
    }
}
