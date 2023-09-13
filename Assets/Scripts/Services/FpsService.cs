using UnityEngine;

namespace Services
{
    public class FpsService : MonoBehaviourSingleton<FpsService>
    {
        protected override void Awake()
        {
            base.Awake();

            Set60TargetFps();
        }

        private void Set60TargetFps()
        {
#if !UNITY_WEBGL && !UNITY_EDITOR
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 0;
#endif
        }
    }
}