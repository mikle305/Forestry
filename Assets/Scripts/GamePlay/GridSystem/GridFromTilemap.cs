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
            
            for(var x = 0; x < bounds.size.x; x++)
            for(var y = 0; y < bounds.size.y; y++)
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
            => new(
                x: bounds.xMin + 0.5f + x * _tilemap.cellSize.x, 
                y: bounds.yMin + 0.5f + y * _tilemap.cellSize.y);

        private static TileState CreateTileState(TileBase[] unityTiles, BoundsInt bounds, int x, int y)
        {
            TileBase unityTile = unityTiles[x + y * bounds.size.x];
            return unityTile == null 
                ? TileState.Outside 
                : TileState.Empty;
        }
        
        private static void SetTilesEdges(Tile[,] map)
        {
            for(var x = 0; x < map.GetLength(0); x++)
            for(var y = 0; y < map.GetLength(1); y++)
            {
                Tile tile = map[x, y];
                if (tile.State != TileState.Empty)
                    continue;

                if(CheckX(map, x, y) ||
                   CheckY(map, x, y) /*||
                   CheckRightUp(map, x, y) ||
                   CheckRightDown(map, x, y) ||
                   CheckLeftUp(map, x, y) ||
                   CheckLeftDown(map, x, y)*/)
                {
                    tile.State = TileState.Edge;
                }
            }
        }

        private static bool CheckX(Tile[,] map, int x, int y)  
            => x == 0 ||
               x == map.GetLength(0) - 1 ||
               map[x - 1, y].State == TileState.Outside ||
               map[x + 1, y].State == TileState.Outside;

        private static bool CheckY(Tile[,] map, int x, int y)  
            => y == 0 ||
               y == map.GetLength(1) - 1 ||
               map[x, y - 1].State == TileState.Outside ||
               map[x, y + 1].State == TileState.Outside;

        private static bool CheckRightUp(Tile[,] map, int x, int y)
            => CheckX(map, x + 1, y) && CheckY(map, x, y + 1);
        
        private static bool CheckRightDown(Tile[,] map, int x, int y)
            => CheckX(map, x + 1, y) && CheckY(map, x, y - 1);

        private static bool CheckLeftUp(Tile[,] map, int x, int y)
            => CheckX(map, x - 1, y) && CheckY(map, x, y + 1);
        
        private static bool CheckLeftDown(Tile[,] map, int x, int y)
            => CheckX(map, x - 1, y) && CheckY(map, x, y - 1);
    }
}