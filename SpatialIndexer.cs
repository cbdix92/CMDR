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
        //public static List<GameObject>[,] GridCells;
        public static Dictionary<(int, int), Cell> GridCells;

        private static int _cellSize;
        public static int CellSize
        {
            get => _cellSize;
            set
            {
                _cellSize = value;
                SetCellSize();
            }
        }
        private static SpatialIndexer _instance = new SpatialIndexer();
        private SpatialIndexer()
        {
            GridCells = new Dictionary<(int, int), Cell>();
        }
        private static void SetCellSize()
        {
            foreach (GameObject GameObject in SceneManager.ActiveScene.ColliderGameObjects)
            {
                CalcGridPos(GameObject);
            }
        }

        internal static void CalcGridPos(GameObject gameObject)
        {
            // Remove "gameObject" from all the current cells then reset "gameObject.CurrentCells"
            gameObject.OverlappedCells.ForEach(x => x.Remove(gameObject));
            gameObject.OverlappedCells.Clear();

            // Top left corner of "gameObject" converted to grid cordinates
            int P1X = (int)Math.Floor((double)gameObject.Transform.X / CellSize);
            int P1Y = (int)Math.Floor((double)gameObject.Transform.Y / CellSize);

            // Bottom right corner of "gameObject" converted to grid cordinates
            int P2X = (int)Math.Floor((double)(gameObject.Transform.X + gameObject.Width) / CellSize);
            int P2Y = (int)Math.Floor((double)(gameObject.Transform.Y + gameObject.Height) / CellSize);


            // "gameObject" is occupying one gridcell do not iterate.
            if (P1X == P2X && P1Y == P2Y)
            {
                if (GridCells[(P1Y, P1X)] == null)
                {
                    GridCells[(P1X, P1Y)] = new Cell(P1Y, P1X);
                }
                gameObject.OverlappedCells = null;
                GridCells[(P1Y, P1X)].Add(gameObject);
                gameObject.CenterCell = GridCells[(P1Y, P1X)].Cache;
                return;
            }
            for (int Y = P1Y; Y <= P2Y; Y++)
                for (int X = P1X; X <= P2X; X++)
                {
                    if (!GridCells.ContainsKey((Y, X)))
                    {
                        GridCells[(Y, X)] = new Cell(Y, X);
                    }
                    if (!gameObject.OverlappedCells.Contains(GridCells[(Y, X)])) gameObject.OverlappedCells.Add(GridCells[(Y, X)]);
                    if (!GridCells[(Y, X)].Cache.Contains(gameObject)) GridCells[(Y, X)].Cache.Add(gameObject);
                }

            int CenterX = (int)Math.Floor((double)((gameObject.Transform.X + gameObject.Width) / 2)/ CellSize);
            int CenterY = (int)Math.Floor((double)((gameObject.Transform.Y + gameObject.Height) / 2) / CellSize);
            if (!GridCells.ContainsKey((CenterY, CenterX)))
            {
                GridCells.Add((CenterY, CenterX), new Cell(CenterY, CenterX));
            }
            gameObject.CenterCell = GridCells[(CenterY, CenterX)].Cache;
        }
        internal static List<GameObject> GetNearbyColliders(GameObject gameObject)
        {
            // Update "gameObjet"s grid position
            CalcGridPos(gameObject);

            // Get all the colliders in "GameObjects" center cell
            List<GameObject> Colliders = new List<GameObject>(gameObject.CenterCell);

            // Get all the colliders in "gameObject"s overlapped cells
            foreach (Cell Cell in gameObject.OverlappedCells)
            {
                foreach (GameObject Collider in Cell.Cache)
                {
                    // Mitigate duplicates
                    if (!Colliders.Contains(Collider))
                    {
                        Colliders.Add(Collider);
                    }
                }
            }
            Colliders.Remove(gameObject);
            return Colliders;
        }
    }
    internal static class SpatialIndexerExtensions
    {
        internal static List<GameObject> GetNearbyColliders(this GameObject gameObject)
        {
            SpatialIndexer.CalcGridPos(gameObject);

            List<GameObject> Colliders = new List<GameObject>(gameObject.CenterCell);
            for (int i = 0; i < gameObject.OverlappedCells.Count; i++)
            {
                Colliders.AddRange(gameObject.OverlappedCells[i].Cache);
            }

            List<GameObject> TrimmedList = Colliders.GroupBy(x => x.Hash).Select(y => y.FirstOrDefault()).ToList();
            TrimmedList.Remove(gameObject);
            return TrimmedList;
        }
    }
}
