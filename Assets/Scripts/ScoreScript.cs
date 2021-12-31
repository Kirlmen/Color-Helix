using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private Text scoreText;
    private Text bestScoreText;


    void Awake()
    {
        bestScoreText = transform.GetChild(0).GetComponent<Text>();
        scoreText = transform.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        if (BallHandler.GetZ() == 0)
        {
            bestScoreText.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
        }
        else
        {
            bestScoreText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
        }

        scoreText.text = GameController.Instance.score.ToString();

        if (GameController.Instance.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", GameController.Instance.score);
        }
        bestScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
