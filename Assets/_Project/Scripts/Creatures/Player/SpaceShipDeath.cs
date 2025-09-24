using _Project.Scripts.UI;
using UnityEngine;

public class SpaceShipDeath : MonoBehaviour
{
    [SerializeField] private GameObject _restartCanvas;
    private int _score;
    
    private void OnEnable()
    {
        EnemyEvents.OnEnemyDestroyed += UpdateScore;
    }
        
    private void OnDisable()
    {
        EnemyEvents.OnEnemyDestroyed -= UpdateScore;
    }
    
    private void UpdateScore()
    {
        _score++;
    }

    public void Death()
    {
        _restartCanvas = Instantiate(_restartCanvas);
        RestartPanelUI restartPanelUI = _restartCanvas.GetComponent<RestartPanelUI>();
        restartPanelUI.SetScore(_score);
        Destroy(gameObject);
    }
}
