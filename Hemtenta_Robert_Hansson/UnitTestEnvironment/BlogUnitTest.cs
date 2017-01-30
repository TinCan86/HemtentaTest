using System;
using HemtentaTdd2017.blog;
using Xunit;

namespace UnitTestEnvironment
{
    public class BlogUnitTest
    {
        IBlog bg;

        public BlogUnitTest()
        {
            bg = new Blog();
        }


        [Theory]
        //[InlineData("B", 26)]       
        public void EnMetod()
        {
            //Act


            //Arrange


            //Assert
        }

        //När man inte har parametrar att tänka på. Kan vara tex ett exception
        [Fact]
        public void EnTillMetod()
        {

        }

        [Fact]
        public void TestMethod1()
        {
            Assert.Equal(4, Add(2, 2));

        }

        int Add(int x, int y)
        {
            return x + y;
        }

    }
}
