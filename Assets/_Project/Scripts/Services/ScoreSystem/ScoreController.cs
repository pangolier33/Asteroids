namespace _Project.Scripts.Services.ScoreSystem
{
    public class ScoreController
    {
        private int _enemyScore = 1;
    
        public int UfoKilledScore { get; private set; }
        public int AsteroidsKilledScore { get; private set; }
    
        public int TotalScore => UfoKilledScore + AsteroidsKilledScore;
    
        public void AddUfoKill()
        {
            UfoKilledScore += _enemyScore;
        }
    
        public void AddAsteroidKill()
        {
            AsteroidsKilledScore += _enemyScore;
        }
    }
}
