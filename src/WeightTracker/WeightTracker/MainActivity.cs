using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.IO;
using System.Collections.Generic;

namespace WeightTracker
{
	[Activity (Label = "WeightTracker", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private ListView _measurementListView = null;
		private MeasurementAdapter _adapter = null;

		private TextView _dateTextView = null;
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

		private TextView _weightChange = null;
		private TextView _leftArmChange = null;
		private TextView _rightArmChange = null;
		private TextView _chestChange = null;
		private TextView _waistChange = null;
		private TextView _absChange = null;
		private TextView _hipsChange = null;
		private TextView _leftThighChange = null;
		private TextView _rightThighChange = null;
		private TextView _leftCalfChange = null;
		private TextView _rightCalfChange = null;

		private BodyMeasurements _lastMeasurement = null;
		private DateTime _currentDate = DateTime.Today;

		private List<BodyMeasurements> _bodyMeasurements = null;

		private const int DATE_DIALOG_ID = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			Utilities.CreateApplicationDirectory ();
			Utilities.CreateTables ();
			
            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            var saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            saveButton.Click += SaveMeasurements;

			_dateTextView = FindViewById<TextView> (Resource.Id.Date);
			_weightTextBox = FindViewById<EditText> (Resource.Id.WeightTextBox);
			_leftArmTextBox = FindViewById<EditText> (Resource.Id.LeftArmTextBox);
			_rightArmTextBox = FindViewById<EditText> (Resource.Id.RightArmTextBox);
			_chestTextBox = FindViewById<EditText> (Resource.Id.ChestTextBox);
			_waistTextBox = FindViewById<EditText> (Resource.Id.WaistTextBox);
			_absTextBox = FindViewById<EditText> (Resource.Id.AbsTextBox);
			_hipsTextBox = FindViewById<EditText> (Resource.Id.HipsTextBox);
			_leftThighTextBox = FindViewById<EditText> (Resource.Id.LeftThighTextBox);
			_rightThighTextBox = FindViewById<EditText> (Resource.Id.RightThighTextBox);
			_leftCalfTextBox = FindViewById<EditText> (Resource.Id.LeftCalfTextBox);
			_rightCalfTextBox = FindViewById<EditText> (Resource.Id.RightCalfTextBox);

			_weightChange = FindViewById<TextView> (Resource.Id.WeightChange);
			_leftArmChange = FindViewById<TextView> (Resource.Id.LeftArmChange);
			_rightArmChange = FindViewById<TextView> (Resource.Id.RightArmChange);
			_chestChange = FindViewById<TextView> (Resource.Id.ChestChange);
			_waistChange = FindViewById<TextView> (Resource.Id.WaistChange);
			_absChange = FindViewById<TextView> (Resource.Id.AbsChange);
			_hipsChange = FindViewById<TextView> (Resource.Id.HipsChange);
			_leftThighChange = FindViewById<TextView> (Resource.Id.LeftThighChange);
			_rightThighChange = FindViewById<TextView> (Resource.Id.RightThighChange);
			_leftCalfChange = FindViewById<TextView> (Resource.Id.LeftCalfChange);
			_rightCalfChange = FindViewById<TextView> (Resource.Id.RightCalfChange);

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

			_dateTextView.Click += ShowDatePicker;

			_measurementListView = FindViewById<ListView> (Resource.Id.MeasurementsListView);
			_bodyMeasurements = BodyMeasurements.GetAllMeasurements ();
			_adapter = new MeasurementAdapter (this, _bodyMeasurements);
			_measurementListView.Adapter = _adapter;
			_adapter.NotifyDataSetChanged ();

			UpdateDateView ();
		}

		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
				case DATE_DIALOG_ID:
					return new DatePickerDialog (this, OnDateSet, _currentDate.Year, _currentDate.Month - 1, _currentDate.Day); 
			}
			return null;
		}

		private void ShowDatePicker (object sender, EventArgs e)
		{
			ShowDialog (DATE_DIALOG_ID);
		}

		private void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			_currentDate = e.Date;
			UpdateDateView ();
		}

		private void UpdateDateView()
		{
			_dateTextView.Text = _currentDate.ToString ("MM/dd/yyyy");
		}

		private void WeightLostFocus (object sender, View.FocusChangeEventArgs e)
		{
			if (e.HasFocus)
				return;

			if (_lastMeasurement == null)
				_lastMeasurement = BodyMeasurements.GetLastMeasurement ();

			TextView changeTextView = null;
			double previousValue = 0;
			double newValue = 0;

			if (sender == _weightTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.WeightChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.Weight : 0;
				newValue = _weightTextBox.Text.AsDouble ();
			}
			else if (sender == _leftArmTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.LeftArmChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.LeftArm : 0;
				newValue = _leftArmTextBox.Text.AsDouble ();
			}
			else if (sender == _rightArmTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.RightArmChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.RightArm : 0;
				newValue = _rightArmTextBox.Text.AsDouble ();
			}
			else if (sender == _chestTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.ChestChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.Chest : 0;
				newValue = _chestTextBox.Text.AsDouble ();
			}
			else if (sender == _waistTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.WaistChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.Waist : 0;
				newValue = _waistTextBox.Text.AsDouble ();
			}
			else if (sender == _absTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.AbsChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.Abdominal : 0;
				newValue = _absTextBox.Text.AsDouble ();
			}
			else if (sender == _hipsTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.HipsChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.Hips : 0;
				newValue = _hipsTextBox.Text.AsDouble ();
			}
			else if (sender == _leftThighTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.LeftThighChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.LeftThigh : 0;
				newValue = _leftThighTextBox.Text.AsDouble ();
			}
			else if (sender == _rightThighTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.RightThighChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.RightThigh : 0;
				newValue = _rightThighTextBox.Text.AsDouble ();
			}
			else if (sender == _leftCalfTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.LeftCalfChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.LeftCalf : 0;
				newValue = _leftCalfTextBox.Text.AsDouble ();
			}
			else if (sender == _rightCalfTextBox)
			{
				changeTextView = FindViewById<TextView> (Resource.Id.RightCalfChange);
				previousValue = (_lastMeasurement != null) ? _lastMeasurement.RightCalf : 0;
				newValue = _rightCalfTextBox.Text.AsDouble ();
			}

			PopulateChange (changeTextView, previousValue, newValue);
		}

		private void PopulateChange(TextView view, double previousValue, double newValue)
		{
			double result = newValue - previousValue;
			view.Text = (previousValue == 0) ? "0" : string.Format ("{0}{1}", (result < 0) ? "-" : "", result);

			Color color = Color.White;
			if (result > 0)
				color = Color.Red;
			else if (result < 0)
				color = Color.Green;

			if (previousValue == 0)
				color = Color.White;

			view.SetTextColor (color);
		}

		private void SaveMeasurements (object sender, EventArgs e)
        {
			var bodyMeas = new BodyMeasurements ();
			bodyMeas.Date = _currentDate;

			if (!string.IsNullOrEmpty (_weightTextBox.Text))
				bodyMeas.Weight =  _weightTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_leftArmTextBox.Text))
				bodyMeas.LeftArm = _leftArmTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_rightArmTextBox.Text))
				bodyMeas.RightArm = _rightArmTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_chestTextBox.Text))
				bodyMeas.Chest = _chestTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_waistTextBox.Text))
				bodyMeas.Waist = _waistTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_absTextBox.Text))
				bodyMeas.Abdominal = _absTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_hipsTextBox.Text))
				bodyMeas.Hips = _hipsTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_leftThighTextBox.Text))
				bodyMeas.LeftThigh = _leftThighTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_rightThighTextBox.Text))
				bodyMeas.RightThigh = _rightThighTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_leftCalfTextBox.Text))
				bodyMeas.LeftCalf = _leftCalfTextBox.Text.AsDouble ();

			if (!string.IsNullOrEmpty (_rightCalfTextBox.Text))
				bodyMeas.RightCalf = _rightCalfTextBox.Text.AsDouble ();

			bodyMeas.Save ();
			_lastMeasurement = bodyMeas;

			ClearScreen ();
        }

		private void ClearScreen()
		{
			_weightTextBox.Text = string.Empty;
			_leftArmTextBox.Text = string.Empty;
			_rightArmTextBox.Text = string.Empty;
			_chestTextBox.Text = string.Empty;
			_waistTextBox.Text = string.Empty;
			_absTextBox.Text = string.Empty;
			_hipsTextBox.Text = string.Empty;
			_leftThighTextBox.Text = string.Empty;
			_rightThighTextBox.Text = string.Empty;
			_leftCalfTextBox.Text = string.Empty;
			_rightCalfTextBox.Text = string.Empty;

			_weightChange.Text = "0";
			_weightChange.SetTextColor (Color.White);
			_leftArmChange.Text = "0";
			_leftArmChange.SetTextColor (Color.White);
			_rightArmChange.Text = "0";
			_rightArmChange.SetTextColor (Color.White);
			_chestChange.Text = "0";
			_chestChange.SetTextColor (Color.White);
			_waistChange.Text = "0";
			_waistChange.SetTextColor (Color.White);
			_absChange.Text = "0";
			_absChange.SetTextColor (Color.White);
			_hipsChange.Text = "0";
			_hipsChange.SetTextColor (Color.White);
			_leftThighChange.Text = "0";
			_leftThighChange.SetTextColor (Color.White);
			_rightThighChange.Text = "0";
			_rightThighChange.SetTextColor (Color.White);
			_leftCalfChange.Text = "0";
			_leftCalfChange.SetTextColor (Color.White);
			_rightCalfChange.Text = "0";
			_rightCalfChange.SetTextColor (Color.White);

			_dateTextView.RequestFocus ();
		}
	}
}


