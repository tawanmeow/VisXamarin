using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VisXamarin.Pages
{
    public interface IBaseUrl { string Get(); }
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class FreeboardPage : ContentPage
    {
        public FreeboardPage()
        {
            
            WebView browser = new WebView();
            var htmlSource = new HtmlWebViewSource();
            
            htmlSource.Html = @"<html>
                                <head>
                                <link rel=""stylesheet"" href=""default.css"">
                                </head>
                                <body>
                                <!--<p><a href=""https://freeboard.io/board/6333e3e4b35a78a174000006"">Pub freeboard</a></p>-->

                                <p><a href=""index.html"">Pub freeboard</a></p>
                                </body>
                                </html>";

            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            
            browser.Source = htmlSource;
            browser.HeightRequest = 300;
            //browser.EvaluateJavaScriptAsync("document.body.innerHTML");
            Content = browser;

        }
    }
}