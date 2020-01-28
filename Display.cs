using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace CMDR
{
    public class Display : Form
    {
        public Display(int sizeX, int sizeY)
        {
            Camera.SizeY = sizeY;
            Camera.SizeX = sizeX;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(sizeX, sizeY);
            this.Text = Assembly.GetEntryAssembly().FullName;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }
        public void Start()
        {
            Camera.Start();
            Render.SetDisplay(this);
            Updater.Init();
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
