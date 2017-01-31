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
                //Gör ett if statement i return
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

        // För att publicera en sida måste Page vara
        // ett giltigt Page-objekt och användaren
        // måste vara inloggad.
        // Returnerar true om det gick att publicera,
        // false om publicering misslyckades och
        // exception om Page har ett ogiltigt värde.
        public bool PublishPage(Page p)
        {
            if (p == null || string.IsNullOrEmpty(p.Title) || string.IsNullOrEmpty(p.Content))
            {
                throw new Exception();
            }
           

            if (!UserIsLoggedIn)
            {
                //todo publish:

                return false;
            }

            return true;
        }

        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        public int SendEmail(string address, string caption, string body)
        {         
            if (user != null)
            {
                //todo publish:
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
