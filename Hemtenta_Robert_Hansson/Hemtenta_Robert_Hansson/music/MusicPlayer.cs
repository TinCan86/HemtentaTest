using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {
        // Antal sånger som finns i spellistan.
        // Returnerar alltid ett heltal >= 0.
        public int NumSongsInQueue
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // Söker i databasen efter sångtitlar som
        // innehåller "search" och lägger till alla
        // sökträffar i spellistan.
        public void LoadSongs(string search)
        {
            throw new NotImplementedException();
        }

        // Börjar spela nästa sång i kön. Om kön är tom
        // har funktionen samma effekt som Stop().
        public void NextSong()
        {
            throw new NotImplementedException();
        }

        // Returnerar strängen "Tystnad råder" om ingen
        // sång spelas, annars "Spelar <namnet på sången>".
        // Exempel: "Spelar Born to run".
        public string NowPlaying()
        {
            throw new NotImplementedException();
        }

        // Om ingen låt spelas för tillfället ska
        // nästa sång i kön börja spelas. Om en låt
        // redan spelas har funktionen ingen effekt.
        public void Play()
        {
            throw new NotImplementedException();
        }

        // Om en sång spelas ska den sluta spelas.
        // Sången ligger kvar i spellistan. Om ingen
        // sång spelas har funktionen ingen effekt.
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
