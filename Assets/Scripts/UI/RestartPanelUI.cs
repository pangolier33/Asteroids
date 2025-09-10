using UnityEngine;
using TMPro;

public class RestartPanelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    private int _score;

    private void OnEnable()
    {
        EnemyEvents.OnEnemyDestroyed += UpdateScore;
    }

    public void UpdateScore()
    {
        _score++;
        if (_scoreText.gameObject.activeSelf == false) return;

        _scoreText.text = $"Score: {_score}";
    }

    private void OnDisable()
    {
        EnemyEvents.OnEnemyDestroyed -= UpdateScore;
    }
}
