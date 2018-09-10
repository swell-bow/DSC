using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DSC.Services
{
    public interface ICompassService : INotifyPropertyChanged
    {
        string CurrentHeadingPropertyName { get; }

        void Start();

        void Stop();

        double CurrentHeading { get; }
    }
}
