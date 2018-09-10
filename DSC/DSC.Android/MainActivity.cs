using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Android.Hardware;
using DSC.Services;

namespace DSC.Droid
{
    [Activity(Label = "DSC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ISensorEventListener
    {
        Android.Hardware.SensorManager mSensorManager;
        Android.Hardware.Sensor mRotationVectorSensor;
        Android.Hardware.Sensor mGeomagneticRotationVectorSensor;
        Android.Hardware.Sensor mAccelerometerSensor;
        Android.Hardware.Sensor mMagneticFieldSensor;

        float[] rotationMatrix = new float[9];
        float[] orientationAngles = new float[3];

        float[] mAccelerometerReading = new float[3];
        float[] mMagnetometerReading = new float[3];

        float[] mRotationMatrix = new float[9];
        float[] mOrientationAngles = new float[3];

        XYZ xyz;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());



            mSensorManager = GetSystemService(Android.Content.Context.SensorService) as Android.Hardware.SensorManager;
            mRotationVectorSensor = mSensorManager.GetDefaultSensor(Android.Hardware.SensorType.RotationVector);
            mGeomagneticRotationVectorSensor = mSensorManager.GetDefaultSensor(SensorType.GeomagneticRotationVector);
            mAccelerometerSensor = mSensorManager.GetDefaultSensor(SensorType.Accelerometer);
            mMagneticFieldSensor = mSensorManager.GetDefaultSensor(SensorType.MagneticField);

            mSensorManager.RegisterListener(this, mRotationVectorSensor, SensorDelay.Normal);
            mSensorManager.RegisterListener(this, mGeomagneticRotationVectorSensor, SensorDelay.Normal);
            mSensorManager.RegisterListener(this, mAccelerometerSensor, SensorDelay.Normal);
            mSensorManager.RegisterListener(this, mMagneticFieldSensor, SensorDelay.Normal);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        //ISensorEventListener
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            
        }

        //ISensorEventListener
        public void OnSensorChanged(SensorEvent e)
        {
            if (e.Sensor.Type == SensorType.Accelerometer)
            {
                e.Values.CopyTo(mAccelerometerReading, 0);
                //System.arraycopy(event.values, 0, mAccelerometerReading, 0, mAccelerometerReading.size);
            } else if (e.Sensor.Type == SensorType.MagneticField)
            {
                e.Values.CopyTo(mMagnetometerReading, 0);
                //System.arraycopy(event.values, 0, mMagnetometerReading, 0, mMagnetometerReading.size)
            }

            // Update rotation matrix, which is needed to update orientation angles.
            var shiz1 = SensorManager.GetRotationMatrix(
                    mRotationMatrix,
                    null,
                    mAccelerometerReading,
                    mMagnetometerReading
            );
            //// "mRotationMatrix" now has up-to-date information.
            //for (int i = 0; i < mRotationMatrix.Length; i++)
            //{
            //    System.Diagnostics.Debug.Write($"{i}: {mRotationMatrix[i]}");
            //}
            //System.Diagnostics.Debug.WriteLine("");

            var shiz2 = SensorManager.GetOrientation(mRotationMatrix, mOrientationAngles);
            for (int i = 0; i < shiz2.Length; i++)
            {
                System.Diagnostics.Debug.Write($"{i}: {shiz2[i]}");
            }
            System.Diagnostics.Debug.WriteLine("");
            // "mOrientationAngles" now has up-to-date information.
            //for (int i = 0; i < mOrientationAngles.Length; i++)
            //{
            //    System.Diagnostics.Debug.Write($"{i}: {mOrientationAngles[i]}");
            //}
            //System.Diagnostics.Debug.WriteLine("");
            xyz = new XYZ { X = shiz2[0], Y = shiz2[1], Z = shiz2[2] };
            Xamarin.Forms.MessagingCenter.Send(xyz, XYZ.MCUpdated);
        }
    }
}

