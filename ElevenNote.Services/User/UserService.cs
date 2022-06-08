public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }
   public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        var doesEmailExist = EmailAlreadyExists(model.Email);
       var doesUsernameExist = UserNameAlreadyExists(model.Username);
       if (doesUsernameExist)
       {
           throw new ArgumentException("Username is not Unique");
       }
       if(doesEmailExist)
       {
           throw new ArgumentException("Email is already in use");
       }

        var entity = new UserEntity
        {
            Email = model.Email,
            Username = model.Username,
            Password = model.Password,
            DateCreated = DateTime.Now
        };

        _context.Users.Add(entity);
        var numberOfChanges = await _context.SaveChangesAsync();

        return numberOfChanges == 1;
    } 
    private bool UserNameAlreadyExists(string usernameToBeChecked)
    {
       var result = _context.Users.Any(
            c => 
                c.Username.ToLower() == usernameToBeChecked.ToLower()
        );
        
        return result;
        
    }
    private bool EmailAlreadyExists(string EmailToBeChecked)
    {
       var result = _context.Users.Any(
            c => 
                c.Email.ToLower() == EmailToBeChecked.ToLower()
        );
        
        return result;
        
    }
}
