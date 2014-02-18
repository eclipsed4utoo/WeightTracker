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
		private DatePicker _datePicker = null;
		private EditText _weightTextBox = null;
		private EditText _leftArmTextBox = null;
		private EditText _rightArmTextBox = null;
		private EditText _chestTextBox = null;
		private EditText _waistTextBox = null;
		private EditText _absTextBox = null;
		private EditText _hipsTextBox = null;
		private EditText _leftThighTextBox = null;
		private EditText _rightThighTextBox = null;
		private EditText _leftCalfTextBox = null;
		private EditText _rightCalfTextBox = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			
            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            var saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            saveButton.Click += SaveMeasurements;

			_datePicker = FindViewById<DatePicker> (Resource.Id.Date);
			_weightTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_leftArmTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_rightArmTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_chestTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_waistTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_absTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_hipsTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_leftThighTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_rightThighTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_leftCalfTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_rightCalfTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);

			_weightTextBox.FocusChange += WeightLostFocus;
			_leftArmTextBox.FocusChange += WeightLostFocus;
			_rightArmTextBox.FocusChange += WeightLostFocus;
			_chestTextBox.FocusChange += WeightLostFocus;
			_waistTextBox.FocusChange += WeightLostFocus;
			_absTextBox.FocusChange += WeightLostFocus;
			_hipsTextBox.FocusChange += WeightLostFocus;
			_leftThighTextBox.FocusChange += WeightLostFocus;
			_rightThighTextBox.FocusChange += WeightLostFocus;
			_leftCalfTextBox.FocusChange += WeightLostFocus;
			_rightCalfTextBox.FocusChange += WeightLostFocus;
		}

		private void WeightLostFocus (object sender, View.FocusChangeEventArgs e)
		{

		}

		private void SaveMeasurements (object sender, EventArgs e)
        {
			var bodyMeas = new BodyMeasurements ();

			if (!string.IsNullOrEmpty (_weightTextBox.Text))
				bodyMeas.Weight = double.Parse (_weightTextBox.Text);

			if (!string.IsNullOrEmpty (_leftArmTextBox.Text))
				bodyMeas.Weight = double.Parse (_leftArmTextBox.Text);

			if (!string.IsNullOrEmpty (_rightArmTextBox.Text))
				bodyMeas.Weight = double.Parse (_rightArmTextBox.Text);

			if (!string.IsNullOrEmpty (_chestTextBox.Text))
				bodyMeas.Weight = double.Parse (_chestTextBox.Text);

			if (!string.IsNullOrEmpty (_waistTextBox.Text))
				bodyMeas.Weight = double.Parse (_waistTextBox.Text);

			if (!string.IsNullOrEmpty (_absTextBox.Text))
				bodyMeas.Weight = double.Parse (_absTextBox.Text);

			if (!string.IsNullOrEmpty (_hipsTextBox.Text))
				bodyMeas.Weight = double.Parse (_hipsTextBox.Text);

			if (!string.IsNullOrEmpty (_leftThighTextBox.Text))
				bodyMeas.Weight = double.Parse (_leftThighTextBox.Text);

			if (!string.IsNullOrEmpty (_rightThighTextBox.Text))
				bodyMeas.Weight = double.Parse (_rightThighTextBox.Text);

			if (!string.IsNullOrEmpty (_leftCalfTextBox.Text))
				bodyMeas.Weight = double.Parse (_leftCalfTextBox.Text);

			if (!string.IsNullOrEmpty (_rightCalfTextBox.Text))
				bodyMeas.Weight = double.Parse (_rightCalfTextBox.Text);

			bodyMeas.Save ();
        }
	}
}


