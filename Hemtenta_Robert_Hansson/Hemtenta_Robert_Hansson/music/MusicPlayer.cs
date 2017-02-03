using System.Collections.Generic;
using System.Linq;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {
        ISoundMaker _soundMaker;
        IMediaDatabase _database;
        IList<ISong> _playList;

        public MusicPlayer(IMediaDatabase database, ISoundMaker soundMaker)
        {
            _soundMaker = soundMaker;
            _database = database;
            _playList = new List<ISong>();

        }

        // Antal sånger som finns i spellistan.
        // Returnerar alltid ett heltal >= 0.
        public int NumSongsInQueue
        {
            get
            {
                if (_playList.Count >= 0)
                {
                    return _playList.Count;
                }

                else
                {
                    return 0;
                }
            }
        }
 
        public void LoadSongs(string search)
        {
            if (!_database.IsConnected)
            {
                throw new DatabaseClosedException();
            }

            _database.OpenConnection();

            
            if (!string.IsNullOrEmpty(search))
            {
                _playList = _database.FetchSongs(search);
            }

            if (string.IsNullOrEmpty(search))
            {
                search = "No songs found";
            }
            
            _database.CloseConnection();
        }

        public void NextSong()
        {
            if (NumSongsInQueue > 0)
            {
                
                _soundMaker.Play(_playList.FirstOrDefault());        
            }

            else
            {
                Stop();
            }
            
        }

        public string NowPlaying()
        {
            if (string.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                return "Tystnad råder";
            }

            else
            {
                return string.Format("Spelar {0}", _soundMaker.NowPlaying);
            }

        }

        public void Play()
        {
            if (string.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                //Använder samma som i NextSong. Så om soundmakers NowPlaying är null eller tom så sker detta.
                _soundMaker.Play(_playList.FirstOrDefault());
            }
        }

        public void Stop()
        {
            _soundMaker.Stop();
        }

        public void OpenConnection()
        {
            if (_database.IsConnected)
            {
                throw new DatabaseAlreadyOpenException();
            }

            _database.OpenConnection();
        }
    }
}
