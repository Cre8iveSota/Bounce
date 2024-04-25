using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartPanelController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private TMP_Text countDownText;
    private float time = 0f;
    private int cnt = 0;
    GameManager gameManager;
    TextMeshProUGUI text;
    TextAnimateRotateAndEnlarge textAnimateRotateAndEnlarge;
    // Start is called before the first frame update
    void Awake()
    {
        startPanel.SetActive(true);
        textAnimateRotateAndEnlarge = countDownText.GetComponent<TextAnimateRotateAndEnlarge>();
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        gameManager.isGameEnd = true;
        textAnimateRotateAndEnlarge.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 3f)
        {
            gameManager.isGameEnd = false;
            startPanel.SetActive(false);
            textAnimateRotateAndEnlarge.enabled = false;
        }
        else if ($"{3 - time:0}" == "0")
        {
            countDownText.text = "Go!!";
            cnt++;
        }
        else if (time - 1 < cnt)
        {
            cnt++;
            countDownText.text = $"{3 - time:0}".ToString();
        }
    }
}
