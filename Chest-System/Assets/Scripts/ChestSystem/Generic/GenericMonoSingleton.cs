using UnityEngine;

namespace ChestSystem
{
    public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                instance = (T)this;
                DontDestroyOnLoad(this as T);
                // will only work for root gameobjects.
            }
            else
            {
                Destroy(this);
            }
        }
    }
}