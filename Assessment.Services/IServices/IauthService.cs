using Assessment.Models.ViewModel;

namespace Assessment.Services.IServices;

public interface IAuthService
{
    Task<(bool isSuccess, string message, string? role)> CheckLoginCredentials(UserViewModel user);
}
