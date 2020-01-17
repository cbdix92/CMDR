using CMDR;
using System;
using System.Windows.Input;

namespace Test
{
    class Program
    {
        public static Display _Display;
        public static Scene TestScene;

        public static GameObject GameObject1;
        public static GameObject GameObject2;

        public static int _speed = 5;

        public static CMDR.Image TestImage;
        
        [STAThread]
        static void Main(string[] args)
        {
            _Display = new Display(1920 / 2, 1080 / 2);
            SpatialIndexer.Init();
            Updater.Init();
            TestScene = new Scene();

            GameObject1 = TestScene.AddGameObject();
            GameObject2 = TestScene.AddGameObject(100, 300, 0);

            TestImage = new CMDR.Image("Test.bmp");

            GameObject1.AddComponet(TestImage);
            GameObject2.AddComponet(TestImage);

            GameObject1.Collider = true;
            GameObject2.Collider = true;

            KeyListener.AddKeyBind(Key.W, () => { GameObject1.Transform.Yvel = -_speed; }, () => { GameObject1.Transform.Yvel = 0; });
            KeyListener.AddKeyBind(Key.A, () => { GameObject1.Transform.Xvel = -_speed; }, () => { GameObject1.Transform.Xvel = 0; });
            KeyListener.AddKeyBind(Key.S, () => { GameObject1.Transform.Yvel = _speed; }, () => { GameObject1.Transform.Yvel = 0; });
            KeyListener.AddKeyBind(Key.D, () => { GameObject1.Transform.Xvel = _speed; }, () => { GameObject1.Transform.Xvel = 0; });

            _Display.Start();
        }
    }
}
