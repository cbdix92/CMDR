using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace CMDR
{
    public class Display : Form
    {
        //public static Display CDisplay = new Display(1280/2,1080/2);
        public Display(int sizeX, int sizeY)
        {
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Size = new Size(sizeX, sizeY);
            this.Text = Assembly.GetEntryAssembly().FullName;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        }
        public void Start()
        {
            Render.SetDisplay(this);
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
