using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    // Start is called before the first frame update
    string sceneName;
    public static int altCharaNum;
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        BGMSelecter(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadScene(string sceneName)
    {
        // SoundManager.instance.PlaySE(0); // TODO
        FadeIOManager.instane.FadeOutToIn(() => SceneManager.LoadScene(sceneName));
    }

    private void BGMSelecter(string sceneName)
    {
        switch (sceneName)
        {
            // case "Start": 
            //     SoundManager.instance.PlayBGM(0);
            //     break;
            // case "HowtoPlay":
            //     SoundManager.instance.PlayBGM(0);
            //     break;
            // case "CharacterSelection":
            //     SoundManager.instance.PlayBGM(0);
            //     break;
            // case "StageSelection":
            //     SoundManager.instance.PlayBGM(0);
            //     break;
            // case "Stage1":
            //     SoundManager.instance.PlayBGM(0);
            //     break;
            // case "Stage2":
            //     SoundManager.instance.PlayBGM(0);
            //     break;
        }
    }
}
