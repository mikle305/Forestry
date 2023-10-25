using Additional.Utils;
using TriInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GamePlay
{
    public class GridFromTilemap : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Vector2 _offset;

        private GridMap _gridMap;
        private Transform _textsParent;


        private void Start()
        {
            _textsParent = new GameObject("GridMapTexts").transform;
            _gridMap = GridMap.Instance;
            _gridMap.Init(CreateMap());
        }

        private bool[,] CreateMap()
        {
            BoundsInt bounds = _tilemap.cellBounds;
            TileBase[] tiles = _tilemap.GetTilesBlock(bounds);
            var map = new bool[bounds.size.x, bounds.size.y];
            ClearMapTexts();
            
            for(var x = 0; x < bounds.size.x; x++)
            for(var y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tiles[x + y * bounds.size.x];
                if(tile != null)
                {
                    map[x, y] = true;
                    var worldPosition = new Vector3(
                        x: (x + _offset.x - bounds.size.x / 2.0f) * _tilemap.cellSize.x,
                        y: (y + _offset.y - bounds.size.y / 2.0f) * _tilemap.cellSize.y);
                    
                    UiUtils.CreateWorldText("0", worldPosition, 10, _textsParent, sortingOrder: 1);
                }
                else
                {
                    map[x, y] = false;
                }
            }

            return map;
        }

        private void ClearMapTexts()
        {
            foreach (Transform itemText in _textsParent)
                Destroy(itemText.gameObject);
        }

        [Button("Regenerate")]
        private void GenerateMapButton()
            => _gridMap.Init(CreateMap());
    }
}