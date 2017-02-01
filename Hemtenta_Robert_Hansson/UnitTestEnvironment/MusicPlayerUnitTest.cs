using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;
using HemtentaTdd2017.music;

namespace UnitTestEnvironment
{
    public class MusicPlayerUnitTest
    {
        

        [Fact]
        public void LoadSongs_Success()
        {
            var mockDataBase = new Mock<IMediaDatabase>();
            MusicPlayer mediaPlayer = new MusicPlayer();
            mediaPlayer.mediaDataBase = mockDataBase.Object;

            string search = "antonio1";

            mediaPlayer.LoadSongs(search);
            mediaPlayer.LoadSongs("antonio");


            mockDataBase.Verify(x => x.FetchSongs("antonio"), Times.Once());
        }

        [Fact]
        public void LoadSongs_Throw_Exception_If_Load_Fail()
        {
            
        }

        }
}
