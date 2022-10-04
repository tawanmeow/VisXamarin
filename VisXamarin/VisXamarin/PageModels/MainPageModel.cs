using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VisXamarin.Pages;

namespace VisXamarin.PageModels
{
    public class MainPageModel : FreshMvvm.FreshBasePageModel
    {
        public string NetpieToken { get; set; } = "eyJhbGciOiJFUzI1NiIsInR5cCI6IkpXVCJ9.eyJjdHgiOnsidXNlcmlkIjoiVT" +
            "gwMTcxNDU2ODA3OSIsImNsaWVudGlkIjoiNWI0NGM0ZDRkMWMwODY5ZmE0YjNlZDkyZTFmNzgzYjgifSwic2NvcGUiOltdLCJpYX" +
            "QiOjE2NjQyNDY1MDMsIm5iZiI6MTY2NDI0NjUwMywiZXhwIjoxNjY0MzMyOTAzLCJleHBpcmVJbiI6ODY0MDAsImp0aSI6IlRIVW" +
            "tXZnBNIiwiaXNzIjoiY2VyOnVzZXJ0b2tlbiJ9.QA2yxmUJm06jFB4dDgl9gIBrDxZZlQsFX3XrrSKUo5qUtWV7qWSDZA6ZeywYf2mjafrR-92bjPbKevZ2FlUxBw";

        public string Temp { get; set; } = "10";
        public string TextVisible { get; set; } = "True";
        public bool TextVisualize { get; set; } = false;
        public string GaugeVisible { get; set; } = "True";
        public bool GaugeVisualize { get; set; } = false;
        public string Widget { get; set; } = "";
        

        public MainPageModel() 
        {
            //gqlNetpie();
            //StartTimer();
            Console.WriteLine("MainPageModel");
        }

        public Command ClickCommand => new Command(() =>
        {
            gqlNetpie();
        });

        public Command IsTextVisible => new Command(() =>
        {
            if(TextVisible == "False")
            {
                TextVisible = "True";
                
            }
            else
            {
                //TextVisible = "False";
            }
            RaisePropertyChanged(nameof(TextVisible));
            

        });

        public Command IsGaugeVisible => new Command(() =>
        {
            if (GaugeVisible == "False")
            {
                GaugeVisible = "True";
            }
            else
            {
                GaugeVisible = "False";
            }
            RaisePropertyChanged(nameof(GaugeVisible));

        });

        public Command AddWidget => new Command(() =>
        {
            CoreMethods.PushPageModel<AddWidgetPageModel>();
        });

        public Command GoToFreeboard => new Command(() =>
        {
            CoreMethods.PushPageModel<FreeboardPageModel>();
        });

        public void StartTimer()
        {
            Device.StartTimer(new TimeSpan(0, 0, 60), () =>
            {
                gqlNetpie();
                MainPage mainpage = new MainPage();
                mainpage.gqlVisualizeNetpie();
                Device.BeginInvokeOnMainThread(() =>
                {
                    // interact with UI elements

                });
                return true; // runs again, or false to stop
            });
        }

        // Widget visibillity (OBSOLETE)
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            Widget = returnedData as string;
            RaisePropertyChanged(nameof(Widget));
            switch (Widget){
                case "Text":
                    TextVisible = "True";
                    RaisePropertyChanged(nameof(TextVisible));

                    break;
                case "Gauge":
                    GaugeVisible = "True";
                    RaisePropertyChanged(nameof(GaugeVisible));
                    break;

                default:
                    Console.WriteLine("XD");
                    break;
            }
        }



        // Note : app crash when using Task<String> & return
        //public async Task<String> gqlNetpie()
        public async void gqlNetpie()
        {
            var graphQLClient = new GraphQLHttpClient("http://gqlv2.netpie.io/", new NewtonsoftJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {NetpieToken}");

            var query = new GraphQLRequest
            {
                Query = @"
                query Query {
			        shadow (deviceid:""d01af75b-091f-4d63-b47b-96da4ed4f9db"") {
                        data
                        deviceid
                    }
                }"
            };
            var graphQLResponse = await graphQLClient.SendQueryAsync<ShadowResponse>(query);
            var data = graphQLResponse.Data.Shadow.Data.Temp;
            Console.WriteLine("Display result on screen.");
            Console.WriteLine(data);
            Temp = data.ToString();
            RaisePropertyChanged(nameof(Temp));
            
        }

        

        public void DisplayText(ref int simTemp)
        {
            Console.WriteLine(simTemp.ToString());
        }

        public class ShadowResponse
        {
            public ShadowContent Shadow { get; set; }
        }

        public class ShadowContent
        {
            public DataContent Data { get; set; }
            public string DeviceID { get; set; }

        }
        public class DataContent
        {
            public int Temp { get; set; }
            public string Sth { get; set; }
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
            public bool Text { get; set; }
            //public string Gauge { get; set; }
        }
    }



}
