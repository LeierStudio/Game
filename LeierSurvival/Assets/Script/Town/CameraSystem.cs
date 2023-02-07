using UnityEngine;

namespace Game.Town
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Inst;

        void Awake()
        {
            Inst = this;
        }

        /// <summary>
        /// 取得滑鼠網格座標
        /// </summary>
        public Vector3 GetMouseGridPosition()
        {
            var position = BuildingSystem.Inst.SnapCoordinateToGrid(GetMouseWorldPosition());
            return position;
        }

        /// <summary>
        /// 取得滑鼠世界座標
        /// </summary>
        public Vector3 GetMouseWorldPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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