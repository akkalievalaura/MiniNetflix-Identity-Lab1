namespace MiniNetflix.Identity.Constants
{
    public static class AppRoles
    {
        public const string Client = "Client";
        public const string Admin = "Admin";

        public static readonly string[] DefaultRoles =
        [
            Client,
            Admin
        ];
    }
}
