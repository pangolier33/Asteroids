using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        [SerializeField] private int _scoreTextIndex = 2;
        
        private TMP_Text _scoreText;
        private int _score;

        private void OnEnable()
        {
            _scoreText = gameObject.transform.GetChild(_scoreTextIndex).GetComponent<TMP_Text>();
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
