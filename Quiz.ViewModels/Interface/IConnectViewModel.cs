namespace Quiz.ViewModels.Interface
{
    public interface IConnectViewModel
    {
        string Login { get; set; }
        string Password { get; set; }

        Task<bool> LogUserIn();
    }
}
