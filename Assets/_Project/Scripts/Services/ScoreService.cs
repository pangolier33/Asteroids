using UnityEngine;

namespace _Project.Scripts.Services
{
    public class ScoreService : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
        }

        public int ShowCurrentScore()
        {
            return PlayerPrefs.GetInt("CurrentScore");
        }
    }
}
