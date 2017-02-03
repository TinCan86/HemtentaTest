using HemtentaTdd2017.music;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTestEnvironment
{
    public class MusicPlayerUnitTest
    {
        //Det jag har lärt mig för att slippa upprepning av deklarationer att göra på detta sättet.

        MusicPlayer _musicPlayer;
        Mock<IMediaDatabase> _database;
        ISoundMaker _soundMaker;
        List<ISong> _songs;

        //Gör variablar för att slippa upprepa titelnamnet på låtarna.
        const string Song1 = "Number of the beast";
        const string Song2 = "Run to the hills";

        public MusicPlayerUnitTest()
        {
            //Gör en mock av IMediaDatabase
            _database = new Mock<IMediaDatabase>();
            _soundMaker = new SoundMaker();


            _songs = new List<ISong>
            {
                new Song(Song1),
                new Song(Song2)
            };

            //Gör en setup så att IsConnected returns true.
            _database.Setup(x => x.IsConnected).Returns(true);
            //Hämtar låtar vad det än är för sträng, returnerar song listan.
            _database.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs);

            //gör ett nytt musicplayer object med mockdatabasen och soundmaker i.
            _musicPlayer = new MusicPlayer(_database.Object, _soundMaker);
        }

        [Fact]
        public void NumSongsInQueue_Return_Int_Success()
        {
            _database.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs.Where(s => s.Title.Contains(Song1)).ToList());

            _musicPlayer.LoadSongs(Song1);            

            //Kollar så att det är 1 låtar i kön
            Assert.Equal(1, _musicPlayer.NumSongsInQueue);
        }

        [Fact]
        public void NumSongsInQueue_Return_0_If_Nothing_In_Queue_When_Trying_To_Load_Empty_String()
        {
            _database.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs.Where(s => s.Title.Contains("")).ToList());

            _musicPlayer.LoadSongs("");

            //Kollar så att det är 0 låtar i kön, då det ska returneras 0.
            Assert.Equal(0, _musicPlayer.NumSongsInQueue);
        }

        [Fact]
        public void LoadSongs_Success()
        {
            _database.Setup(x => x.FetchSongs(It.IsAny<string>())).Returns(_songs.Where(s => s.Title.Contains(Song2)).ToList());

            _musicPlayer.LoadSongs(Song1);

            //Kollar så att playlisten har laddats upp, kollar detta med att räkna hur många som finns i kön.
            Assert.Equal(1, _musicPlayer.NumSongsInQueue);
        }

        [Fact]
        public void LoadSongs_Throw_Exception_If_Load_Fail_Not_Connected_To_Database()
        {
            _database.Setup(x => x.IsConnected).Returns(false);
            Assert.Throws<DatabaseClosedException>(() => _musicPlayer.LoadSongs("search"));          
        }

        [Fact]
        public void LoadSongs_InvalidValue()
        {
            //_musicPlayer.LoadSongs("");
            _musicPlayer.LoadSongs(null);

            Assert.Equal(0, _musicPlayer.NumSongsInQueue);      
        }

        [Fact]
        public void Play_And_NowPlaying_Successfully()
        {
            _musicPlayer.LoadSongs("To be wild");
            _musicPlayer.Play();

            var expected = string.Format("Spelar {0}", Song1);
            var result = _musicPlayer.NowPlaying();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Stop_NowPlaying_Success()
        {
            _musicPlayer.LoadSongs("search");
            _musicPlayer.Play();
            _musicPlayer.Stop();

            var expected = "Tystnad råder";
            var result = _musicPlayer.NowPlaying();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NextSong_Success()
        {
            _musicPlayer.LoadSongs("search");
            _musicPlayer.NextSong();

            var expected = string.Format("Spelar {0}", Song1);
            var result = _musicPlayer.NowPlaying();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NowPlaying_Success_Write_Out_Title_Song()
        {
            _musicPlayer.LoadSongs("search");
            _musicPlayer.Play();

            var expected = string.Format("Spelar {0}", Song1);
            var result = _musicPlayer.NowPlaying();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void OpenConnection_DatabaseAlreadyOpen_Throws_Exception()
        {
            Assert.Throws<DatabaseAlreadyOpenException>(() => _musicPlayer.OpenConnection());
        }
    }
}

