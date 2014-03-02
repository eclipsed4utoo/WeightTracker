using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using Android.Content;

namespace WeightTracker
{
	public class MeasurementAdapter : BaseAdapter<BodyMeasurements>
	{
		private ListView _parent = null;
		private LayoutInflater layoutInflater;
		private List<BodyMeasurements> _measurements = null;

		public MeasurementAdapter (Context context, List<BodyMeasurements> measurements)
		{
			_measurements = measurements;
			layoutInflater = LayoutInflater.From (context);
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
            return _measurements[position].ID;
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			ViewHolder holder = null;
			if (convertView == null)
			{
				convertView = layoutInflater.Inflate (Resource.Layout.MeasurementRow, null);
				holder = new ViewHolder ();
				holder.DateTextView = convertView.FindViewById<TextView> (Resource.Id.RowDate);
				holder.DeleteImageButton = convertView.FindViewById<ImageButton> (Resource.Id.RowDeleteButton);
				holder.TotalChangeTextView = convertView.FindViewById<TextView> (Resource.Id.RowTotalChange);
				holder.WeightChangeTextView = convertView.FindViewById<TextView> (Resource.Id.RowWeightChange);

				holder.DeleteImageButton.Click += DeleteRow;

				convertView.Tag = holder;
			}
			else
			{
				holder = (ViewHolder)convertView.Tag;
			}

			var mea = _measurements [position];
			holder.DateTextView.Text = mea.Date.ToString ("MM/dd/yyyy");
            holder.TotalChangeTextView.Text = mea.TotalSizeChange.ToString();
            holder.WeightChangeTextView.Text = mea.TotalWeightChange.ToString();

			_parent = (ListView)parent;

			return convertView;
		}

		private void DeleteRow (object sender, EventArgs e)
		{
			int position = _parent.GetPositionForView ((View)sender);
            var measurement = _measurements[position];
			_measurements.RemoveAt (position);
            measurement.Delete();
			this.NotifyDataSetChanged ();
		}

		public override int Count {
			get {
				return _measurements.Count;
			}
		}

		public override BodyMeasurements this [int index] {
			get {
				return _measurements [index];
			}
		}

		#endregion


		private class ViewHolder : Java.Lang.Object
		{
			public TextView DateTextView { get; set; }
			public TextView TotalChangeTextView { get; set; }
			public TextView WeightChangeTextView { get; set; }
			public ImageButton DeleteImageButton { get; set; }
		}
	}
}

