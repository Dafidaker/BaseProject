using UnityEditor;
using UnityEngine;
using Utility;

[CustomEditor(typeof(TestFeatures))]
public class TestFeaturesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TestFeatures myScript = (TestFeatures)target;
        if (GUILayout.Button("Test Transition to Next Scene"))
        {
            TransitionSceneManager.Instance.ActivateDontDestroyOnLoad();
            TransitionSceneManager.Instance.FadeToNextScene();
        }
        
    }
}


