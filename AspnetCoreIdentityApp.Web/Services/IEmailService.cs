namespace AspnetCoreIdentityApp.Web.Services
{
    public interface IEmailService
    {
        Task SendForgetPasswordEmail(string forgetEmailLink, string toEmail);
    }
}
