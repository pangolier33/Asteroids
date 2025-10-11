using UnityEngine;

namespace _Project.Scripts.Services
{
    public class ScoreService : MonoBehaviour
    {
        [field: SerializeField] public int Score  { get; private set; }

        private void Start()
        {
            Score = 0;
            PlayerPrefs.SetInt("CurrentScore", 0);
        }

        public int ShowCurrentScore()
        {
            return PlayerPrefs.GetInt("CurrentScore");
        }
    }
}
