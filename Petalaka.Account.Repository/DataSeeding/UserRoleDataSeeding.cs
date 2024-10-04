using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Repository.DataSeeding;

public class UserRoleDataSeeding
{
    public static IList<ApplicationUserRoles> DefaultUserRoles => new List<ApplicationUserRoles>
    {
        new ApplicationUserRoles()
        {
            UserId = new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"),
            RoleId = new Guid("B74C0A77-A451-4F16-DE61-08DCDFCDB851")
        },
        new ApplicationUserRoles()
        {
            UserId = new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"),
            RoleId = new Guid("D14E804F-1132-4923-DE63-08DCDFCDB851")
        },
    };
}