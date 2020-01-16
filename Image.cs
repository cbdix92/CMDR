using System;
using System.IO;

namespace CMDR
{
    public class Image : Componet
    {
        private System.Drawing.Image _image;
        public Image(string src)
        {
            ID = "Image";
            try
            {
                _image = System.Drawing.Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"The file: '{src}' Could not be found!");
            }
        }
        public override System.Drawing.Image GetImage()
        {
            return _image;
        }
    }
}
