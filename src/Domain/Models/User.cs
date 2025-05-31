using Domain.Enums;
using Domain.Models.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class User : IdentityUser<int>, IAuditable
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string FullName
    {
        get { return $"{FirstName} {LastName}"; }
    }

    public UserState State { get; set; }

    public string? PictureUrl { get; set; }

    // Identity navigation properties

    public virtual ICollection<UserClaim>? Claims { get; set; }
    public virtual ICollection<UserLogin>? Logins { get; set; }
    public virtual ICollection<UserToken>? Tokens { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }

    // Auditable

    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedOn { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }
    public int? UpdatedById { get; set; }
    public User? UpdatedBy { get; set; }
}
