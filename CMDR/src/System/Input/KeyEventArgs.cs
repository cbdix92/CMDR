

namespace CMDR.System
{
	public struct KeyEventArgs
	{
		Key Key;
		byte ModCode;
		long Time;

		public KeyEventArgs(Key key, byte modCode, long time)
		{
			(Key, ModCode, Time) = (key, modCode, time);
		}
	}
}
