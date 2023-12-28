using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class LoginCheckViewDAL
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public int? RollId { get; set; }
    }
}
