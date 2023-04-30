namespace ModelsLibrary.Models.Users;

public class UserTokensDto
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public TimeSpan Validaty { get; set; }
    public string RefreshToken { get; set; }
    public int Id { get; set; }
    public string EmailId { get; set; }
    public DateTime ExpiredTime { get; set; }

}
