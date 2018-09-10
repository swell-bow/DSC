using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DSC.Droid.Services;
using DSC.Services;
using Plugin.Compass;

[assembly: Xamarin.Forms.Dependency(typeof(CompassService))]
namespace DSC.Droid.Services
{
    class CompassService : ICompassService
    {
        private bool _isStarted;

        public double CurrentHeading { get; private set; }

        public string CurrentHeadingPropertyName => "CurrentHeading";

        public event PropertyChangedEventHandler PropertyChanged;

        public void Start()
        {
            if (!_isStarted)
            {
                _isStarted = true;

                CrossCompass.Current.Start();

                CrossCompass.Current.CompassChanged += (s, e) =>
                {
                    CurrentHeading = e.Heading;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(CurrentHeadingPropertyName));
                };
            }
        }

        public void Stop()
        {
            _isStarted = false;
            CrossCompass.Current.Stop();
        }
    }
}