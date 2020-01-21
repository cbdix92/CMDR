using CMDR;
using System;
using System.Windows.Input;

namespace Test
{
    class Program
    {
        private static Display _display;
        public static Scene TestScene;

        public static GameObject GameObject1;
        public static GameObject GameObject2;

        public static int _speed = 2;

        public static CMDR.Image TestImage;
        private static CMDR.Image _projectileImage;
        
        [STAThread]
        static void Main(string[] args)
        {
            _display = new Display(1920 / 2, 1080 / 2);

            TestScene = new Scene();

            GameObject1 = TestScene.AddGameObject();
            GameObject2 = TestScene.AddGameObject(100, 300, 0);

            TestImage = new CMDR.Image("Test.bmp");
            _projectileImage = new CMDR.Image("Projectile.png");

            GameObject1.AddComponet(TestImage);
            GameObject2.AddComponet(TestImage);

            GameObject1.Collider = true;
            GameObject2.Collider = true;

            KeyListener.AddKeyBind(Key.W, () => { GameObject1.Transform.Yvel += -_speed; }, () => { GameObject1.Transform.Yvel -= -_speed; });
            KeyListener.AddKeyBind(Key.A, () => { GameObject1.Transform.Xvel += -_speed; }, () => { GameObject1.Transform.Xvel -= -_speed; });
            KeyListener.AddKeyBind(Key.S, () => { GameObject1.Transform.Yvel += _speed; }, () => { GameObject1.Transform.Yvel -= _speed; });
            KeyListener.AddKeyBind(Key.D, () => { GameObject1.Transform.Xvel += _speed; }, () => { GameObject1.Transform.Xvel -= _speed; });
            KeyListener.AddKeyBind(Key.Space, () => { TestScene.AddGameObject(new Projectile(TestScene,GameObject1, _projectileImage)); });

            _display.Start();
        }

        public class Projectile : GameObject
        {
            public Projectile(Scene scene, GameObject parent, Component image) : base (scene, parent.Transform.X, parent.Transform.Y, 0)
            {
                base.Transform.Xvel += 10;
                base.AddComponet(image);
            }
        }
    }
}
