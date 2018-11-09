using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {

    private static Score single_instance = null;
    private int scorePositive;
    private int scoreNegative;
    private Text scorePositiveText;
    private Text scoreNegativeText;

    public Score(int scorePositive, int scoreNegative, string scorePositiveText, string scoreNegativeText)
    {
        this.scorePositiveText = GameObject.Find(scorePositiveText).GetComponent<UnityEngine.UI.Text>();
        this.scoreNegativeText = GameObject.Find(scoreNegativeText).GetComponent<UnityEngine.UI.Text>();
        UpdateScorePositive(scorePositive);
        UpdateNegativeScore(scoreNegative);
    }

    public Score(string scorePositiveText, string scoreNegativeText)
    {
        this.scorePositiveText = GameObject.Find(scorePositiveText).GetComponent<UnityEngine.UI.Text>();
        this.scoreNegativeText = GameObject.Find(scoreNegativeText).GetComponent<UnityEngine.UI.Text>();
        UpdateScorePositive(0);
        UpdateNegativeScore(0);
    }

    public static Score GetInstance(int scorePositive, int scoreNegative, string scorePositiveText, string scoreNegativeText)
    {
        if (single_instance == null)
        {
            single_instance = new Score(scorePositive, scoreNegative, scorePositiveText, scoreNegativeText);
        }

        return single_instance;
    }

    public static Score GetInstance(string scorePositiveText, string scoreNegativeText)
    {
        if (single_instance == null)
        {
            single_instance = new Score(scorePositiveText, scoreNegativeText);
        }

        return single_instance;
    }

    public int getScorePositive()
    {
        return scorePositive;
    }

    public int getScoreNegative()
    {
        return scoreNegative;
    }

    public void UpdateScorePositive(int pontos)
    {
        scorePositive += pontos;
        scorePositiveText.text = scorePositive.ToString();
    }

    public void UpdateNegativeScore(int pontos)
    {
        scoreNegative += pontos;
        scoreNegativeText.text = scoreNegative.ToString();
    }

    public void ResetScore()
    {
        UpdateScorePositive(0);
        UpdateNegativeScore(0);
    }
}
