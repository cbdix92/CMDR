using System;
using System.Collections.Generic;

namespace CMDR
{
    static internal class SceneManager
    {
        public static List<Scene> Scenes = new List<Scene>();
        public static Scene ActiveScene { get; set; }
        
        public static void LoadScene(Scene scene)
        {
            Scenes.Add(scene);
            ActiveScene = scene;
        }
        public static void UnloadScene(Scene scene)
        {
            Scenes.Remove(scene);
        }
    }
    public class Scene
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        public List<GameObject> ActiveGameObjects = new List<GameObject>();
        public List<GameObject> ColliderGameObjects = new List<GameObject>();
        public List<GameObject> RenderObjects = new List<GameObject>();
        public List<GameObject> RenderActive = new List<GameObject>();
        private List<GameObject>[] _master;

        public Scene()
        {
            SceneManager.LoadScene(this);
            _master = new List<GameObject>[] { GameObjects, ActiveGameObjects, ColliderGameObjects, RenderObjects, RenderActive };
        }

        public GameObject AddGameObject() { return AddGameObject(0, 0, 0); }
        public GameObject AddGameObject(int posX, int posY, int posZ)
        {
            return AddGameObject(new GameObject(this, posX, posY, posZ));
        }
        public GameObject AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
            return GameObjects[GameObjects.Count - 1];
        }
        public void RemoveGameObject(GameObject gameObject)
        {
            foreach (List<GameObject> list in _master)
            {
                list.Remove(gameObject);
                list.TrimExcess();
            }

            gameObject = null;
        }
    }
}
