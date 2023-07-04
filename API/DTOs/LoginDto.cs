/*-it's going to be used for when a user logs in and inside here we just need to properties the string of their email
-*/
namespace API.DTOs
{
    public class LoginDto
    {
        public string? Email { get; set; } 
        public string? Password { get; set; }
        
    }
}