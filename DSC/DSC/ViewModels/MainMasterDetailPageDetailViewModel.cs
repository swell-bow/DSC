using DeviceMotion.Plugin;
using DSC.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DSC.ViewModels
{
    class MainMasterDetailPageDetailViewModel : INotifyPropertyChanged
    {
        public readonly string CurrentHeadingPropertyName = "CurrentHeading";

        public ICompassService CompassService { get; }

        public double CurrentHeading => CompassService.CurrentHeading;

        DeviceMotion.Plugin.Abstractions.MotionVector magnetometerVector;
        public DeviceMotion.Plugin.Abstractions.MotionVector MagnetometerVector
        {
            get
            {
                return magnetometerVector;
            }
            set
            {
                magnetometerVector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MagnetometerVector"));
            }
        }

        double magnetometerSingle;
        public double MagnetometerSingle
        {
            get
            {
                return magnetometerSingle;
            }
            set
            {
                magnetometerSingle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MagnetometerSingle"));
            }
        }

        DeviceMotion.Plugin.Abstractions.MotionVector gyroscopeVector;
        public DeviceMotion.Plugin.Abstractions.MotionVector GyroscopeVector
        {
            get
            {
                return gyroscopeVector;
            }
            set
            {
                gyroscopeVector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GyroscopeVector"));
            }
        }

        double gyroscopeSingle;
        public double GyroscopeSingle
        {
            get
            {
                return gyroscopeSingle;
            }
            set
            {
                gyroscopeSingle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GyroscopeSingle"));
            }
        }

        DeviceMotion.Plugin.Abstractions.MotionVector accelerometerVector;
        public DeviceMotion.Plugin.Abstractions.MotionVector AccelerometerVector
        {
            get
            {
                return accelerometerVector;
            }
            set
            {
                accelerometerVector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AccelerometerVector"));
            }
        }

        double accelerometerSingle;
        public double AccelerometerSingle
        {
            get
            {
                return accelerometerSingle;
            }
            set
            {
                accelerometerSingle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AccelerometerSingle"));
            }
        }

        DeviceMotion.Plugin.Abstractions.MotionVector compassVector;
        public DeviceMotion.Plugin.Abstractions.MotionVector CompassVector
        {
            get
            {
                return compassVector;
            }
            set
            {
                compassVector = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompassVector"));
            }
        }

        double compassSingle;
        public double CompassSingle
        {
            get
            {
                return compassSingle;
            }
            set
            {
                compassSingle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CompassSingle"));
            }
        }

        Position currentPosition;
        public Position CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                currentPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPosition"));
            }
        }

        float x;
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("X"));
            }
        }

        float y;
        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Y"));
            }
        }

        float z;
        public float Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Z"));
            }
        }

        public MainMasterDetailPageDetailViewModel()
        {
            CompassService = DependencyService.Get<ICompassService>();
            CompassService.Start();
            CompassService.PropertyChanged += CompassService_PropertyChanged;

            CrossDeviceMotion.Current.Start(DeviceMotion.Plugin.Abstractions.MotionSensorType.Accelerometer);
            CrossDeviceMotion.Current.Start(DeviceMotion.Plugin.Abstractions.MotionSensorType.Compass);
            CrossDeviceMotion.Current.Start(DeviceMotion.Plugin.Abstractions.MotionSensorType.Gyroscope);
            CrossDeviceMotion.Current.Start(DeviceMotion.Plugin.Abstractions.MotionSensorType.Magnetometer);
            CrossDeviceMotion.Current.SensorValueChanged += Current_SensorValueChanged;

            CrossGeolocator.Current.PositionChanged += Current_PositionChanged;

            MessagingCenter.Subscribe<XYZ>(this, XYZ.MCUpdated, (sender) => 
            {
                X = sender.X;
                Y = sender.Y;
                Z = sender.Z;
            });
        }

        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CurrentPosition = e.Position;
        }

        public ICommand AppearingCommand => new Command(async() => { await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(10), .1); });

        private void Current_SensorValueChanged(object sender, DeviceMotion.Plugin.Abstractions.SensorValueChangedEventArgs e)
        {
            switch (e.SensorType)
            {
                case DeviceMotion.Plugin.Abstractions.MotionSensorType.Accelerometer:
                    if (e.ValueType == DeviceMotion.Plugin.Abstractions.MotionSensorValueType.Vector)
                        AccelerometerVector = e.Value as DeviceMotion.Plugin.Abstractions.MotionVector;
                    else
                        AccelerometerSingle = e.Value.Value.GetValueOrDefault();
                    break;
                case DeviceMotion.Plugin.Abstractions.MotionSensorType.Gyroscope:
                    if (e.ValueType == DeviceMotion.Plugin.Abstractions.MotionSensorValueType.Vector)
                        GyroscopeVector = e.Value as DeviceMotion.Plugin.Abstractions.MotionVector;
                    else
                        GyroscopeSingle = e.Value.Value.GetValueOrDefault();
                    break;
                case DeviceMotion.Plugin.Abstractions.MotionSensorType.Magnetometer:
                    if (e.ValueType == DeviceMotion.Plugin.Abstractions.MotionSensorValueType.Vector)
                        MagnetometerVector = e.Value as DeviceMotion.Plugin.Abstractions.MotionVector;
                    else
                        MagnetometerSingle = e.Value.Value.GetValueOrDefault();
                    break;
                case DeviceMotion.Plugin.Abstractions.MotionSensorType.Compass:
                    if (e.ValueType == DeviceMotion.Plugin.Abstractions.MotionSensorValueType.Vector)
                        CompassVector = e.Value as DeviceMotion.Plugin.Abstractions.MotionVector;
                    else
                        CompassSingle = e.Value.Value.GetValueOrDefault();
                    break;
                default:
                    break;
            }

            //System.Diagnostics.Debug.WriteLine($"{e.SensorType.ToString()} {e.ValueType.ToString()} {e.Value.Value}");

        }

        private void CompassService_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CompassService.CurrentHeadingPropertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(CurrentHeadingPropertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
