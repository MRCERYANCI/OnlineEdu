namespace OnlineEdu.PresentationLayer.Services.MailServices
{
    public interface IEmailSender
    {
        Task SendConfirmedEmailAsync(string email, string Name_Surname, string userName, string urlLink);
    }
}
