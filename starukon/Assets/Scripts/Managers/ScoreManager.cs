using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;
    public int totalScore;

    public AudioSource audioSourceScore;
    private bool newTotal = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("totalScore");
        UIManager.Instance.totalScore.text = Instance.totalScore.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("lastScore", score);
        PlayerPrefs.SetInt("NewScore", totalScore);
    }

    public void SetScore(int s)
    {
        score += s;
        UIManager.Instance.score.text = score.ToString();

        if (score > totalScore)
        {
            newRecord();
        }
    }

    public void newRecord()
    {
        if (!newTotal)
        {
            audioSourceScore.Play();
            newTotal = true;
        }
        totalScore = score;
        UIManager.Instance.totalScore.text = totalScore.ToString();
    }
}
