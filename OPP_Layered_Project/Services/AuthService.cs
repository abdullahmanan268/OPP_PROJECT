namespace LostAndFoundCSharp.Services
{
    
    public class AuthService
    {
        public bool IsAdmin = false;

        public bool CheckPassword(string password)
        {
            if (password == "1234")
            {
                IsAdmin = true;
                return true;
            }
            return false;
        }
    }
}
