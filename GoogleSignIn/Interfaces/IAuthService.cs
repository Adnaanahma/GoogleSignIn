namespace GoogleSignIn.Service.Interfaces
{
    public interface IAuthService
    {
        Task Login(string redirectUrl);
        Task Logout();
        UserModel GetUserInfo();
    }
}
