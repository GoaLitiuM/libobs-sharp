using System;

using OBS;

namespace test
{
	public class Transition : ObsSource
	{
		public Transition(string id, string name) : base(ObsSourceType.Transition, id, name)
		{
		}

		public Transition(string id, string name, ObsData settings) : base(ObsSourceType.Transition, id, name, settings)
		{
		}

		public Transition(IntPtr instance) : base(instance)
		{
		}
	}
}