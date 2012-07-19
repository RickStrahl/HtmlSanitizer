using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Westwind.Web.Utilities;
using System.Text.RegularExpressions;

namespace HtmlSanitizerTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class HtmlSanitizerTests
    {
        public HtmlSanitizerTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void HtmlSanitizerTest()
        {
            string html =
@"<div>
        Should remove Script:
        <script>alert('hello');</script>

        Should remove iFrame:
        <iframe src=""http://www.west-wind.com"" class=""iframeclass""></iframe>

        Should remove href (javascript:)
        <a href=""javascript:alert('xss');"" class='hoverbutton' />
        <br/>

        Should remove javascript: src:
        <img src=""javascript:alert('xss')"" class='hoverbutton' />

        Should remove javascript with illegal quotes:
        <img src=`javascript:alert('xss')` class='hoverbutton' />

        Should work:
        <img src='http://www.west-wind.com/images/new.gif' class='hoverbutton' />

        Remove onclick
        <div onclick=""alert('xss')"" class='test'>
        
        <div style=""color: expression(0)"" >
        </div>

<span>

        </div>
</div>
";

            string result = HtmlSanitizer.SanitizeHtml(html);

            Console.Write(result);
        }

        [TestMethod]
        public void RemoveScriptTagTest()
        {
            var html = "Should remove Script:" +
                       "<script>alert('hello');</script>";

            string result = HtmlSanitizer.SanitizeHtml(html);

            Assert.IsFalse(result.Contains("<script>") || result.Contains("</script>"));
            Console.WriteLine(result);
        }

        [TestMethod]
        public void RemoveiFrameTagTest()
        {
            var html = "Should remove iFrame: " +
                        "<iframe src=\"http://www.west-wind.com\" class='iframeclass'></iframe> <div></div>";

            string result = HtmlSanitizer.SanitizeHtml(html);

            // iframe should be removed
            Assert.IsFalse(result.Contains("<iframe>") || result.Contains("</iframe>"));
        }


        [TestMethod]
        public void RemoveJavaScriptHrefTest()
        {
            var html = "Should remove href (javascript:) " +
                       "<a href=\"javascript:alert('xss');\" />";

            string result = HtmlSanitizer.SanitizeHtml(html);

            // HREF attr should be removed (a link stays)
            Assert.IsFalse(result.Contains("href="));
        }

        [TestMethod]
        public void RemoveJavaScriptSrcTest()
        {
            var html = "Should remove src (javascript:) " +
                       "<img src=\"javascript:alert('xss');\" />";

            string result = HtmlSanitizer.SanitizeHtml(html);

            // HREF attr should be removed (a link stays)
            Assert.IsFalse(result.Contains("src="));
            Console.WriteLine(result);
        }

        [TestMethod]
        public void RemoveJavaScriptEventsTest()
        {
            var html = "Remove onclick " +
                       "<div onclick=\"alert('xss')\" onmouseover=\"alert('xss')\" class='test'>";

            string result = HtmlSanitizer.SanitizeHtml(html);

            Assert.IsFalse(result.Contains("onclick=") || result.Contains("onmouseover="));
        }

        [TestMethod]
        public void RemoveCssExpressionTest()
        {
            var html = "<div style=\"color: expression(alert('xss'))\" ></div>";


            string result = HtmlSanitizer.SanitizeHtml(html);

            //style tag should be removed
            Assert.IsFalse(result.Contains("style="));
        }




    }


}
