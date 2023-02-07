using UnityEngine;
using UnityEngine.Tilemaps;

// Tile 圖塊

namespace Game.Town
{
    public class BuildingSystem : MonoBehaviour
    {
        public static BuildingSystem Inst;

        /// <summary> 網格佈局 </summary>
        public GridLayout GridLayout;

        /// <summary> 欲置物 01 </summary>
        public GameObject Prefab1;

        /// <summary> 欲置物 02 </summary>
        public GameObject Prefab2;

        /// <summary> 欲置物 03 </summary>
        public GameObject Prefab3;

        /// <summary> 主圖塊地圖 </summary>
        [SerializeField] Tilemap MainTilemap;

        /// <summary> 白色地圖 </summary>
        [SerializeField] TileBase WhiteTile;

        /// <summary> 網格 </summary>
        Grid _grid;

        /// <summary> 物件放置元件 </summary>
        PlaceableObject _objectToPlace;

        #region Unity Methods
        void Awake()
        {
            Inst = this;
            _grid = GridLayout.GetComponent<Grid>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                InitializeWithObject(Prefab1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                InitializeWithObject(Prefab2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                InitializeWithObject(Prefab3);
            }

            if (!_objectToPlace)
                return;

            if (Input.GetKeyDown(KeyCode.R))
            {
                _objectToPlace.Rotate();
            }
            else if (Input.GetMouseButtonDown(0))
            {
                if (CanBePlaced(_objectToPlace))
                {
                    // 放置
                    _objectToPlace.Place();
                    // 起始位置
                    var start = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
                    // 填滿圖塊
                    TakeArea(start, _objectToPlace.Size);
                    _objectToPlace = null;
                }
            }
            else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(_objectToPlace.gameObject);
                _objectToPlace = null;
            }
        }
        #endregion

        #region Utils

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

        /// <summary>
        /// 取得圖塊阻擋格
        /// </summary>
        /// <param name="area">區域</param>
        /// <param name="tilemap">圖塊地圖</param>
        static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
        {
            var array = new TileBase[area.size.x * area.size.y * area.size.z];
            var counter = 0;
            foreach (var v in area.allPositionsWithin)
            {
                // 位置
                var pos = new Vector3Int(v.x, v.y, 0);
                // 加入，圖塊基底。
                array[counter] = tilemap.GetTile(pos);
                ++counter;
            }
            return array;
        }
        #endregion

        #region Building Placement
        /// <summary>
        /// 物件初始化
        /// </summary>
        /// <param name="prefab">欲置物</param>
        public void InitializeWithObject(GameObject prefab)
        {
            // 位置
            var position = CameraSystem.Inst.GetMouseGridPosition();
            // 物件
            var obj = Instantiate(prefab, position, Quaternion.identity);
            _objectToPlace = obj.GetComponent<PlaceableObject>();
            // 附加，拖曳元件。
            obj.AddComponent<ObjectDrag>();
        }

        /// <summary>
        /// 是否可以放置
        /// </summary>
        /// <param name="placeableObject">可放置的物件</param>
        bool CanBePlaced(PlaceableObject placeableObject)
        {
            // 範圍
            var area = new BoundsInt();
            area.position = GridLayout.WorldToCell(_objectToPlace.GetStartPosition());
            area.size = placeableObject.Size;

            // 取得圖塊陣列
            var baseArray = GetTilesBlock(area, MainTilemap);
            foreach (var b in baseArray)
            {
                // 如果已被占用
                if (b == WhiteTile)
                {
                    // 不行
                    return false;
                }
            }
            // 可以
            return true;
        }

        /// <summary>
        /// 填滿圖塊
        /// </summary>
        /// <param name="start">開始位置</param>
        /// <param name="size">大小</param>
        public void TakeArea(Vector3Int start, Vector3Int size)
        {
            // 填滿，圖塊。
            MainTilemap.BoxFill(start, WhiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
        }
        #endregion
    }
}