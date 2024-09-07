using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility.Scene
{
    public class SceneUtil : MonoBehaviour
    {
        public static bool IsSceneLoadableFromPath(string scenePath)
        {
            if (string.IsNullOrWhiteSpace(scenePath))
                return false;
            if (SceneUtility.GetBuildIndexByScenePath(scenePath) >= 0)
                return true;
            if (SceneUtility.GetBuildIndexByScenePath(scenePath + ".unity") >= 0)
                return true;
            if (SceneUtility.GetBuildIndexByScenePath("Assets/" + scenePath) >= 0)
                return true;
        
            return SceneUtility.GetBuildIndexByScenePath("Assets/" + scenePath + ".unity") >= 0;
        }
        
        public static string GetNextSceneName()
        {
            // Get the current scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Calculate the next scene index
            int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            // Get the path of the next scene
            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);

            // Extract the scene name from the path
            string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);

            return nextSceneName;
        }
    }
    
    
}
