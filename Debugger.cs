using System;
using System.Drawing;
using System.Windows.Input;

namespace CMDR
{
    public static class Debugger
    {
        private static bool _enableDebugger;
        public static bool EnableDebugger
        {
            get => _enableDebugger;
            set
            {
                if (value != _enableDebugger)
                {
                    if (value)
                        KeyListener.AddKeyBind(Key.F5, () => { DrawSpatialLines = !DrawSpatialLines; });
                    else if (!value)
                        KeyListener.RemoveKeyBind(Key.F5);
                }
                _enableDebugger = value;
            }
        }
        internal static bool DrawSpatialLines { get; set; }
        internal static void Draw()
        {
            if (DrawSpatialLines)
            {
                // Draw SpatialIndexer Grid Lines
                foreach ((int, int) i in SpatialIndexer.GridCells.Keys)
                {
                    int s = SpatialIndexer.CellSize;
                    int x = i.Item2 * s;
                    int y = i.Item1 * s;
                    int cx = x + s / 2;
                    int cy = y + s / 2;
                    Render.Buffer.Graphics.DrawRectangle(new Pen(Brushes.Red), new Rectangle(x - (int)Camera.X, y - (int)Camera.Y, s, s));
                    Render.Buffer.Graphics.DrawString(SpatialIndexer.GridCells[i].Cache.Count.ToString(), Render.Display.Font, Brushes.Red, cx - Camera.X, cy - Camera.Y);
                    Render.Buffer.Graphics.DrawString("("+i.Item1 + ", " + i.Item2+")", Render.Display.Font, Brushes.Red, x - (int)Camera.X + 5, y - (int)Camera.Y + 5);
                }
            }
        }
    }
}
