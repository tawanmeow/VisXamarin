using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VisXamarin.Pages
{
    public interface IBaseUrl { string Get(); }
    //[XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class FreeboardPage : ContentPage
    {
        public static WebView browser = new WebView();
        public FreeboardPage()
        {
            Console.WriteLine("Start");
            //WebView browser = new WebView();
            /*var htmlSource = new HtmlWebViewSource();
            
            htmlSource.Html = @"<html>
                                <head>
                                <link rel=""stylesheet"" href=""default.css"">
                                <script type=""text/javascript"">
                                function changeDivContent(receivedText)
                                    {  
                                        alert(receivedText);
                                        console.log(receivedText);
                                        document.getElementById(""content"").innerHTML = receivedText;
                                    };
                                </script>
                                </head>
                                <body>
                                <!--<p><a href=""https://freeboard.io/board/6333e3e4b35a78a174000006"">Pub freeboard</a></p>-->
                                <div id=""content"" name=""content"">XD</div>
                                <p><a href=""index.html"">Pub freeboard</a></p>
                                </body>
                                </html>";

            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            browser.Source = htmlSource;
            browser.HeightRequest = 300;
            string testText = "i`m here!";
            browser.Navigated += (o, s) => {
                //browser.Eval("changeDivContent();");
                string jsCmd = $"changeDivContent('" + testText +"')";
                browser.EvaluateJavaScriptAsync(jsCmd);
                //browser.EvaluateJavaScriptAsync($"changeDivContent{testText}");
                //browser.Eval(string.Format("changeDivContent({0})", strtest));
            };
            //browser.Eval("alert('text')");

            //browser.EvaluateJavaScriptAsync(@"document.getElementById(""content"").innerHTML = ""whatever""");
            Content = browser;*/


            // New Version
            //DependencyService.Get<IClearCookies>().ClearAllCookies();
            WebView browser = new WebView();
            
            var urlSource = new UrlWebViewSource();

            string baseUrl = DependencyService.Get<IBaseUrl>().Get();
            //string filePathUrl = Path.Combine(baseUrl, "index.html");
            string clientid = "f12896d3-a400-41c1-9b53-b57574d252a1";
            string token = "XUgU7ygfaRKbWf6UUjz6DQP23scdcTco";
            string freeboardUrl = "index.html#" + clientid + ":" + token;
            string filePathUrl = Path.Combine(baseUrl, freeboardUrl);
            urlSource.Url = filePathUrl;
            browser.Source = urlSource;
            string JWTtoken = "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdHgiOnsidXNlcmlkIjoiVTkzODc4ODI5NDUyMiIsImNsaWVudGlkIjoiN" +
                "WI0NGM0ZDRkMWMwODY5ZmE0YjNlZDkyZTFmNzgzYjgifSwic2NvcGUiOltdLCJpYXQiOjE2NjQ3ODQxNzYsIm5iZiI6MTY2NDc4NDE3NiwiZXhwIjo" +
                "xOTgwNDAzNTQ4LCJleHBpcmVJbiI6MzE1NjE5MzcyLCJqdGkiOiJpZ21rMUVpRSIsImlzcyI6ImNlcjp1c2VydG9rZW4ifQ.wNf8zR-wYqGOW5IRs4" +
                "EG7MzrukVer-HxSHHaXmW13LwGgA3YAxKq9uCMnhEhSjyptVgHFPjI9vTEhne15oG--A";
            //string testText = "not a freeboard";
            browser.Navigated += (o, s) => {
                string javascriptCommand = $"loadDashboard('"+ JWTtoken + "');";
                browser.EvaluateJavaScriptAsync(javascriptCommand);
                //browser.EvaluateJavaScriptAsync("alert('ehe');");
            };


            Content = browser;
            

        }

        /*protected override void OnAppearing()
        {
            base.OnAppearing();

            DependencyService.Get<IClearCookies>().ClearAllCookies();

        }/*

        /*public static async Task JSRun()
        {
            retorno = await webnav.EvaluateJavaScriptAsync("changeDivContent();");
        }*/




    }
}