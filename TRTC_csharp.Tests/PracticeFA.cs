using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Tests
{
    [TestClass]
    public class PracticeFA
    {


        [TestMethod]
        public void TestStringMethod()
        {
            string actual = "ABCDEFGHI";
            actual.Should().StartWith("AB").And.EndWith("HI").And.Contain("EF").And.HaveLength(9, "That's nice");
        }

        [TestMethod]
        public void TestWotsit()
        {
            string wotsit = "cat";
            wotsit.Should().Be("cat", "I really like doggos");
        }

    }
}
