using System;

using OBS;

namespace test
{
	public class Filter : ObsSource
	{
		public Filter(ObsSourceType type, string id, string name)
			: base(type, id, name)
		{
		}

		public Filter(ObsSourceType type, string id, string name, ObsData settings)
			: base(type, id, name, settings)
		{
		}

		public Filter(IntPtr instance)
			: base(instance)
		{
		}
	}
}
