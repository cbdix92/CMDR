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

        public static float _speed = 2.5F;
        public static float CameraSpeed = 2.5F;

        public static RenderData PlayerImageData;
        private static RenderData _projectileImage;

        
        [STAThread]
        static void Main(string[] args)
        {
            _display = new Display(1920, 1080);

            TestScene = new Scene();

            GameObject1 = TestScene.AddGameObject();
            GameObject2 = TestScene.AddGameObject(100, 300, 0);

            PlayerImageData = new RenderData();
            RenderState PlayerImage1 = PlayerImageData.LoadFile("Test.bmp");
            RenderState PlayerImage2 = PlayerImageData.LoadFile("StateTest.png");

            _projectileImage = new RenderData();
            _projectileImage.LoadFile("Projectile.png");
            //RenderState ProjectileHandle = _projectileImage.LoadFile("Projectile.png");


            PlayerImageData.ParentTo(GameObject1);
            PlayerImageData.ParentTo(GameObject2);

            GameObject1.Collider = true;
            GameObject2.Collider = true;


            KeyListener.AddKeyBind(Key.W, () => { GameObject1.Transform.Yvel += -_speed; }, () => { GameObject1.Transform.Yvel -= -_speed; });
            KeyListener.AddKeyBind(Key.A, () => { GameObject1.Transform.Xvel += -_speed; }, () => { GameObject1.Transform.Xvel -= -_speed; });
            KeyListener.AddKeyBind(Key.S, () => { GameObject1.Transform.Yvel += _speed; }, () => { GameObject1.Transform.Yvel -= _speed; });
            KeyListener.AddKeyBind(Key.D, () => { GameObject1.Transform.Xvel += _speed; }, () => { GameObject1.Transform.Xvel -= _speed; });
            KeyListener.AddKeyBind(Key.Space, () => { TestScene.AddGameObject(new Projectile(TestScene, GameObject1, _projectileImage)); });
            KeyListener.AddKeyBind(Key.Q, () => { PlayerImage2.State = GameObject1; }, () => { PlayerImage1.State = GameObject1; });
            
            KeyListener.AddKeyBind(Key.Up, () => { Camera.Yvel += CameraSpeed; }, () => { Camera.Yvel -= CameraSpeed; });
            KeyListener.AddKeyBind(Key.Left, () => { Camera.Xvel += CameraSpeed; }, () => { Camera.Xvel -= CameraSpeed; });
            KeyListener.AddKeyBind(Key.Down, () => { Camera.Yvel += -CameraSpeed; }, () => { Camera.Yvel -= -CameraSpeed; });
            KeyListener.AddKeyBind(Key.Right, () => { Camera.Xvel += -CameraSpeed; }, () => { Camera.Xvel -= -CameraSpeed; });

            _display.Start();
        }

        public class Projectile : GameObject
        {
            public Projectile(Scene scene, GameObject parent, RenderData image) : base(scene, parent.Transform.X+parent.Width+1, parent.Transform.Y, parent.Transform.Z)
            {
                base.Transform.Xvel += 5.5F;
                image.ParentTo(this);
                base.Collider = true;
                PhysicsConstraints p = (PhysicsConstraints)base.Components[ComponentType.PhysicsConstraints];
                p.OnCollision += OnCollision;

            }
            private void OnCollision(GameObject collider)
            {
                collider.Transform.X += 20.5F;
                base.Transform.Xvel = 0;
                base.Collider = false;
                base.Dispose();
            }
        }
    }
}
