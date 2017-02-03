using System;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        User user;
        IAuthenticator auth;

        public Blog(IAuthenticator auth)
        {
            this.auth = auth;
        }

        public bool UserIsLoggedIn
        {
            get
            {
                return user != null;
            }
        }
       
        public void LoginUser(User u)
        {
            //om user är Null, så kastar vi ett exception
            if (u == null)
            {
                throw new Exception();

            }
           
            var databaseUser = auth.GetUserFromDatabase(u.Name);

            this.user = databaseUser;
                     
        }
      
        public void LogoutUser(User u)
        {
            // Försöker logga ut en användare. Kastar
            // exception om User är null.
            if (u == null)
            {
                throw new Exception();
            }

            user = null;
        }
       
        public bool PublishPage(Page p)
        {
            if (p == null || string.IsNullOrEmpty(p.Title) || string.IsNullOrEmpty(p.Content))
            {
                throw new Exception();
            }
           

            if (!UserIsLoggedIn)
            {
                return false;
            }

            return true;
        }

        public int SendEmail(string address, string caption, string body)
        {         
            if (user != null)
            {               
                if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(caption) || string.IsNullOrEmpty(body))
                {
                    return 0;
                }

                return 1;
            }

            return 0;            
        }
    }
}
