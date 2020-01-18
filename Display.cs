using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace CMDR
{
    internal class TooManyDisplaysException : Exception
    {
        public TooManyDisplaysException(string message) : base(message)
        {
        }
    }
    internal static class DisplayController
    {
        // Ensure that only one display object is created
        private static bool _isDisplayInstanced;
        internal static bool IsDisplayInstanced
        {
            get => _isDisplayInstanced;
            set
            {
                if (value && !_isDisplayInstanced) _isDisplayInstanced = value;

                else if (value && _isDisplayInstanced) throw new TooManyDisplaysException("Only one Display object can be created!");
            }
        }
    }
    public class Display : Form
    {
        public Display(int sizeX, int sizeY)
        {
            DisplayController.IsDisplayInstanced = true;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(sizeX, sizeY);
            this.Text = Assembly.GetEntryAssembly().FullName;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            Render.Init(this);
            Updater.Init();
            SpatialIndexer.Init();

        }
        public void Start()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
