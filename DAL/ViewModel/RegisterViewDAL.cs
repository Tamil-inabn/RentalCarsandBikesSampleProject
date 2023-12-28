using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class RegisterViewDAL
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? Gender { get; set; }

        public int? Age { get; set; }

        public int? State { get; set; }

        public int? City { get; set; }

        public long? MobileNo { get; set; }

        public string? EmailId { get; set; }

        public string? Password { get; set; }

        public DateTime? UsCreatedOn { get; set; }

        public int? UsCreatedBy { get; set; }

        public DateTime? USModifiedOn { get; set; }

        public int? USModifiedBy { get; set; }

        public bool Accept { get; set; }

        public bool? IsActiveUS { get; set; }
        public int Id { get; set; }

        public string? PhotoId { get; set; }

        public IFormFile? PhotoName { get; set; }

        public string? IdProof1 { get; set; }

        public IFormFile? ProofName { get; set; }

        public DateTime? IPCreatedOn { get; set; }

        public int? IPCreatedBy { get; set; }

        public DateTime? IPModifiedOn { get; set; }

        public int? IPModifiedBy { get; set; }

        public int IsActiveIP { get; set; }
        public int StateId { get; set; }

        public string? StateName { get; set; }

        public int CityId { get; set; }

        public string? CityName { get; set; }

        public int? StateIds { get; set; }
    }
}
