using System;
using System.Collections.Generic;

namespace DAL.EntityModels;

public partial class IdProof
{
    public int Id { get; set; }

    public string? PhotoId { get; set; }

    public string? PhotoName { get; set; }

    public string? IdProof1 { get; set; }

    public string? ProofName { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? ModifiedBy { get; set; }

    public int? UserId { get; set; }

    public bool? IsActive { get; set; }

    public virtual UserRegister? User { get; set; }
}
