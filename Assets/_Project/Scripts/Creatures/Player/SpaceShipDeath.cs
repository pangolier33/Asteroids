using _Project.Scripts.UI;
using UnityEngine;

public class SpaceShipDeath : MonoBehaviour
{
    [SerializeField] private RestartPanelUI _restartCanvas;
    
    private int _score;
    
    public void Death()
    {
        Instantiate(_restartCanvas.gameObject);
        _restartCanvas.SetScore(_score);
        Destroy(gameObject);
    }
    
    public void IncrementScore()
    {
        _score++;
    }
}
