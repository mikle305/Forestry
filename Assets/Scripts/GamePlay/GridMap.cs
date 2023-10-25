using Additional.Game;

namespace GamePlay
{
    public class GridMap : MonoSingleton<GridMap>
    {
        private bool[,] _map;


        public void Init(bool[,] map)
        {
            _map = map;
        }
    }
}