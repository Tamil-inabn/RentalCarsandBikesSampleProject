using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class UserRegister
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

    public DateTime? CreatedOn { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? Accept { get; set; }

    public bool? IsActive { get; set; }

    public int? RollId { get; set; }

    public virtual ICollection<BookingCancel> BookingCancels { get; } = new List<BookingCancel>();

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<IdProof> IdProofs { get; } = new List<IdProof>();

    public virtual ICollection<UserBooking> UserBookings { get; } = new List<UserBooking>();
}
