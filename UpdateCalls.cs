using System;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Render
    {
        public static void Update(object caller, EventArgs e)
        {
			if (Scene != null)
			{
				ClearScreen();
				ScreenBuffer();
				Draw();
			}
        }
    }

    internal static partial class Physics
    {
        public static void Update(object caller, EventArgs e)
        {
            if (SceneManager.ActiveScene == null || SceneManager.ActiveScene.ActiveGameObjects.Count == 0) return;

            Scene Scene = SceneManager.ActiveScene;

            List<GameObject> ActiveObjects = new List<GameObject>(Scene.ActiveGameObjects);

            foreach (GameObject gameObject in ActiveObjects)
            {
				if (gameObject.Static)
				{
					Scene.ActiveGameObjects.Remove(gameObject);
					gameObject.Transform.Xvel = 0;
					gameObject.Transform.Yvel = 0;
					continue;
				}
                gameObject.Move();

                if (gameObject.Collider)
                {
                    SpatialIndexer.CalcGridPos(gameObject);
                    CheckCollision(gameObject);
                }

                if (gameObject.HasZeroVelocity()) gameObject.Active = false;
            }
        }
    }

    public static partial class KeyListener
    {
        internal static void Update(object caller, EventArgs e)
        {
            foreach(KeyBind KeyBind in KeyBinds)
            {
                KeyBind.Detect();
            }
        }

    }

    public static partial class Camera
    {
        internal static void Update(object caller, EventArgs e)
        {
            X += Xvel;
            Y += Yvel;

            CheckGameObjects();
        }
    }
}
