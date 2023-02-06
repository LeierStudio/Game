using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Game
{
    public class ScalerSystem : MonoBehaviour
    {
        /// <summary> 原本大小 </summary>
        [SerializeField] Vector3 FromScale = Vector3.zero;

        /// <summary> 目標大小 </summary>
        [SerializeField] Vector3 ToScale = Vector3.one;

        /// <summary> 時間 </summary>
        [SerializeField] float Time;

        /// <summary> 間隔時間 </summary>
        [SerializeField] float Interval = 0.001f;

        /// <summary> 目標大小 </summary>
        [SerializeField] Ease ScaleEase;

        /// <summary> 要縮上的物件們 </summary> 
        readonly List<Transform> _scaleObjs = new List<Transform>();

        void Awake()
        {
            var childs = transform.childCount;
            for (var i = 0; i < childs; i++)
            {
                var child = transform.GetChild(i);
                _scaleObjs.Add(child);
            }
        }

        void OnEnable()
        {
            PlayAnim();
        }

        /// <summary>
        /// 播放動畫
        /// </summary>
        void PlayAnim()
        {
            for (var i = 0; i < _scaleObjs.Count; i++)
            {
                var scaleObj = _scaleObjs[i];
                scaleObj.DOKill();
                scaleObj.localScale = FromScale;
                scaleObj.DOScale(ToScale, Time).SetEase(ScaleEase).SetDelay(i * Interval);
            }
        }
    }
}