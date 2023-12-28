using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? UserName { get; set; }

    public string? Comment1 { get; set; }

    public int? UsersCount { get; set; }

    public int? BookingCount { get; set; }

    public int? StarRating { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    public virtual UserRegister? User { get; set; }
}
