using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;

    public void Init()
    {
        ScoreText.text = "Score: 0";
    }
    public void UpdateScore(int score)
    {
        ScoreText.text = "Score: " + score;
    }
}
