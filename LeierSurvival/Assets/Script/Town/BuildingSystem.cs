using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Town
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Inst;

        public GridLayout GridLayout;
        public GameObject Prefab1;
        public GameObject Prefab2;

        [SerializeField] Tilemap MainTilemap;
        [SerializeField] TileBase WhiteTile;

        Grid _grid;
        PlaceableObject _objectToPlace;

        #region Unity Methods
        void Awake()
        {
            Inst = this;
            _grid = GridLayout.GetComponent<Grid>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                InitializeWithObject(Prefab1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                InitializeWithObject(Prefab2);
            }
        }
        #endregion

        #region Utils
        /// <summary>
        /// 取得滑鼠世界座標
        /// </summary>
        public static Vector3 GetMouseWorldPosition()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var raycastHit))
            {
                return raycastHit.point;
            }
            return Vector3.zero;
        }

        /// <summary>
        /// 將座標捕捉到網格
        /// </summary>
        /// <param name="position">世界座標</param>
        public Vector3 SnapCoordinateToGrid(Vector3 position)
        {
            // 格子座標
            var cellPos = GridLayout.WorldToCell(position);
            // 設定，格子中心座標。
            position = _grid.GetCellCenterWorld(cellPos);
            return position;
        }
        #endregion

        #region Building Placement
        public void InitializeWithObject(GameObject prefab)
        {
            var position = SnapCoordinateToGrid(Vector3.zero);
            var obj = Instantiate(prefab, position, Quaternion.identity);
            _objectToPlace = obj.GetComponent<PlaceableObject>();
            obj.AddComponent<ObjectDrag>();
        }
        #endregion
    }
}