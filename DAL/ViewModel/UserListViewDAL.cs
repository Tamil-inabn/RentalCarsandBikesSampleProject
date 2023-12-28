using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class UserListViewDAL
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public int? State { get; set; }
        public string? StateName { get; set; }
        public int? City { get; set; }
        public string? CityName { get; set; }
        public long? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? PhotoId { get; set; }
        public string? PhotoName { get; set; }
        public string? PhotoNames { get; set; }
        public string? IdProof1 { get; set; }
        public string? ProofName { get; set; }
        public string? ProofNames { get; set; }
        public int? RollId { get; set; }
        public bool? IsActive { get; set; }
        public int Id { get; set; }
        public string? Password { get; set; }
    }
}
