using System;
using System.Linq;
using System.Collections.Generic;

namespace CMDR
{
    internal class Cell
    {
        public (int Y, int X) GridKey;
        public List<GameObject> Cache = new List<GameObject>();
        public Cell(int y,int x)
        {
            GridKey = (y, x);
        }
        public void Add(GameObject gameObject)
        {
            Cache.Add(gameObject);
        }
        public void Remove(GameObject gameObject)
        {
            Cache.Remove(gameObject);
        }
    }
    internal class SpatialIndexer
    {
        public static Dictionary<(int, int), Cell> GridCells;

        private static int _cellSize = 30;
        public static int CellSize
        {
            get => _cellSize;
            internal set
            {
                if (value != _cellSize)
                {
                    _cellSize = value;
                    SceneManager.ActiveScene.ColliderGameObjects.ForEach(x => CalcGridPos(x));
                }
            }
        }
        private static SpatialIndexer _instance = new SpatialIndexer();
        private SpatialIndexer()
        {
            GridCells = new Dictionary<(int, int), Cell>();
        }
        internal static List<GameObject> GetNearbyColliders(GameObject gameObject)
        {
            List<GameObject> Colliders = new List<GameObject>();

            for (int i = 0; i < gameObject.OverlappedCells.Count; i++)
                Colliders.AddRange(gameObject.OverlappedCells[i].Cache);

            List<GameObject> TrimmedList = Colliders.GroupBy(x => x.Hash).Select(y => y.FirstOrDefault()).ToList();
            TrimmedList.Remove(gameObject);
            return TrimmedList;
        }

        internal static void CalcGridPos(GameObject gameObject)
        {
            // Recaculating grid position for a disposed 
            // object will cause ghost objects to remain 
            // inside a gridcell if the object is both 
            // moving and disposed on collision. DO NOT REMOVE THIS!
            if (gameObject.Disposed)
                return;

            // Remove "gameObject" from all the current cells then reset "gameObject.CurrentCells"
            List<Cell> oldCells = new List<Cell>(gameObject.OverlappedCells);
            gameObject.OverlappedCells.ForEach(x => x.Cache.Remove(gameObject));
            gameObject.OverlappedCells.Clear();

            // Top left corner of "gameObject" converted to grid cordinates
            int P1X = (int)Math.Floor((double)gameObject.Transform.X / CellSize);
            int P1Y = (int)Math.Floor((double)gameObject.Transform.Y / CellSize);

            // Bottom right corner of "gameObject" converted to grid cordinates
            int P2X = (int)Math.Floor((double)(gameObject.Transform.X + gameObject.Width) / CellSize);
            int P2Y = (int)Math.Floor((double)(gameObject.Transform.Y + gameObject.Height) / CellSize);

            // Place "gameObject" in all of it's occupied "GridCells"
            for (int Y = P1Y; Y <= P2Y; Y++)
                for (int X = P1X; X <= P2X; X++)
                {
                    // Create a new cell if it doesn't exist
                    if (!GridCells.ContainsKey((Y, X)))
                        GridCells[(Y, X)] = new Cell(Y, X);

                    GridCells[(Y,X)].Cache.Add(gameObject);
                    gameObject.OverlappedCells.Add(GridCells[(Y, X)]);
                }
 
            // WHICH GRID CELL CONATINS THE CENTER OF THE OBJECT. FOR TWO OBJECTS TO BE COLLIDING ONE OF THEM MUST HAVE THEIR CENTER
            // INSIDE OF THE SHARED GRID CELL. THIS WILL HELP ME TO OPTIMIZE THE SPATIAL INDEXER IN THE FUTURE.
            //int CenterX = (int)Math.Floor((double)(gameObject.Transform.X + gameObject.Width / 2) / CellSize);
            //int CenterY = (int)Math.Floor((double)(gameObject.Transform.Y + gameObject.Height / 2) / CellSize);
            //gameObject.CenterCell = GridCells[(CenterY, CenterX)].Cache;
            
            // Remove Empty Cells
            foreach (Cell Cell in oldCells)
                if (Cell.Cache.Count == 0)
                    GridCells.Remove(Cell.GridKey);
        }
    }
}
