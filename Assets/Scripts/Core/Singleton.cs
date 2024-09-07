using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
            
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance != null) return _instance;

                CreateSingleton();
            
                return _instance;
            }
        }
        
        private static void CreateSingleton()
        {
            var singleton = new GameObject();
            _instance = singleton.AddComponent<T>();
            singleton.name =  typeof(T) + " Singleton" ;
        }

        public  void ActivateDontDestroyOnLoad()
        {
            if (!_instance.gameObject.scene.name.Equals("DontDestroyOnLoad"))
            {
                DontDestroyOnLoad(_instance.gameObject);
            }
        }
        
        public  void DeactivateDontDestroyOnLoad()
        {
            if (_instance.gameObject.scene.name.Equals("DontDestroyOnLoad"))
            {
                SceneManager.MoveGameObjectToScene(Instance.gameObject,SceneManager.GetActiveScene()); 
            }
        }
    }
}
