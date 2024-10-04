using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Repository.DataSeeding;

public class AccountDataSeeding
{
    public static IList<ApplicationUser> DefaultUsers => new List<ApplicationUser>
    {
        new ApplicationUser
        {
            Id = new Guid("094de1df-60b1-4a58-878c-dc6909f7350b"),
            FullName = "admin",
            UserName = "admin@gmail.com",
            NormalizedUserName = "admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            Salt = "UQquiGRiRIG1g/4gdm/sfMY7Kk0qqcV8iAYaY8eRmAo=",
            SecurityStamp = "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US",
            PasswordHash = "AQAAAAIAAYagAAAAEKlMNvvuvDkRs2XwysLan5iHCJP9ImDgi6iw39nygXtE1ant3Kv5n2oi6hZCqwDybA==",
        },
        new ApplicationUser
        {
            Id = new Guid("a3ee2988-67b2-4017-b63b-a0dae4708359"),
            FullName = "provider",
            UserName = "provider@gmail.com",
            NormalizedUserName = "provider",
            Email = "provider@gmail.com",
            NormalizedEmail = "PROVIDER@GMAIL.COM",
            EmailConfirmed = true,
            Salt = "UQquiGRiRIG1g/4gdm/sfMY7Kk0qqcV8iAYaY8eRmAo=",
            SecurityStamp = "A6WZZDMSOY6XEPH4VJRSRVTAXICX34US",
            PasswordHash = "AQAAAAIAAYagAAAAEKlMNvvuvDkRs2XwysLan5iHCJP9ImDgi6iw39nygXtE1ant3Kv5n2oi6hZCqwDybA==",
        }
    };
}