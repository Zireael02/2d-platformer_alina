using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer
{
    public class GeneratorLevelView : MonoBehaviour
    {
        public Tilemap _tilemapCollaider;
        public Tilemap _tilemapNonCollaider;
        public Tile _tileForest;
        public Tile _tileSwamp;
        public Tile _tileWater;

        public int _mapHeight;
        public int _mapWight;

        [Range(0, 100)] public int _fillPercent;
        [Range(0, 100)] public int _smoothPercent;

        public bool _borders;
    }
}