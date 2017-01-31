using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // True om användaren är inloggad (behöver
        // inte testas separat)
        public bool UserIsLoggedIn
        {
            get
            {
                //Gör ett if statement i return
                return user != null;
            }
        }

        // Försöker logga in en användare. Man kan
        // se om inloggningen lyckades på property
        // UserIsLoggedIn.
        // Kastar ett exception om User är null.
        public void LoginUser(User u)
        {
            //om user är Null, så kastar vi ett exception
            if (u == null)
            {
                throw new Exception();

            }

            //Annars sätter vi user till u.
            user = u;
        }

        // Försöker logga ut en användare. Kastar
        // exception om User är null.
        public void LogoutUser(User u)
        {
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
           
            if (user != null)
            {
                //todo publish:

                return true;
            }

            return false;
        }

        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        public int SendEmail(string address, string caption, string body)
        {
            throw new NotImplementedException();
        }
    }
}
