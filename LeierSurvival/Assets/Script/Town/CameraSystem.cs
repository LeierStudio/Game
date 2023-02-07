using UnityEngine;

namespace Game.Town
{
    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Inst;

        /// <summary> 滑鼠世界座標 </summary>
        public Vector3 MouseWorldPosition;

        /// <summary> 滑鼠網格座標 </summary>
        public Vector3 MouseGridPosition;

        /// <summary> 射到的目標 </summary>
        public GameObject HitObj;

        /// <summary> 射到的目標位置 </summary>
        public GameObject HitObjPosition;

        void Awake()
        {
            Inst = this;
        }

        void Update()
        {
            MouseWorldPosition = Input.mousePosition;
            MouseGridPosition = GetMouseGridPosition();
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
            var ray = Camera.main.ScreenPointToRay(MouseWorldPosition);
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