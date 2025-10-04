using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Project.Scripts.Tools
{
    public class LoadingScene : MonoBehaviour
    {
        [SerializeField] private int _scene = 0;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Loading);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Loading);
        }

        public void Loading()
        {
            SceneManager.LoadScene(_scene);
        }
    }
}
