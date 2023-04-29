namespace ApiWeb.ModelDto
{
    public class UserDto
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
