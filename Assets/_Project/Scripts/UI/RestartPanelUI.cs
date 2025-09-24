using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
