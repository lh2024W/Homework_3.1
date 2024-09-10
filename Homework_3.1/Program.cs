namespace Homework_3._1
{
    public class Program
    {
        private static DatabaseService databaseService;
        static void Main()
        {
            databaseService = new DatabaseService();
            databaseService.EnsurePopulated();
        }

        public static bool Register(UserRepository userRepository)
        {
            string regUsername = Helper.GetString("username");
            string regPassword = Helper.GetString("password");
            User user = new User
            {
                UserName = regUsername,
                PasswordHash = regPassword
            };
            if (Helper.Check(user))
            {
                userRepository.ReguisterUser(user);
                return true;
            }
            return false;
        }
    }
}
