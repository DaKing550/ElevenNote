public interface IUserService
{
    Task<bool> RegisterUserAsync(UserRegister model);
}