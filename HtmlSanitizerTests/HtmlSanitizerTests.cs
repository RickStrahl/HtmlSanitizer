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
        public void HtmlSanitizerTest2()
        {
            string html =
@"<div style=""color: #333333;""><font size=""2"" face=""Arial,Verdana,sans-serif""><table width=""100%"" height=""100%"" border=""0"" align=""center"" cellpadding=""10"" cellspacing=""0""> <tr> <td colspan=""2"" align=""center"" valign=""top"" background=""http://www.postlets.com/css/styles/unionsquare/bg_body.gif""><table width=""740"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center""> <tr> <td></td> <td height=""20"" align=""right""> <div style=""background-color: #6B4242; color: Gainsboro; padding-left: 5px; padding-right: 5px; padding-top: 5px; padding-bottom: 5px;""><font size=""2""><strong>April Moore, Broker</strong> | John L. Scott  | aprilmoore@johnlscott.com | (541) 296-8880</font></div> </td> </tr> </table> <table width=""740"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center"" style=""border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC;""> <tr> <td> <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center""> <tr> <td> <div style=""background-color: #E6E6E6;""> <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""7""> <tr> <td colspan=""2"" background=""http://www.postlets.com/css/colors/E6E6E6.gif"">
<table width=""100%"" cellspacing=""0"" cellpadding=""1""> <tr valign=""top""> <td height=""30"" align=""left"" valign=""top""><div style=""color: #524239;""><font size=""5"">1510 W 10th St, The Dalles, OR</font></div></td> </tr> <tr> <td width=""560"" align=""left"" valign=""top""> <div style=""color: #333333;"">Great house located on a nice lot. Minutes from shopping centers. Cute cottage home just waiting for your special touch.</div></td> </tr> </table></td> </tr> <tr> <td colspan=""2"" valign=""top"" background=""http://www.postlets.com/css/colors/E6E6E6.gif""> <table width=""724"" border=""0"" cellpadding=""4"" cellspacing=""0"" style=""border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC; border-top: 1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC;""> <tr> <td align=""left"" background=""http://www.postlets.com/css/colors/FFFEFD.gif""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""> <tr> <td width=""350"" height=""35"" valign=""top""> <div style=""color: #333333;""><font size=""4"">2BR/1BA Single Family House</font></div></td> <td valign=""top""><span style=""padding-right: 5px;""></span></td> <td align=""right"" valign=""top""><div style=""color: #333333;""><font size=""4"">offered at $88,000</font></div></td> </tr> <tr> <td valign=""top""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3"" background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-top: 1px solid #CCCCCC;"">  <tr> <td width=""125"" background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Year Built</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">1945 </td> </tr> <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Sq Footage</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">1,536 </td> </tr>  <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Bedrooms</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">2</td> </tr> <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Bathrooms</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">1 full, 0 partial </td> </tr>  <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Floors</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;""> 2 </td> </tr> <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Parking</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;""> Unspecified </td> </tr>  <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">Lot Size</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">.14 acres </td> </tr>  <tr> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: bold; color: #333333;"">HOA/Maint</td> <td background=""http://www.postlets.com/css/colors/FFFEFD.gif"" style=""border-bottom: 1px solid #CCCCCC; font-size: 12px; font-weight: normal; color: #333333;"">$0 per month</td> </tr>  </table>  </td> <td valign=""top"" width=""5""><span style=""padding-right: 5px;""></span></td> <td valign=""top""><table width=""100%"" border=""0"" cellpadding=""8"" cellspacing=""0"" background=""http://www.postlets.com/css/colors/6B4242.gif"" style=""border-left: 1px solid #6B4242; border-right: 1px solid #6B4242; border-top: 1px solid #6B4242; border-bottom: 1px solid #6B4242;""> <tr> <td><img src=""http://www.postlets.com/create/photos/20111130/123744_6666352_1035837113.jpg"" border=""1"" width=""350"" height=""262""><br>
</td> </tr> </table> </div> <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""> <tr> <td align=""center""><table width=""350"" border=""0"" cellspacing=""0"" cellpadding=""1""> <tr> <td height=""25"" align=""center"" style=""font-size: 12px; font-weight: normal; color: #333333;"">see additional photos below</td> </tr></table></td> </tr> </table> </td> </tr> </table> </td> </tr> </table> </td> </tr>  <tr> <td colspan=""2"" valign=""top"" background=""http://www.postlets.com/css/colors/E6E6E6.gif""><table width=""724"" border=""0"" cellpadding=""4"" cellspacing=""0"" style=""border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC; border-top: 1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC;""> <tr> <td align=""left"" background=""http://www.postlets.com/css/colors/FFFEFD.gif""> <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center""> <tr align=""center"" valign=""middle""> <td valign=""top"">   <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""> <tr> <td align=""left""> <div style=""color: #333333;""><span style=""font-weight: bold;"">PROPERTY FEATURES</span></div> <hr size=""1"" noshade style=""border-top: 1px solid #CCCCCC;""> <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""><tr style=""font-size: 13px; font-weight: normal; color: #333333;"">
<td width=""33%"">- Hardwood floor</td>
<td width=""33%"">- Basement</td>
<td width=""33%""></td></tr>
</table> </td> </tr> </table> <br>    </td> </tr> </table> </td> </tr> </table> </td> </tr>  
<tr> <td colspan=""2"" valign=""top"" background=""http://www.postlets.com/css/colors/E6E6E6.gif""><table width=""724"" border=""0"" cellpadding=""4"" cellspacing=""0"" style=""border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC; border-top: 1px solid #CCCCCC; border-bottom: 1px solid #CCCCCC;""> <tr> <td align=""left"" background=""http://www.postlets.com/css/colors/FFFEFD.gif""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center""> <tr align=""center"" valign=""middle""> <td valign=""top""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""3""><tr> <td valign=""top"" align=""left""> <div style=""color: #333333;""><span style=""font-weight: bold;"">ADDITIONAL PHOTOS </span></div> <hr size=""1"" noshade style=""border-top: 1px solid #CCCCCC;""><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"" align=""center""><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20111130/123744_6666352_1035837113.jpg"" border=""0"" width=""344""><br>Photo 1</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175857_6666352_2048779369.jpg"" border=""0"" width=""344""><br>Photo 2</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175857_6666352_1199427033.jpg"" border=""0"" width=""344""><br>Photo 3</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175857_6666352_4116517321.jpg"" border=""0"" width=""344""><br>Photo 5</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175858_6666352_3359455353.jpg"" border=""0"" width=""344""><br>Photo 6</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175858_6666352_2409448105.jpg"" border=""0"" width=""344""><br>Photo 7</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175858_6666352_816708808.jpg"" border=""0"" width=""344""><br>Photo 8</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175858_6666352_231593336.jpg"" border=""0"" width=""344""><br>Photo 9</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175859_6666352_1824582009.jpg"" border=""0"" width=""344""><br>Photo 10</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175859_6666352_1369489609.jpg"" border=""0"" width=""344""><br>Photo 11</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175859_6666352_369146393.jpg"" border=""0"" width=""344""><br>Photo 12</div></td><td style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175859_6666352_2571128761.jpg"" border=""0"" width=""344""><br>Photo 13</div><tr align=""center"" valign=""top""><td height=""262"" style=""font-size: 12px; font-weight: normal; color: #333333;""><div align=""center"" style=""padding-left: 2px; padding-right: 2px; padding-top: 2px; padding-bottom: 2px;""><img src=""http://www.postlets.com/create/photos/20120104/175859_6666352_2753586697.jpg"" border=""0"" width=""344""><br>Photo 14</div></td><td><span style=""padding-right: 5px;""></span></td></tr></table></td></tr></table></td></tr></table></td></tr></table></td></tr><tr> <td width=""50%"" valign=""top"" align=""left"" background=""http://www.postlets.com/css/colors/E6E6E6.gif""> <table width=""350"" border=""0"" cellpadding=""0"" cellspacing=""1"" background=""http://www.postlets.com/css/colors/E6E6E6.gif"" style=""border-left: 1px solid #E6E6E6; border-right: 2px solid #FFFEFD; border-top: 1px solid #E6E6E6; border-bottom: 1px solid #E6E6E6;""> <tr> <td><table width=""100%"" border=""0"" cellpadding=""5"" cellspacing=""0""> <tr> <td><div style=""color: #333333;""><span style=""font-weight: bold;""> Contact info:</span></div></td> </tr> <tr> <td><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0""> <tr valign=""top""> <td width=""100"" valign=""top""><img border=0 src=""http://www.postlets.com/galleries/photos/20090627233723_smallpicture.jpg"" width=""95""></td>
 <td><table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""2""> <tr><td><div style=""color: #333333;"">April Moore, Broker</div></td></tr>
<tr><td><div style=""color: #333333;"">John L. Scott </div></td></tr>
<tr><td><div style=""color: #333333;"">aprilmoore@johnlscott.com</div></td></tr>
<tr><td><div style=""color: #333333;"">(541) 296-8880</div></td></tr>
 <tr> <td><div style=""color: #333333;"">For sale by agent/broker</div></td> </tr> </table></td> </tr> </table><br></td> </tr> </table></td> </tr> </table></td><td align=""right"" valign=""bottom"" background=""http://www.postlets.com/css/colors/E6E6E6.gif"" ><table border=""0"" cellpadding=""2"" cellspacing=""0""><tr><td valign=""top""><img border=0 src=""http://www.postlets.com/galleries/logos/20090627233659_ISyoyrcfpxmwf7.jpg""></td></tr></table></td></tr></table></td></tr><tr><td background=""http://www.postlets.com/css/colors/FFFFFF.gif""><span style=""padding-left: 5px; padding-right: 5px;""><img src=""http://www.postlets.com/images/created_at_with_text_re.png"" alt=""Created at Postlets"" width=""730"" height=""59"" border=""0""></span></td></tr></table></td> </tr> </table> <table width=""740"" border=""0"" cellspacing=""0"" cellpadding=""0""> <tr> <td height=""20"" align=""left"" valign=""middle""><div style=""background-color: #6B4242; color: Gainsboro; padding-left: 5px; padding-right: 5px; padding-top: 5px; padding-bottom: 5px;""><font size=""2"">Posted: Apr 18, 2012, 8:36am PDT</font></div></td> </tr> </table></td></tr> </table></font></div>
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
