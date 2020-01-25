using System;
using System.IO;

namespace CMDR.Components
{
    public class Image : Component
    {
        public StateMachine States;
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
        public Image(System.Drawing.Bitmap bitmap) : base (ComponentType.Image)
        {
            _image = bitmap;
        }
        public void Load(string src)
        {
            try
            {
                States.NewState(System.Drawing.Image.FromFile(src));
            }
            catch(FileNotFoundException)
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
    public class ImageData
    {
        public System.Drawing.Image Image { get; private set; }
        
        public ImageData(System.Drawing.Image image)
        {
            Image = image;
        }
    }
}
