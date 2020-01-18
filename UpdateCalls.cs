using System;
using System.Collections.Generic;

namespace CMDR
{
    internal partial class Render
    {
        public static void Update(object caller, EventArgs e)
        {
            if (SceneManager.ActiveScene == null || SceneManager.ActiveScene.GameObjects[0] == null) return;
            ClearScreen();
            ScreenBuffer();
            Draw();
        }
    }

    internal partial class Physics
    {
        public static void Update(object caller, EventArgs e)
        {
            if (SceneManager.ActiveScene == null || SceneManager.ActiveScene.ActiveGameObjects.Count == 0) return;

            Scene Scene = SceneManager.ActiveScene;

            List<GameObject> ActiveObjects = new List<GameObject>(Scene.ActiveGameObjects);

            foreach (GameObject GameObject in ActiveObjects)
            {
                GameObject.Move();

                GameObject.CheckCollision();

                if (GameObject.HasZeroVelocity()) Scene.ActiveGameObjects.Remove(GameObject);
            }
        }
    }
}
