using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Score UI class to show a score in score section
/// </summary>
public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public float score = 0;
    public bool countEnabled = true;

    void Update()
    {
        if(countEnabled)
            //count time in seconds as a score
            score += Time.deltaTime;
        scoreText.text = score.ToString("0");
    }

    /// <summary>
    /// Sets a new score
    /// </summary>
    /// <param name="newScore">New score</param>
    public void SetScore(int newScore)
    {
        PlayerPrefs.SetInt("score", newScore);
        score = (float)newScore;
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
