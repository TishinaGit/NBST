
using UnityEngine;

namespace EntryPoint
{
    public class ConfigManager : MonoBehaviour
    {
        public static ConfigManager Instance;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}