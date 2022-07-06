using System.Diagnostics;

namespace CMDR.System
{
    internal sealed class Updater
    {
        #region PRIVATE_MEMBERS

        /// <summary>
        /// The last time this Updater was called.
        /// </summary>
        private long _lastUpdate;

        /// <summary>
        /// The number of ticks that are needed before the next update is called.
        /// </summary>
        private long _ticksBeforeNextUpdate;

        #endregion

        /// <summary>
        /// Sets the number of updates that are called each second.
        /// </summary>
        internal int UpdatesPerSecond
        {
            get 
            {
                return (int)(_ticksBeforeNextUpdate * Stopwatch.Frequency);
            }
            set 
            { 
                _ticksBeforeNextUpdate = Stopwatch.Frequency / value; 
            }
        }

        internal event UpdateHandler Handler;

        /// <summary>
        /// Calls the UpdateHandler at the appropiate time.
        /// </summary>
        /// <param name="ticks"> The current number of elapsed ticks. </param>
        internal void Update(long ticks)
        {
            if (_lastUpdate + _ticksBeforeNextUpdate <= ticks && Handler != null)
            {
                _lastUpdate = ticks;
                Handler(ticks);
            }
        }
    }
}
