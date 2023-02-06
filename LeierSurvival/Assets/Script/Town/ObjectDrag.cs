using UnityEngine;

namespace Game.Town
{
    public class ObjectDrag : MonoBehaviour
    {
        /// <summary> 偏移量 </summary>
        Vector3 _offset;

        void OnMouseDown()
        {
            // 設定，偏移量。(目前世界座標 - 滑鼠世界座標)
            _offset = transform.position - BuildingSystem.GetMouseWorldPosition();
        }

        void OnMouseDrag()
        {
            // 設定，位置。(滑鼠世界座標 + 偏移量)
            var pos = BuildingSystem.GetMouseWorldPosition() + _offset;
            // 設定，位置。(將座標捕捉到網格)
            transform.position = BuildingSystem.Inst.SnapCoordinateToGrid(pos);
        }
    }
}