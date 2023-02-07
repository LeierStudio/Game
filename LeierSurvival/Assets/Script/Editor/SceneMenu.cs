using UnityEditor;
using UnityEditor.SceneManagement;

namespace Game.Editor
{
    public static class Constant
    {
        public static class Scene
        {
            public const string TownScene = "TownScene";
        }
    }

    public class SceneMenu
    {
        [MenuItem("Game/OpenScene/" + Constant.Scene.TownScene, false, 0)]
        static void OpenRootScene()
        {
            EditorOpenScene(Constant.Scene.TownScene);
        }

        static void EditorOpenScene(string sceneName)
        {
            EditorSceneManager.OpenScene($"Assets/Scene/{sceneName}.unity", OpenSceneMode.Single);
        }
    }
}