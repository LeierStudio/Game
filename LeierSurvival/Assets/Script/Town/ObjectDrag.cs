using UnityEngine;

namespace Game.Town
{
    public class ObjectDrag : MonoBehaviour
    {
        /// <summary> 滑鼠世界座標 </summary>
        public Vector3 MouseWorldPosition;

        /// <summary> 渲染器們 </summary>
        public Renderer[] Renderers;

        /// <summary> 材質球屬性塊 </summary>
        MaterialPropertyBlock _materialPropertyBlock;

        void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            Renderers = GetComponentsInChildren<Renderer>();

            // 設定透明度
            SetTransparency(0.2f);
        }

        void Update()
        {
            // 設定，位置。(將座標捕捉到網格)
            transform.position = CameraSystem.Inst.GetMouseGridPosition();
        }

        void OnDestroy()
        {
            // 設定透明度
            SetTransparency(1);
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
                renderer.sharedMaterial.shader = shader;
                renderer.GetPropertyBlock(_materialPropertyBlock);
                var color = renderer.sharedMaterial.GetColor("_Color");
                color.a = 0.2f;
                _materialPropertyBlock.SetColor("_Color", color);
                renderer.SetPropertyBlock(_materialPropertyBlock);
            }
        }
    }
}