using System.Diagnostics;
using System.Collections.Generic;
using CMDR.Native;

namespace CMDR.System
{

    public static class GameLoop
    {
        public static long GameTime => _time.ElapsedTicks;

        private static Stopwatch _time = new Stopwatch();

        private static List<Updater> _updaters = new List<Updater>();

        public static void Start()
        {

            _time.Start();

            //CreateUpdater(1000, Render.Update);
            //CreateUpdater(100, Physics.Update);
            //CreateUpdater(100, Input.Update);

            while(Win.HandleMessages())
            {
                foreach (Updater updater in _updaters)
                    updater.Update(GameTime);
            }

            Win.DestroyWindow(Win.CurrentWindow);

        }
        public static void CreateUpdater(int perSecond, UpdateHandler update)
        {
            Updater updater = new Updater();
            updater.UpdatesPerSecond = perSecond;
            updater.Handler += update;
            _updaters.Add(updater);
        }
    }
}
