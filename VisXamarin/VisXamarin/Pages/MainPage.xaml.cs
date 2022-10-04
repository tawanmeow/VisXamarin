using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VisXamarin.Pages;
using Xamarin.Forms.Xaml;
using VisXamarin.PageModels;
using RadialGauge;

namespace VisXamarin.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        
        public Command RefreshCommand { get; set; }
        public MainPageModel mainPageModel = new MainPageModel();
        public MainPage()
        {
            //MainPageModel mainPageModel = new MainPageModel();
            InitializeComponent();
            //gqlVisualizeNetpie();
            Console.WriteLine("+++++++++++++++++++++++++ MAINPAGE.XAML.CS +++++++++++++++++++++++++");
            //Console.WriteLine(mainPageModel.TextVisualize);
            
        }

        public async void gqlVisualizeNetpie()
        {
            //MainPageModel mainPageModel = new MainPageModel();
            var graphQLClient = new GraphQLHttpClient("http://gqlv2.netpie.io/", new NewtonsoftJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {mainPageModel.NetpieToken}");

            var query = new GraphQLRequest
            {
                Query = @"
                query Query {
			        shadow (deviceid:""704ab8a5-7ce0-4157-970c-bc540d6c7dbc"") {
                        data
                        deviceid
                    }
                }"
            };
            //Console.WriteLine("GQLVISUALIZENETPIE QUERY +++++++++++++++++++++++++++++++++++++++++++++++");
            var graphQLResponse = await graphQLClient.SendQueryAsync<ShadowResponseVisualize>(query);
            mainPageModel.TextVisualize = graphQLResponse.Data.Shadow.Data.Text.IsVisible;
            mainPageModel.GaugeVisualize = graphQLResponse.Data.Shadow.Data.Gauge.IsVisible;

            Label VisualizeText = new Label();
            if (mainPageModel.TextVisualize == true)
            {
                
                VisualizeText.SetBinding(Label.TextProperty, "Temp"); // Set Binding for Text.Text
                //VisualizeText.Text = mainPageModel.Temp;
                VisualizeText.FontSize = graphQLResponse.Data.Shadow.Data.Text.FontSize;
                VisualizeText.VerticalOptions = LayoutOptions.Center;
                VisualizeText.HorizontalOptions = LayoutOptions.Center;
                VisualizeStack.Children.Add(VisualizeText);
            }
            /*else
            {
                Console.WriteLine("ELSE ELSE ELSE !");
                try
                { VisualizeStack.Children.Remove(VisualizeText); }
                catch { }
                    
            }*/
            if (mainPageModel.GaugeVisualize == true)
            {
                Gauge VisualizeGauge = new Gauge();
                //VisualizeGauge.CurrentValue = Convert.ToDouble(mainPageModel.Temp);
                VisualizeGauge.SetBinding(Gauge.CurrentValueProperty, "Temp"); // Set Binding for Gauge.CurrentValue
                VisualizeGauge.MinValue = graphQLResponse.Data.Shadow.Data.Gauge.MinValue;
                VisualizeGauge.MaxValue = graphQLResponse.Data.Shadow.Data.Gauge.MaxValue;
                VisualizeGauge.UnitOfMeasurement = graphQLResponse.Data.Shadow.Data.Gauge.UnitOfMeasurement;
                VisualizeGauge.BottomText = graphQLResponse.Data.Shadow.Data.Gauge.BottomText;
                VisualizeGauge.WidthRequest = graphQLResponse.Data.Shadow.Data.Gauge.WidthRequest;
                VisualizeGauge.HeightRequest = graphQLResponse.Data.Shadow.Data.Gauge.HeightRequest;
                VisualizeGauge.VerticalOptions = LayoutOptions.CenterAndExpand;
                VisualizeGauge.HorizontalOptions = LayoutOptions.CenterAndExpand;
                VisualizeStack.Children.Add(VisualizeGauge);
            }


        }


        async void RefreshView_Refreshing(System.Object sender, System.EventArgs e)
        {
            await Task.Delay(3000);
            myRefreshView.IsRefreshing = false;

        }
        public class ShadowResponseVisualize
        {
            public ShadowContentVisualize Shadow { get; set; }
        }

        public class ShadowContentVisualize
        {
            public DataContentVisualize Data { get; set; }
            public string DeviceID { get; set; }

        }
        public class DataContentVisualize
        {
            public TextVisualize Text { get; set; }
            public GaugeVisualize Gauge { get; set; }
        }

        public class TextVisualize
        {
            public bool IsVisible { get; set; }
            public string Text { get; set; }
            public int FontSize { get; set; }
        }

        public class GaugeVisualize
        {
            public bool IsVisible { get; set; }
            public string UnitOfMeasurement { get; set; }
            public int MinValue { get; set; }
            public int MaxValue { get; set; }
            public int WidthRequest { get; set; }
            public int HeightRequest { get; set; }
            public string BottomText { get; set; }
        }
    }
}