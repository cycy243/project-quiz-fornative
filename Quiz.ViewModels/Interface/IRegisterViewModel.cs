using Quiz.ViewModels.Exceptions;

namespace Quiz.ViewModels.Interface
{
    public record RegisterUserArgs(string Name, string Firstname, string Pseudo, string Password, string Email, string PicturePath, string Bio);

    /// <summary>
    /// Interface that exposes what the viewmodel of the register view, has to be able to do
    /// </summary>
    public interface IRegisterViewModel
    {
        /// <summary>
        /// Register a user with the given information
        /// </summary>
        /// <param name="args">Argument to register a user</param>
        /// <returns>
        /// true is the user has successfully registered false otherwise
        /// </returns>
        /// <exception cref="ViewModelException">If an error occured</exception>
        bool RegisterUser(RegisterUserArgs args);
    }
}
