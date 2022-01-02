using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    [SerializeField] private Image[] alwaysColoredImages = new Image[3];
    private Image currentTickboxImage, endLevel, progression;
    private Text endLevelText, startLevelText, currentTickboxText;
    [SerializeField] private Text levelCompeleteMesssage;
    private RectTransform currentTickbox;
    private Color color;
    // Start is called before the first frame update
    void Awake()
    {
        alwaysColoredImages[0] = base.transform.GetChild(0).GetComponent<Image>();
        alwaysColoredImages[1] = base.transform.GetChild(1).GetComponent<Image>();
        alwaysColoredImages[2] = base.transform.GetChild(3).GetComponent<Image>();
        endLevel = base.transform.GetChild(4).GetComponent<Image>();

        endLevelText = endLevel.transform.GetChild(0).GetComponent<Text>();
        startLevelText = base.transform.GetChild(3).GetChild(0).GetComponent<Text>();

        progression = base.transform.GetChild(2).GetChild(0).GetComponent<Image>();
        currentTickbox = base.transform.GetChild(2).GetChild(1).GetComponent<RectTransform>();
        currentTickboxImage = currentTickbox.GetComponent<Image>();
        currentTickboxText = currentTickbox.GetChild(0).GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (progression.fillAmount != 1)
        {
            SetProgression(BallHandler.GetZ() / GameController.Instance.GetFinishlineDistance());
        }
        else if (progression.fillAmount >= 1 && BallHandler.GetZ() == 0) { SetProgression(0); }

        UpdateColors();

        startLevelText.text = PlayerPrefs.GetInt("Level").ToString();
        endLevelText.text = (PlayerPrefs.GetInt("Level") + 1).ToString();

    }


    private void SetProgression(float percent)
    {
        progression.fillAmount = percent;
        currentTickbox.anchorMin = new Vector2(percent, 0);
        currentTickbox.anchorMax = currentTickbox.anchorMin;
        currentTickboxText.text = Mathf.RoundToInt(percent * 100) + " %";
    }

    private void UpdateColors()
    {
        color = BallHandler.GetColor();
        if (progression.fillAmount == 1)
        {
            endLevel.color = this.color;
            endLevelText.color = Color.white;

            levelCompeleteMesssage.gameObject.SetActive(true);
            levelCompeleteMesssage.text = "Level " + PlayerPrefs.GetInt("Level") + " Complete!";
        }
        else
        {
            endLevel.color = Color.white;
            endLevelText.color = color;
            levelCompeleteMesssage.gameObject.SetActive(false);
        }

        foreach (Image image in alwaysColoredImages)
        {
            image.color = color;
        }

        progression.color = color;
        currentTickboxImage.color = color;

    }
}
