using System;
using System.IO;
using SQLite;

namespace WeightTracker
{
	public static class Utilities
	{
		public static string ApplicationDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/WeightTracker";
		public static string DatabasePath = Utilities.ApplicationDirectory + "/weight.db3";

		public static void CreateApplicationDirectory()
		{
			if (!Directory.Exists (Utilities.ApplicationDirectory))
				Directory.CreateDirectory (Utilities.ApplicationDirectory);
		}

		public static void CreateTables()
		{
			using (SQLiteConnection conn = new SQLiteConnection(Utilities.DatabasePath))
			{
				conn.CreateTable<BodyMeasurements> ();
			}
		}
	}
}

