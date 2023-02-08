using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    [CreateAssetMenu(fileName = "MySettings", menuName = "Tools/TileData")]
    public class TileData : ScriptableObject
    {
        /// <summary> 圖塊 </summary>
        public TileBase[] Tiles;

        /// <summary> 移動速度 </summary>
        public float WalkingSpeed = 1;

        /// <summary> 中毒 </summary>
        public float Poisonous;
    }
}