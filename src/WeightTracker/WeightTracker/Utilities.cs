using System;

namespace WeightTracker
{
	public static class Utilities
	{
		public static string ApplicationDirectory = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/WeightTracker";
		public static string DatabasePath = Utilities.ApplicationDirectory + "/weight.db3";
	}
}

