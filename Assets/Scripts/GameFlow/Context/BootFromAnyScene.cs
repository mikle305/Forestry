using Additional.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace GameFlow.Context
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class BootFromAnyScene
    {
#if UNITY_EDITOR
        static BootFromAnyScene() 
            => EditorApplication.playModeStateChanged += Run;
#endif
        private static void Run(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                Run();
        }
        
        
#if !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
#endif
        private static void Run()
            => SceneManager.LoadScene(SceneNames.Boot);
        
    }
}