using System;
using Xamarin.Forms;
using VisXamarin.Android;
using VisXamarin.Pages;

[assembly: Dependency(typeof(BaseUrl_Android))]
namespace VisXamarin.Android
{
	public class BaseUrl_Android : IBaseUrl
	{
		public string Get()
		{
			return "file:///android_asset/Freeboard/";
		}
	}
}