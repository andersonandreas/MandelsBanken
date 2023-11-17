using MandelsBankenConsole.Data;
using MandelsBankenConsole.Models;

namespace MandelsBankenConsole.AccountHandler
{
    internal class TempHelpers
    {

        private readonly BankenContext _bankenContext;


        public TempHelpers(BankenContext bankenContext)
        {
            _bankenContext = bankenContext;
        }


        public User MatchingUserIdFromDb(int id)
        {
            User user = _bankenContext.Users.Find(id);

            if (user == null)
            {
                Console.WriteLine("No user in the database with that id..");
                return null;
            }

            return user;
        }












    }


}
