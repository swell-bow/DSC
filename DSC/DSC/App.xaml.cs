using System;
using Xamarin.Forms;
using DSC.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace DSC
{
	public partial class App : Application
	{
		
		public App ()
		{
			InitializeComponent();


            //MainPage = new MainPage();

            MainPage = new MainMasterDetailPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
