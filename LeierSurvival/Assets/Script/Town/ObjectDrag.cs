using UnityEngine;

namespace Game.Town
{
    public class ObjectDrag : MonoBehaviour
    {
        /// <summary> 渲染器們 </summary>
        public Renderer[] Renderers;

        /// <summary> 材質球屬性塊 </summary>
        MaterialPropertyBlock _materialPropertyBlock;

        /// <summary> 測試方塊 </summary>
        GameObject TestCube;

        void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            Renderers = GetComponentsInChildren<Renderer>();

            // 設定透明度
            SetTransparency(0.2f);

            TestCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            TestCube.transform.position = transform.position;
        }

        void Update()
        {
            // 設定，位置。(將座標捕捉到網格)
            transform.position = CameraSystem.Inst.GetMouseCellWorldPos();

            TestCube.transform.position = CameraSystem.Inst.GetMouseCellWorldPos();
        }

        void OnDestroy()
        {
            // 設定透明度
            SetTransparency(1);

            Destroy(TestCube);
        }

        /// <summary>
        /// 設定透明度
        /// </summary>
        /// <param name="alpha">透明度</param>
        void SetTransparency(float alpha)
        {
            var shader = Shader.Find(alpha >= 1 ? "Legacy Shaders/Diffuse" : "Legacy Shaders/Transparent/Diffuse");
            foreach (var renderer in Renderers)
            {
                renderer.material.shader = shader;
                renderer.GetPropertyBlock(_materialPropertyBlock);
                var color = renderer.material.GetColor("_Color");
                color.a = 0.2f;
                _materialPropertyBlock.SetColor("_Color", color);
                renderer.SetPropertyBlock(_materialPropertyBlock);
            }
        }
    }
}