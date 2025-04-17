
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
        public bool IsLoggedIn (string token)
        {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            return jsonToken.ValidTo > DateTime.UtcNow;
        }
        catch
        {
            return false;
        }
    }

        
    

    public async Task<bool> Login(travlr.Backend.Models.UserCredentials credentials)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", credentials);
        return response.IsSuccessStatusCode;
    }
}

public class UserCredentials
{
    public string Email { get; set; }
    public string Password { get; set; }
}
