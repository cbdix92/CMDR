using CMDR;
using CMDR.Components;
using System;
using System.Windows.Input;

namespace Test
{
    class Program
    {
        public static Display Dispaly;
        public static Scene TestWorld;

        public static Player Player1;

        public static float _speed = 6.0F;
        public static float CameraSpeed = 6.0F;

        public static RenderData PlayerImageData;
        public static RenderState PlayerImage1;
        public static RenderState PlayerImage2;

        public static RenderData ProjectileImageData;

        public static PhysicsConstraints PlayerPhysics;
        public static PhysicsConstraints StaticPhysics;
        public static PhysicsConstraints ProjectilePhysics;
        
        [STAThread]
        static void Main(string[] args)
        {
            Dispaly = new Display(1920, 1080);

            TestWorld = new Scene();

            GameObject Ground = TestWorld.AddGameObject(0, 1000, 0);

            // Load Scene Image Data
            RenderData SceneImageData = new RenderData();
            RenderState GroundImage = SceneImageData.LoadFile("Ground.png");

            // Load Player Image Data
            PlayerImageData = new RenderData();
            PlayerImage1 = PlayerImageData.LoadFile("Test.bmp");
            PlayerImage2 = PlayerImageData.LoadFile("StateTest.png");

            // Load Projectile Image Data
            ProjectileImageData = new RenderData();
            ProjectileImageData.LoadFile("Projectile.png");

            // Create PhysicsConstraints
            PlayerPhysics = new PhysicsConstraints(TestWorld);
            StaticPhysics = new PhysicsConstraints(TestWorld);
            ProjectilePhysics = new PhysicsConstraints(TestWorld);
            
            PlayerPhysics.Collider = true;
            StaticPhysics.Static = true;
            ProjectilePhysics.Collider = true;
            ProjectilePhysics.OnCollision += OnProjectileCollision;


            Player1 = new Player(TestWorld);

            Ground.Use(SceneImageData);
            Ground.Use(StaticPhysics);

            
            KeyListener.AddKeyBind(Key.Up, () => { Camera.Yvel += -CameraSpeed; }, () => { Camera.Yvel -= -CameraSpeed; });
            KeyListener.AddKeyBind(Key.Left, () => { Camera.Xvel += -CameraSpeed; }, () => { Camera.Xvel -= -CameraSpeed; });
            KeyListener.AddKeyBind(Key.Down, () => { Camera.Yvel += CameraSpeed; }, () => { Camera.Yvel -= CameraSpeed; });
            KeyListener.AddKeyBind(Key.Right, () => { Camera.Xvel += CameraSpeed; }, () => { Camera.Xvel -= CameraSpeed; });

            Debugger.EnableDebugger = true;
            Dispaly.Start();
        }
        public static void OnProjectileCollision(PhysicsConstraints caller, GameObject parent, GameObject collider)
        {
            collider.Transform.X += 10.0F;
            parent.Dispose();
        }

        public class Projectile : GameObject
        {
            public Projectile(Scene scene, GameObject parent) : base(scene, parent.Transform.X+parent.Width+1, parent.Transform.Y, parent.Transform.Z)
            {
                base.Transform.Xvel += 5.5F;
                base.Transform.Yvel += 1.5F;

                Use(ProjectileImageData);
                Use(ProjectilePhysics);

            }
        }
        public class Player : GameObject
        {
            public Player(Scene scene) : base (scene, 0, 0, 0)
            {
                // Components
                base.Use(PlayerImageData);
                base.Use(PlayerPhysics);

                // Player KeyBinds
                KeyListener.AddKeyBind(Key.W, () => { this.Transform.Yvel += -_speed; }, () => { this.Transform.Yvel -= -_speed; });
                KeyListener.AddKeyBind(Key.A, () => { this.Transform.Xvel += -_speed; }, () => { this.Transform.Xvel -= -_speed; });
                KeyListener.AddKeyBind(Key.S, () => { this.Transform.Yvel += _speed; }, () => { this.Transform.Yvel -= _speed; });
                KeyListener.AddKeyBind(Key.D, () => { this.Transform.Xvel += _speed; }, () => { this.Transform.Xvel -= _speed; });
                KeyListener.AddKeyBind(Key.Space, () => { Shoot(); });
                KeyListener.AddKeyBind(Key.Q, () => { PlayerImage2.State = this; }, () => { PlayerImage1.State = this; });
            }
            public void Shoot()
            {
                TestWorld.AddGameObject(new Projectile(TestWorld, this));
            }
        }
    }
}
