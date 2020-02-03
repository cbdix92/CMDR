using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public static partial class Camera
    {
        public static float X { get; set; }
        public static float Y { get; set; }

        public static float Xvel { get; set; }
        public static float Yvel { get; set; }

        public static int SizeX { get; internal set; }
        public static int SizeY { get; internal set; }

        public static bool CameraRectCheck(GameObject gameObject)
        {
            // Check if "gameObject" is in camera view
            bool B1 = gameObject.Transform.X - X < SizeX;
            bool B2 = gameObject.Transform.Y - Y < SizeY;
            return B1 && B2;
        }
        internal static void Start()
        {
            // Scan all the render objects and determine if they are on the screen, then set them to be rendered
            foreach (GameObject gameObject in SceneManager.ActiveScene.RenderObjects)
            {
                if (CameraRectCheck(gameObject))
                {
                    SceneManager.ActiveScene.RenderActive.Add(gameObject);
                }
            }
        }
        internal static void CheckGameObjects()
        {
            // Check if GameObjects are moving or if the Camera is moving
            // If so, traverse render objects and place them in the active render list
            if (Math.Abs(Xvel) + Math.Abs(Yvel) + SceneManager.ActiveScene.ActiveGameObjects.Count != 0)
            {
                Scene Scene = SceneManager.ActiveScene;
                Scene.RenderActive.Clear();
                foreach (GameObject gameObject in Scene.RenderObjects)
                {
                    if (CameraRectCheck(gameObject))
                    {
                        Scene.RenderActive.Add(gameObject);
                    }
                }
            }
        }
    }
}
