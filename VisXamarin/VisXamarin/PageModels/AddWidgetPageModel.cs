using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VisXamarin.PageModels
{
    public class AddWidgetPageModel : FreshMvvm.FreshBasePageModel
    {
        public AddWidgetPageModel()
        {
        }
        public string Count { get; set; } = "10";
        public string Visible { get; set; } = "True";

        public Command AddText => new Command(() =>
        {
            CoreMethods.PopPageModel("Text");
        });

        public Command AddGauge => new Command(() =>
        {
            CoreMethods.PopPageModel("Gauge");
        });
    }
}
