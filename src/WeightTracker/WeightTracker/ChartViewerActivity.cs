using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BarChart;
using Android.Graphics;

namespace WeightTracker
{
    public enum ChartDataType
    {
        Weight_By_Date = 0,
        Weight_By_Month,
        Size_By_Date,
        Size_By_Month
    }

    [Activity(Label = "Weight Tracker")]			
    public class ChartViewerActivity : Activity
    {
        private BarChartView _barChart = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.ChartViewer);

            // Create your application here
            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Landscape;

            var spinner = FindViewById<Spinner>(Resource.Id.BarChartDataSpinner);
            spinner.ItemSelected += BarChartDataChanged;
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.ChartDataArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            _barChart = FindViewById<BarChartView>(Resource.Id.MeasurementBarChart);
            _barChart.BarWidth = 70;
            _barChart.BarOffset = 15;

            PopulateWeightByDate();
        }

        private void BarChartDataChanged (object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;

            switch(e.Position)
            {
                case (int)ChartDataType.Weight_By_Month:
                    PopulateWeightByMonth();
                    break;
                case (int)ChartDataType.Weight_By_Date:
                    PopulateWeightByDate();
                    break;
                case (int)ChartDataType.Size_By_Month:
                    break;
                case (int)ChartDataType.Size_By_Date:
                    break;
            }
        }

        private void PopulateWeightByDate()
        {
            var items = new List<BarModel>();
            var data = BodyMeasurements.GetWeightByDate();
            foreach(var d in data)
            {
                var bar = new BarModel {
                    Value = (float)d.Value,
                    Color = (d.Value <= 0) ? Color.Green : Color.Red,
                    Legend = d.Key,
                    ValueCaptionHidden = false,
                    ValueCaption = d.Value.ToString()
                };
                items.Add(bar);
            }

            CreateBarChart(items);
        }

        private void PopulateWeightByMonth()
        {
            var items = new List<BarModel>();
            var data = BodyMeasurements.GetWeightByMonth();
            foreach(var d in data)
            {
                var bar = new BarModel {
                    Value = (float)d.Value,
                    Color = (d.Value <= 0) ? Color.Green : Color.Red,
                    Legend = d.Key,
                    ValueCaptionHidden = false,
                    ValueCaption = d.Value.ToString()
                };
                items.Add(bar);
            }

            CreateBarChart(items);
        }

        private void CreateBarChart(List<BarModel> items)
        {
            var chart = new BarChartView (this) {
                ItemsSource = items,
                BarCaptionFontSize = 22,
                BarWidth = 70,
                BarOffset = 35,
                LegendFontSize = 18
            };

            var layout = FindViewById<LinearLayout>(Resource.Id.BarChartLinearLayout);

            if (layout.ChildCount > 0)
                layout.RemoveAllViewsInLayout();

            layout.AddView(chart, new ViewGroup.LayoutParams (
                ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent));
        }
    }
}

