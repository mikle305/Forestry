using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenTransitions
{
    [RequireComponent(typeof(Image))]
    public class TransitionScreen : MonoBehaviour
    {
        [SerializeField] private string _shaderParam = "_Progress";
        [SerializeField] private float _beginValue = 0;
        [SerializeField] private float _endValue = 1;
        [SerializeField] private bool _shownOnStart;

        [SerializeField] private Data _enterData;
        [SerializeField] private Data _exitData;
        

        private TransitionOperation _operation;
        private Image _image;
        
        [Serializable]
        private class Data
        {
            public float Duration = 0.8f;
            public Ease Ease = Ease.Linear;
        }


        protected void Awake()
        {
            _image = GetComponent<Image>();
            _image.raycastTarget = false;
            if (_shownOnStart)
                _image.material.SetFloat(_shaderParam, _endValue);
        }

        public ITransitionOperation Enter()
        {
            _image.raycastTarget = true;
            _operation = new TransitionOperation();
            
            _image.material.SetFloat(_shaderParam, _beginValue);
            _image.material.DOFloat(_endValue, _shaderParam, _enterData.Duration)
                .SetEase(_enterData.Ease)
                .OnComplete(AnimationCompeted);

            return _operation;
        }


        public ITransitionOperation Exit()
        {
            _image.raycastTarget = true;
            _operation = new TransitionOperation();

            _image.material.SetFloat(_shaderParam, _endValue);
            _image.material.DOFloat(_beginValue, _shaderParam, _exitData.Duration)
                .SetEase(_exitData.Ease)
                .OnComplete((() =>
                {
                    _image.raycastTarget = false;
                    AnimationCompeted();
                }));
            
            return _operation;
        }

        private void AnimationCompeted()
        {
            _operation.Complete();
        }
    }
}