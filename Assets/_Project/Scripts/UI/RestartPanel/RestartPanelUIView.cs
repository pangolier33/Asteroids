using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.RestartPanel
{
    public class RestartPanelUIView : MonoBehaviour
    {
        public event Action RestartClicked;
        
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _recordText;
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            RestartClicked?.Invoke();
        }

        public void SetScore(int score)
        {
            _scoreText.text = $"Score: {score}";
        }

        public void SetRecord(int record)
        {
            _recordText.text = $"Record: {record}";
        }
    }
}
