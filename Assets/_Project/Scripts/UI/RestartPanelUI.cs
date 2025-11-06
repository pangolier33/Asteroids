using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI
{
    public class RestartPanelUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _recordText;

        public void SetScore(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public void SetRecord(int record)
        {
            _recordText.text = "Record: " + record;
        }
    }
}
