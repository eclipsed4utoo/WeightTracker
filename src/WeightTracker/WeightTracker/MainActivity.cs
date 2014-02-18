using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace WeightTracker
{
	[Activity (Label = "WeightTracker", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            var saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            saveButton.Click += SaveMeasurements;
		}

        private SaveMeasurementseClick (object sender, EventArgs e)
        {
            
        }
	}
}


