using HemtentaTdd2017.blog;
using Moq;
using System;
using Xunit;

namespace UnitTestEnvironment
{
    public class BlogUnitTest
    {
        const string username = "username";

        IBlog bg;
        Mock<IAuthenticator> authMock;
        User user = new User(username);
        Page validPage = new Page { Title = "Moby Dick", Content = "About a fish" };

        
        public BlogUnitTest()
        {
            authMock = new Mock<IAuthenticator>();
            //Sätter upp mocken att leta rätt på en user vid name username i databasen och returnerar username.
            authMock.Setup(x => x.GetUserFromDatabase(username)).Returns(new User(username));

            bg = new Blog(authMock.Object);
        }  
       
        [Fact]
        public void LoginUser_Valid_and_Success()
        {
            bg.LoginUser(user);

            authMock.Verify((x) => x.GetUserFromDatabase(username), Times.Exactly(1));
            
            Assert.True(bg.UserIsLoggedIn);
        }
   
        [Fact]
        public void LoginUser_LogInFail()
        {
            //Behövdes inte testas enligt kommentar, extra gjort bara.
            //bg.LoginUser(null);
            Assert.False(bg.UserIsLoggedIn);
        }

        [Fact]
        public void LoginUser_InvalidObject_Null_Throws()
        {   
            Assert.Throws<Exception>(() => bg.LoginUser(null));
        }


        [Fact]
        public void LogOut_Success_On_LogOut()
        {
            //Lägg till en log in här, för att sedan logga ut

            bg.LogoutUser(user);

            Assert.False(bg.UserIsLoggedIn);
        }


        [Fact]
        public void LogOut_ThrowException_If_Null()
        {
            Assert.Throws<Exception>(() => bg.LogoutUser(null));
        }


        
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void PublishPage_IncorrectValues_In_Title_Or_Content(string title, string content)
        {
            var page = new Page { Title = title, Content = content };

            //Kastar ett exception när någon av parametrarna är felaktiga i Page.
            Assert.Throws<Exception>(() => bg.PublishPage(page));
        }


        
        [Fact]
        public void PublishPage_PageIsNull_ThrowException()
        {
            
            //Kastar ett exception när page objectet är Null.
            Assert.Throws<Exception>(() => bg.PublishPage(null));
        }


        [Fact]
        public void PublishPage_Object_Publish_Success()
        {
            bg.LoginUser(user);

            authMock.Verify((x) => x.GetUserFromDatabase(username), Times.Exactly(1));

            var result = bg.PublishPage(validPage);

            Assert.True(result);
        }

        [Fact]
        public void PublishPage_NotLoggedIn()
        {
            
            var result = bg.PublishPage(validPage);

            Assert.False(result);
        }


        [Theory]
        [InlineData(null, null, "")]
        [InlineData(null, "", null)]
        [InlineData("", null, null)]
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("", "", null)]
        [InlineData(null, "", "")]
        [InlineData("", "", "")]
        public void SendEmail_String_Is_Null_Or_Empty(string address, string caption, string body)
        {
            bg.LoginUser(user);
             
            var result = bg.SendEmail(address, caption, body);


            Assert.Equal(0, result);
        }


        [Theory]
        [InlineData("Adress", "Caption", "Body")]
        public void SendEmail_Sending_Email_Successfully(string address, string caption, string body)
        {
            bg.LoginUser(user);

            var result = bg.SendEmail(address, caption, body);


            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData("Adress", "Caption", "Body")]
        public void SendEmail_Sending_Email_With_CorrectValue_But_Not_Logged_In(string address, string caption, string body)
        {
            var result = bg.SendEmail(address, caption, body);


            Assert.Equal(0, result);
        }
    }
}
