namespace Quiz.Dtos.App
{
    public record UserDto(string Name, string Firstname, string Pseudo, string Email, string PicturePath, string Bio, string? Id = null, string? Password = null);
}
