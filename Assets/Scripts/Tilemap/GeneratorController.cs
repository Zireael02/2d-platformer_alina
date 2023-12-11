using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    public class GeneratorController
    {
        private Tilemap _tilemapCollaider;
        private Tilemap _tilemapNonCollaider;
        private Tile _tileForest;
        private Tile _tileSwamp;
        private Tile _tileWater;

        private int _mapHeight;
        private int _mapWight;
        private Transform _playerT;

        private int _fillPercent;
        private int _smoothPercent;

        private bool _borders;

        private int[,] _map;

        private MarshingSquareController _controller;

        public GeneratorController(GeneratorLevelView view, Transform player)
        {
            _tilemapCollaider = view._tilemapCollaider;
            _tilemapNonCollaider = view._tilemapNonCollaider;
            _tileForest = view._tileForest;
            _tileWater = view._tileWater;
            _tileSwamp= view._tileSwamp;
            _mapHeight = view._mapHeight;
            _mapWight = view._mapWight;
            _fillPercent = view._fillPercent;
            _smoothPercent = view._smoothPercent;
            _borders = view._borders;
            _map = new int[_mapWight, _mapHeight];
            _playerT = player;
        }

        public void Start()
        {
            FillMap();

            for (int i = 0; i < _smoothPercent; i++)
            {
                SmoothMap();
            }

            _controller = new MarshingSquareController();
            _controller.GenerateGrid(_map, 1);
            _controller.DrawTiles(_tilemapCollaider, _tilemapNonCollaider, _tileForest, _tileWater);

            //DrawTiles();

            PlayerPoint();
        }


        public void PlayerPoint()
        {
            while(true)
            {
                int x = Random.Range(1, _mapWight);
                int y = Random.Range(1, _mapHeight);
                if (_map[x, y] == 0)
                {
                    int neighbour = GetNeignbour(x, y);
                    if (neighbour <= 0)
                    {
                        _playerT.transform.position = new Vector3(_tilemapCollaider.transform.position.x - _mapWight / 2 + x, _tilemapCollaider.transform.position.y - _mapHeight / 2 + y, 0);
                        Camera.main.transform.position = new Vector3(_tilemapCollaider.transform.position.x - _mapWight / 2 + x, _tilemapCollaider.transform.position.y - _mapHeight / 2 + y, -10);
                        return;
                    }
                }
            }

            //for (int x = 0; x < _mapWight; x++)
            //{
            //    for (int y = 0; y < _mapHeight; y++)
            //    {
            //        if (_map[x, y] == 0)
            //        {
            //            int neighbour = GetNeignbour(x, y);
            //            if (neighbour <= 0)
            //            {
            //                _playerT.transform.position = new Vector3(_tilemap.transform.position.x - _mapWight/2 + x, _tilemap.transform.position.y - _mapHeight/2 + y, 0);
            //                Camera.main.transform.position = new Vector3(_tilemap.transform.position.x - _mapWight / 2 + x, _tilemap.transform.position.y - _mapHeight / 2 + y, -10);
            //                return;                            
            //            }
            //        }
            //    }
            //}
        }

        public void FillMap()
        {
            for (int x = 0; x < _mapWight; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (x == 0 || x == _mapWight - 1 || y == 0 || y == _mapHeight - 1)
                    {
                        if (_borders) _map[x, y] = 1;
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    }                   
                }
            }
        }

        public void SmoothMap()
        {
            for (int x = 0; x < _mapWight; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neighbour = GetNeignbour(x, y);
                    if (neighbour > 4)
                    {
                        _map[x, y] = 1;
                    }
                    else if (neighbour < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        public int GetNeignbour(int x, int y)
        {
            int neighbour = 0;

            for (int gridX = x-1; gridX <= x+1; gridX++)
            {
                for (int gridY = y-1; gridY <=y+1; gridY++)
                {
                    if (gridX >= 0 && gridX < _mapWight && gridY >= 0 && gridY < _mapHeight)
                    {
                        if (gridX != x || gridY != y)
                        {
                            neighbour += _map[gridX, gridY];
                        }
                    }
                    else
                    {
                        neighbour++;
                    }
                }
            }
            return neighbour;
        }

        public void DrawTiles()
        {
            if (_map == null) return;

            for (int x = 0; x < _mapWight; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_map[x, y] == 1)
                    {
                        Vector3Int tilePosition = new Vector3Int(-_mapWight/2 + x, _mapHeight/2 +y, 0);
                        _tilemapCollaider.SetTile(tilePosition, _tileForest);
                    }
                    else if (_map[x, y] == 0)
                    {
                        Vector3Int tilePosition = new Vector3Int(-_mapWight / 2 + x, _mapHeight / 2 + y, 0);
                        _tilemapNonCollaider.SetTile(tilePosition, _tileWater);
                    }
                }
            }
        }

        public void Update()
        {

        }
    }
}