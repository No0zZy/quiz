using HGtest.Storage;
using HGtest.Utils;

namespace HGtest.Result
{
    public class HighScoreRecorder
    {
        private const string HighScoreKey = "HGtest.highscore";

        public int BestScore => GetBestScore();

        private readonly IStorage _storage;

        private MyScoreClass _cashedBestScore;
        private bool _isBestScoreCashed = false;

        public HighScoreRecorder(IStorage storage)
        {
            _storage = storage;
        }

        public bool TrySaveNewBestScore(int score)
        {
            if (score <= BestScore) 
                return false;

            _cashedBestScore.Score = score;
            _storage.Save<MyScoreClass>(HighScoreKey, _cashedBestScore);

            return true;
        }

        private int GetBestScore()
        {
            if (_isBestScoreCashed)
                return _cashedBestScore.Score;
            
            _cashedBestScore = _storage.IsExists(HighScoreKey) ? _storage.Load<MyScoreClass>(HighScoreKey) : new MyScoreClass();

            _isBestScoreCashed = true;

            return _cashedBestScore.Score;
        }
    }
}