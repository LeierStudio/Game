using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Town
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Inst;

        /// <summary> 滑鼠世界座標 </summary>
        public Vector3 MouseWorldPosition;

        /// <summary> 滑鼠網格座標 </summary>
        public Vector3Int MouseGridPosition;

        /// <summary> 射到的圖塊 </summary>
        public TileBase HitTile;

        /// <summary> 射到的圖塊移動速度 </summary>
        public float HitTileWalkingSpeed;

        /// <summary> 建築物系統 </summary>
        BuildingSystem BuildingSystem => BuildingSystem.Inst;

        /// <summary> 建築物系統 </summary>
        TileAssetSystem TileAssetSystem => TileAssetSystem.Inst;

        /// <summary> 主攝影機 </summary>
        Camera _mainCamera;

        void Awake()
        {
            Inst = this;
            _mainCamera = Camera.main;
        }

        void Update()
        {
            MouseWorldPosition = Input.mousePosition;
            MouseGridPosition = BuildingSystem.GetCellPos(GetMouseCellWorldPos());
            HitTile = BuildingSystem.GetTile(MouseGridPosition);
            HitTileWalkingSpeed = HitTile ? TileAssetSystem.GetWalkingSpeed(HitTile) : 0;
        }

        /// <summary>
        /// 取得滑鼠網格座標
        /// </summary>
        public Vector3 GetMouseCellWorldPos()
        {
            var position = BuildingSystem.GetCellWorldPos(GetMouseWorldPosition());
            return position;
        }

        /// <summary>
        /// 取得滑鼠世界座標
        /// </summary>
        public Vector3 GetMouseWorldPosition()
        {
            var ray = _mainCamera.ScreenPointToRay(MouseWorldPosition);
            var distance = 1000f;
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                return raycastHit.point;
            }
            return Vector3.zero;
        }
    }
}