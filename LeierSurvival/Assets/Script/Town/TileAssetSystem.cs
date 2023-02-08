using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Town
{
    public class TileAssetSystem : MonoBehaviour
    {
        public static TileAssetSystem Inst;

        [SerializeField] List<TileData> TileDatas;

        Dictionary<TileBase, TileData> DataFromTiles = new Dictionary<TileBase, TileData>();

        private void Awake()
        {
            Inst = this;
            foreach (var tileData in TileDatas)
            {
                foreach (var tile in tileData.Tiles)
                {
                    DataFromTiles.Add(tile, tileData);
                }
            }
        }

        /// <summary>
        /// 取得行走速度
        /// </summary>
        /// <param name="tileBase">圖塊</param>
        public float GetWalkingSpeed(TileBase tileBase)
        {
            var tileData = GetTileData(tileBase);
            var walkingSpeed = tileData.WalkingSpeed;
            return walkingSpeed;
        }

        /// <summary>
        /// 取得圖塊資料
        /// </summary>
        /// <param name="tileBase">圖塊</param>
        public TileData GetTileData(TileBase tileBase)
        {
            if (!DataFromTiles.ContainsKey(tileBase))
            {
                Debug.LogError($"[TileAssetSystem] 找不到 tileBase: {tileBase} 這個圖塊資料。");
            }
            var tileData = DataFromTiles[tileBase];
            return tileData;
        }
    }
}