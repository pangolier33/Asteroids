using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Tools
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private int _scene = 0;
        
        public void Loading()
        {
            SceneManager.LoadScene(_scene);
        }
    }
}
