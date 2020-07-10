using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public bool countEnabled = true;

    void Update()
    {
        if(countEnabled)
            score += (int)Time.deltaTime;
        scoreText.text = score.ToString("0");
    }

    public void SetScore(int newScore)
    {
        PlayerPrefs.SetInt("score", newScore);
        score = newScore;
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", (int)score);
    }

    void OnEnable()
    {
        score = PlayerPrefs.GetInt("score", 0);
    }
}
