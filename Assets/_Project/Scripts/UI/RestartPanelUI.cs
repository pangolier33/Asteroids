using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        private TMP_Text _scoreText;
        private int _score;

        private void OnEnable()
        {
            _scoreText = gameObject.transform.GetChild(2).GetComponent<TMP_Text>();
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
