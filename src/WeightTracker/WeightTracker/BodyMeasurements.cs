using System;
using SQLite;

namespace WeightTracker
{
	public class BodyMeasurements
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }

		public DateTime Date { get; set; }
		public double Weight { get; set; }
		public double LeftArm { get; set; }
		public double RightArm { get; set; }
		public double Chest { get; set; }
		public double Waist { get; set; }
		public double Abdominal { get; set; }
		public double Hips { get; set; }
		public double LeftThigh { get; set; }
		public double RightThigh { get; set; }
		public double LeftCalf { get; set; }
		public double RightCalf { get; set; }

		public BodyMeasurements ()
		{
		}

		public void Save()
		{
			using (SQLiteConnection conn = new SQLiteConnection(Utilities.DatabasePath))
			{
				conn.Insert (this);
			}
		}
	}
}

