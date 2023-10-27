using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay.GridSystem
{
    public class GridFromTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;

        private GridMap _gridMap;


        private void Start()
        {
            _gridMap = GridMap.Instance;
            _gridMap.Init(CreateMap());
        }

        private Tile[,] CreateMap()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            Tile[,] map = new Tile[bounds.size.x, bounds.size.y];
            TileBase[] unityTiles = _tilemap.GetTilesBlock(bounds);

            for (var x = 0; x < bounds.size.x; x++)
            for (var y = 0; y < bounds.size.y; y++)
            {
                map[x, y] = new Tile
                {
                    Position = GetTilePosition(bounds, x, y),
                    State = CreateTileState(unityTiles, bounds, x, y),
                };
            }

            SetTilesEdges(map);
            return map;
        }

        private Vector3 GetTilePosition(BoundsInt bounds, int x, int y)
        {
            Vector3 cellSize = _tilemap.cellSize;
            return new Vector3(
                x: bounds.xMin + 0.5f + x * cellSize.x,
                y: bounds.yMin + 0.5f + y * cellSize.y);
        }

        private static TileState CreateTileState(TileBase[] unityTiles, BoundsInt bounds, int x, int y)
        {
            TileBase unityTile = unityTiles[x + y * bounds.size.x];
            return unityTile == null
                ? TileState.Outside
                : TileState.Empty;
        }

        private static void SetTilesEdges(Tile[,] map)
        {
            for (var x = 0; x < map.GetLength(0); x++)
            for (var y = 0; y < map.GetLength(1); y++)
            {
                Tile tile = map[x, y];
                if (tile.State != TileState.Empty)
                    continue;

                if (CheckAroundTile(map, x, y) || CheckCorners(map, x, y)) 
                    tile.State = TileState.Edge;
            }
        }

        private static bool CheckCorners(Tile[,] map, int x, int y)
        {
            bool leftUp = CheckAroundTile(map, x - 1, y) && CheckAroundTile(map, x, y + 1) && CheckTileBlock(map, x - 1, y + 1);
            bool leftDown = CheckAroundTile(map, x - 1, y) && CheckAroundTile(map, x, y - 1) && CheckTileBlock(map, x - 1, y - 1);
            bool rightUp = CheckAroundTile(map, x + 1, y) && CheckAroundTile(map, x, y + 1) && CheckTileBlock(map, x + 1, y + 1);
            bool rightDown = CheckAroundTile(map, x + 1, y) && CheckAroundTile(map, x, y - 1) && CheckTileBlock(map, x + 1, y - 1);

            return leftUp || leftDown || rightUp || rightDown;
        }

        private static bool CheckTileBlock(Tile[,] map, int x, int y)
            => IsTileOutside(map, x, y) || !CheckAroundTile(map, x, y);

        private static bool CheckAroundTile(Tile[,] map, int x, int y)
            => CheckLeft(map, x, y) || 
               CheckRight(map, x, y) || 
               CheckDown(map, x, y) || 
               CheckUp(map, x, y);

        private static bool CheckLeft(Tile[,] map, int x, int y)
            => x == 0 || IsTileOutside(map, x - 1, y);

        private static bool CheckRight(Tile[,] map, int x, int y)
            => x == map.GetLength(0) - 1 || IsTileOutside(map, x + 1, y);

        private static bool CheckDown(Tile[,] map, int x, int y)
            => y == 0 || IsTileOutside(map, x, y - 1);

        private static bool CheckUp(Tile[,] map, int x, int y)
            => y == map.GetLength(1) - 1 || IsTileOutside(map, x, y + 1);

        private static bool IsTileOutside(Tile[,] map, int x, int y)
            => map[x, y].State == TileState.Outside;
    }
}