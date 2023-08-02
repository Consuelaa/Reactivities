/*-this is going to have props that we want to send back when a client has successfully logged/registered
- ? dupa tipul de date= to indicate that the type is nullable
- 

 */
namespace API.DTOs
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }

    }
}