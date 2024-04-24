using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseStageMangaer : MonoBehaviour
{
    [SerializeField] private GameObject choseStageImgPanel;
    [SerializeField] private Sprite[] stageSourceImg = new Sprite[2];
    [SerializeField] private GameObject[] stageButtons = new GameObject[2];
    [SerializeField] private TMP_Text cautionText;
    public int choseStageNum;
    // Start is called before the first frame update
    void Start()
    {
        stageButtons[0].GetComponent<Image>().sprite = stageSourceImg[0];
        stageButtons[1].GetComponent<Image>().sprite = stageSourceImg[1];
        // stageButtons[2].GetComponent<Image>().sprite = stageSourceImg[2];
        cautionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChoseStage(int stageNum) // 1to2
    {
        choseStageNum = stageNum;
        choseStageImgPanel.GetComponent<Image>().sprite = stageSourceImg[stageNum - 1];
    }

    public void LoadGame()
    {
        if (choseStageNum != 0)
        {
            cautionText.enabled = false;
            SceneManager.LoadScene($"Stage{choseStageNum}");
        }
        else
        {
            cautionText.enabled = true;
        }
    }
}
