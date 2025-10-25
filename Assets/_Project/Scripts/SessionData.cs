using UnityEngine;

namespace _Project.Scripts
{
    public class SessionData : MonoBehaviour
    {
        [field: SerializeField] public int _enemyKilledScore { get; private set; }

        private int _enemyScore = 1;

        public void AddKillEvent()
        {
            _enemyKilledScore += _enemyScore;
        }
    }
}