using Services;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class UiCameraSetter : MonoBehaviour
    {
        private void Start()
        {
            var canvas = GetComponent<Canvas>();
            Camera uiCamera = ObjectsProvider.Instance.UICamera;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
        }
    }
}
