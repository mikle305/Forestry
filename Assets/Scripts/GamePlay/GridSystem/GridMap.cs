using System;
using Additional.Game;
using Additional.Utils;
using TriInspector;
using UnityEngine;

namespace GamePlay.GridSystem
{
    public class GridMap : MonoSingleton<GridMap>
    {
        private Tile[,] _map;
        private Transform _textsParent;


        protected override void Awake()
        {
            base.Awake();
            _textsParent = new GameObject("GridMapTexts").transform;
        }

        public void Init(Tile[,] map)
        {
            _map = map;
        }

        
        [Button("Show map")]
        private void ShowMapButton()
        {
            ClearMapTexts();
            ShowMapTexts();
        }
        
        private void ClearMapTexts()
        {
            foreach (Transform itemText in _textsParent)
                Destroy(itemText.gameObject);
        }

        private void ShowMapTexts()
        {
            for (var x = 0; x < _map.GetLength(0); x++)
            for (var y = 0; y < _map.GetLength(1); y++)
            {
                Tile tile = _map[x, y];
                if (tile.State == TileState.Outside)
                    continue;
                
                UiUtils.CreateWorldText(
                    text: GetTextFromState(tile.State).ToString(), 
                    localPosition: tile.Position, 
                    fontSize: 10, 
                    parent: _textsParent, 
                    sortingOrder: 1);
            }
        }
        
        private static int GetTextFromState(TileState tileState)
        {
            return tileState switch
            {
                TileState.Outside => 0,
                TileState.Edge => 0,
                TileState.Build => 0,
                TileState.Empty => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(tileState), tileState, null),
            };
        }
    }
}