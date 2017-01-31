using System;
using HemtentaTdd2017.blog;
using Xunit;
using Moq;

namespace UnitTestEnvironment
{
    public class BlogUnitTest
    {

        IBlog bg;
        Mock<IAuthenticator> authMock;
        User user = new User("Robert");
        
        public BlogUnitTest()
        {
            authMock = new Mock<IAuthenticator>();
            authMock.Setup(x => x.GetUserFromDatabase(It.IsAny<string>())).Returns(user);

            bg = new Blog(authMock.Object);
        }  

        //När man inte har parametrar att tänka på. Kan vara tex ett exception
        [Fact]
        public void LoginUser_Valid_and_Success()
        {
            bg.LoginUser(new User("Robert"));

            Assert.True(bg.UserIsLoggedIn);
        }

        //Extra gjort bara.
        [Fact]
        public void LoginUser_LogInFail()
        {
            //bg.LoginUser(null);
            Assert.False(bg.UserIsLoggedIn);
        }

        [Fact]
        public void LoginUser_InvalidObject_NullLogger_Throws()
        {   
            Assert.Throws<Exception>(() => bg.LoginUser(null));
        }


        [Fact]
        public void LogOut_Success()
        {
            bg.LogoutUser(user);

            Assert.False(bg.UserIsLoggedIn);
        }


        [Fact]
        public void LogOut_ThrowException_If_Wrong_Null()
        {
            Assert.Throws<Exception>(() => bg.LogoutUser(null));
        }


        //Kastar ett exception när någon av parametrarna är felaktiga i Page.
        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void PublishPage_IncorrectValues(string title, string content)
        {
            var page = new Page { Title = title, Content = content };

            Assert.Throws<Exception>(() => bg.PublishPage(page));
        }


        //Kastar ett exception när page objectet är Null.
        [Fact]
        public void PublishPage_PageIsNull_ThrowException()
        {
            Assert.Throws<Exception>(() => bg.PublishPage(null));
        }


        [Fact]
        public void PublishPage_Object_Publish_Success()
        {
            Assert.Throws<Exception>(() => bg.PublishPage(null));
        }

    }
}
