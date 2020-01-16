using System;
using System.Windows.Forms;
using System.Drawing;
namespace CMDR
{
    public class Display : Form
    {
        public Display(int sizeX, int sizeY)
        {
            this.MaximizeBox = false;
            this.Size = new Size(sizeX, sizeY);
            this.Text = System.Reflection.Assembly.GetExecutingAssembly().FullName;


            Render.Display = this;
            Render.GFX = this.CreateGraphics();

        }
        public void Start()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
