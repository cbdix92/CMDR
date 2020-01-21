using System;
using System.Collections.Generic;

namespace CMDR
{
    static internal class SceneManager
    {
        public static List<Scene> Scenes = new List<Scene>();
        public static Scene ActiveScene { get; set; }
        
        public static void LoadScene(Scene newScene)
        {
            Scenes.Add(newScene);
            ActiveScene = newScene;
        }
        public static void UnloadScene(Scene oldScene)
        {
            Scenes.Remove(oldScene);
        }
    }
    public class Scene
    {
        public List<GameObject> GameObjects = new List<GameObject>();
        public List<GameObject> ActiveGameObjects = new List<GameObject>();
        public List<GameObject> ColliderGameObjects = new List<GameObject>();
        public Scene()
        {
            SceneManager.LoadScene(this);
        }
        public GameObject AddGameObject() { return AddGameObject(0, 0, 0); }
        public GameObject AddGameObject(int posX, int posY, int posZ)
        {
            GameObjects.Add(new GameObject(this, posX, posY, posZ));
            return GameObjects[GameObjects.Count - 1];
        }
        public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }
    }
}
