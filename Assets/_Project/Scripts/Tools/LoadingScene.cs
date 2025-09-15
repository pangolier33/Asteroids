using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Tools
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private int _scene = 0;
        [SerializeField] private GameObject LoadingScreen;

        public void Loading()
        {
            LoadingScreen.SetActive(true);

            StartCoroutine(LoadAsync());
        }

        private IEnumerator LoadAsync()
        {
            AsyncOperation loadAsync = SceneManager.LoadSceneAsync(_scene);
            loadAsync.allowSceneActivation = false;

            while (!loadAsync.isDone)
            {
                if (loadAsync.progress >= 0.9f && !loadAsync.allowSceneActivation)
                {
                    loadAsync.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
