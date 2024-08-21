using Quiz.Dtos.App;
using Quiz.ForNative.Repository.Interfaces;

namespace Quiz.ForNative.Repository
{
    public record UserSearchArgs(string Name, string Firstname, string Pseudo, string Email);

    public class UserRepository : IRepository<UserDto, UserSearchArgs>
    {
        public HttpClient HttpClient { get; set; }

        public UserRepository(HttpClient httpClient) 
        {
            HttpClient = httpClient;
        }

        public bool Add(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(UserDto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> Search(UserSearchArgs args)
        {
            throw new NotImplementedException();
        }

        public UserDto Update(UserDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
