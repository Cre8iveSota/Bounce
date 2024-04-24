using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacterMangaer : MonoBehaviour
{
    [SerializeField] private GameObject choseCharacterImg;
    [SerializeField] private Sprite[] characterSourceImg = new Sprite[3];
    [SerializeField] private GameObject[] characterButtons = new GameObject[3];
    [SerializeField] private TMP_Text cautionText, Explanation;
    [SerializeField] private GameObject characterDataTransfer;
    public static List<int> buttonStatus = new List<int>();
    // Start is called before the first frame update
    void Start()
    {
        characterButtons[0].GetComponent<Image>().sprite = characterSourceImg[0];
        characterButtons[1].GetComponent<Image>().sprite = characterSourceImg[1];
        characterButtons[2].GetComponent<Image>().sprite = characterSourceImg[2];
        cautionText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChoseCharacter(int characterNum) // 0to2
    {
        buttonStatus.Clear();
        buttonStatus.Add(characterNum);
        characterDataTransfer.GetComponent<CharacterDataTransfer>().UpdateChoseCharacterNum(characterNum);

        foreach (int i in buttonStatus)
        {
            Debug.Log(i + "chosen");
        }
        InsertCharacterImage(characterNum);
    }
    private void InsertCharacterImage(int index)
    {
        choseCharacterImg.GetComponent<Image>().sprite = characterSourceImg[index];
        if (index == 0) Explanation.text = "Left Click:<br>Dash.<br><br>Right Click:<br>Attach the spider thread to the nearest ball.";
        else if (index == 1) Explanation.text = "Left Click:<br>Dash.<br><br>Right Click:<br>It can fire and control Magnet, which can pull the ball.";
        else { Explanation.text = ""; }
    }

    public void LoadGame(string sceneName)
    {
        if (buttonStatus.Count != 0)
        {
            cautionText.enabled = false;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            cautionText.enabled = true;
        }
    }

}
