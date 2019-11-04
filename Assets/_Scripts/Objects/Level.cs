using UnityEngine;

[System.Serializable]
public class Level
{
    public Level()
    {
        _highScore = 0;
        _currentScore = 0;
        _unlocked = false;
        _defeated = false;
    }

    public int getCurrentStars()
    {
        if (_currentScore == 0)
            return 0;
        else if (_currentScore <= _thereStarScore)
            return 3;
        else if (_currentScore <= _twoStarScore)
            return 2;
        else
            return 1;
    }
    public int getHighScoreStars()
    {
        if (_highScore == 0)
            return 0;
        else if (_highScore <= _thereStarScore)
            return 3;
        else if (_highScore <= _twoStarScore)
            return 2;
        else
            return 1;
    }
    [SerializeField] private GameObject _levelPrefab;
    [Space(10)]
    [SerializeField] private int _highScore, _currentScore;
    [Space(10)]
    [SerializeField] private int _oneStarScore;
    [SerializeField] private int _twoStarScore;
    [SerializeField] private int _thereStarScore;
    [Space(10)]
    [SerializeField] private bool _unlocked;
    [SerializeField] private bool _defeated;

    public GameObject LevelPrefab { get { return _levelPrefab; } }
    public int HighScore { get { return _highScore; } set { _highScore = value; } }
    public int CurrentScore { get { return _currentScore; } set { _currentScore = value; } }
    public bool Unlocked { get { return _unlocked; } set { _unlocked = value; } }
    public bool Defeated { get { return _defeated; } set { _defeated = value; } }

}
