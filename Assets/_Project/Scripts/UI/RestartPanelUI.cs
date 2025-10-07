using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private int _score;

        public void SetScore(int score)
        {
            _scoreText.text = "Score: " + score;
        }
    }
}
