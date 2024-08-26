namespace Quiz.ForNative.Domain
{
    public record User(string Name, string Firstname, string Pseudo, string Email, string PicturePath, string Bio, string? Id = null, string? Password = null);
}
