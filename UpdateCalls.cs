using System;
using System.Collections.Generic;

namespace CMDR
{
    internal static partial class Render
    {
        public static void Update(object caller, EventArgs e)
        {
            if (SceneManager.ActiveScene == null || SceneManager.ActiveScene.GameObjects[0] == null) return;
            ClearScreen();
            ScreenBuffer();
            Draw();
        }
    }

    internal static partial class Physics
    {
        public static void Update(object caller, EventArgs e)
        {
            if (SceneManager.ActiveScene == null || SceneManager.ActiveScene.ActiveGameObjects.Count == 0) return;

            Scene Scene = SceneManager.ActiveScene;

            List<GameObject> ActiveObjects = new List<GameObject>(Scene.ActiveGameObjects);

            foreach (GameObject GameObject in ActiveObjects)
            {
				if (GameObject.Static)
				{
					Scene.ActiveGameObjects.Remove(GameObject);
					GameObject.Transform.Xvel = 0;
					GameObject.Transform.Yvel = 0;
					continue;
				}
                GameObject.Move();

                if (GameObject.Collider)
                {
                    CheckCollision(GameObject);
                }

                if (GameObject.HasZeroVelocity()) GameObject.Active = false;
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
