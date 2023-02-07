using UnityEngine;

namespace Game.Town
{
    public class PlaceableObject : MonoBehaviour
    {
        /// <summary> 已放置 </summary>
        public bool Placed { get; private set; }

        /// <summary> 大小 </summary>
        public Vector3Int Size { get; private set; }

        /// <summary> 頂點們 </summary>
        Vector3[] _vertices = new Vector3[4];

        void Start()
        {
            GetColliderVertexPositionsLocal();
            CalculateSizeInCells();
        }

        /// <summary>
        /// 旋轉
        /// </summary>
        public void Rotate()
        {
            transform.Rotate(new Vector3(0, 90, 0));
            Size = new Vector3Int(Size.y, Size.x, 1);

            var vertices = new Vector3[_vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i] = _vertices[(i + 1) % vertices.Length];
            }
            _vertices = vertices;
        }

        /// <summary>
        /// 放置
        /// </summary>
        public virtual void Place()
        {
            var drag = GetComponent<ObjectDrag>();
            Destroy(drag);
            Placed = true;

            // Invoke events of placement
        }

        /// <summary>
        /// 取得起始位置
        /// </summary>
        public Vector3 GetStartPosition()
        {
            var pos = transform.TransformPoint(_vertices[0]);
            return pos;
        }

        /// <summary>
        /// 取得碰撞器頂點位置
        /// </summary>
        void GetColliderVertexPositionsLocal()
        {
            var b = GetComponent<BoxCollider>();
            // 左下角
            _vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
            // 右下角
            _vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
            // 右上角
            _vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
            // 左上角
            _vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
        }

        /// <summary>
        /// 計算大小佔了幾格
        /// </summary>
        void CalculateSizeInCells()
        {
            var vertices = new Vector3Int[_vertices.Length];
            for (var i = 0; i < vertices.Length; i++)
            {
                // 世界座標 (區域頂點 轉 世界座標)
                var worldPos = transform.TransformPoint(_vertices[i]);
                // 設定，頂點。(世界座標 轉 格子座標)
                vertices[i] = BuildingSystem.Inst.GridLayout.WorldToCell(worldPos);
            }
            // X 大小 (左下角 - 右下角)
            var xSize = Mathf.Abs((vertices[0] - vertices[1]).x);
            // Y 大小 (左下角 - 左上角)
            var ySize = Mathf.Abs((vertices[0] - vertices[3]).y);
            // 設定，大小。
            Size = new Vector3Int(xSize, ySize, 1);
        }
    }
}