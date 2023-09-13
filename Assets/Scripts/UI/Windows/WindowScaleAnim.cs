using System;
using DG.Tweening;
using UnityEngine;

namespace UI.Windows
{
    public class WindowScaleAnim : Window
    {
        [SerializeField, Min(0)] private float _duration = 0.3f;

        private Tween _tween;

        
        public override void Show(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(1, _duration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }

        public override void Hide(Action onStart = null, Action onDone = null)
        {
            _tween?.Kill();
            _tween = transform
                .DOScale(0, _duration)
                .OnStart(() => onStart?.Invoke())
                .OnComplete(() => onDone?.Invoke());
        }
    }
}