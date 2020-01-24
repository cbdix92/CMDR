using System;
using System.IO;

namespace CMDR
{
    public class Image : Component
    {
        private System.Drawing.Image _image;
        public Image(string src) : base (ComponentType.Image)
        {
            try
            {
                _image = System.Drawing.Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"The file: '{src}' Could not be found!");
            }
        }
        public override System.Drawing.Image GetRenderData()
        {
            return _image;
        }
        public override void Dispose()
        {
            _image.Dispose();
        }
    }
}
