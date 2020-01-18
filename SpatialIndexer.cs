using System;
using System.Collections.Generic;

namespace CMDR
{
    internal static class SpatialIndexer
    {
        public static List<GameObject>[,] GridCells;

        private static int _cellSize;

        public static int CellSize
        {
            get => _cellSize;
            set
            {
                _cellSize = value;
                Init();
            }
        }
        private static int _gridLX, _gridLY;

        public static void Init()
        {
            if (_cellSize == 0) _cellSize = 25;

            _gridLX = (int)Math.Ceiling((float)Render.Display.Size.Width / CellSize);
            _gridLY = (int)Math.Ceiling((float)Render.Display.Size.Height / CellSize);

            GridCells = new List<GameObject>[_gridLY, _gridLX];

            // Instantiate the Lists inside of "GridCells"
            foreach (int Y in _gridLY.GetArrayRange())
            {
                foreach (int X in _gridLX.GetArrayRange())
                {
                    GridCells[Y, X] = new List<GameObject>();
                }
            }
        }

        public static void CalcPos(GameObject gameObject)
        {
            // Remove "gameObject" from all the current cells then reset "gameObject.CurrentCells"
            foreach (List<GameObject> Cell in gameObject.CurrentCells)
            {
                Cell.Remove(gameObject);
            }
            gameObject.CurrentCells.Clear();

            // Top left corner of "gameObject" converted to grid cordinates
            int P1X = (int)Math.Floor((double)gameObject.Transform.X / CellSize);
            int P1Y = (int)Math.Floor((double)gameObject.Transform.Y / CellSize);

            // Bottom right corner of "gameObject" converted to grid cordinates
            int P2X = (int)Math.Floor((double)(gameObject.Transform.X + gameObject.Width) / CellSize);
            int P2Y = (int)Math.Floor((double)(gameObject.Transform.Y + gameObject.Height) / CellSize);

            // "gameObject" is occupying one gridcell do not iterate.
            if (P1X == P2X && P1Y == P2Y)
            {
                gameObject.CurrentCells.Add(GridCells[P1Y, P1X]);
                GridCells[P1Y, P1X].Add(gameObject);
                return;
            }
            for (int Y = P1Y; Y < P2Y; Y++)
                for (int X = P1X; X < P2X; X++)
                {
                    if (!gameObject.CurrentCells.Contains(GridCells[Y, X])) gameObject.CurrentCells.Add(GridCells[Y, X]);
                    if (!GridCells[Y, X].Contains(gameObject)) GridCells[Y, X].Add(gameObject);
                }
        }
        public static List<GameObject> GetNearbyColliders(this GameObject gameObject)
        {
            // Returns a List of objects that are likely to be colliding with "gameObject"
            List<GameObject> Colliders = new List<GameObject>();
            foreach (List<GameObject> Cell in gameObject.CurrentCells)
            {
                foreach (GameObject Collider in Cell)
                {
                    if (Colliders.Contains(Collider))
                    {
                        continue;
                    }
                    else
                    {
                        Colliders.Add(Collider);
                    }
                }
            }
            return Colliders;
        }

    }
}
