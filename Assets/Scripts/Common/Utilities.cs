using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    // Start is called before the first frame update
    string sceneName;
    public static int altCharaNum;
    static private int BGMplayNum = 0; // For using same bgm apart from Buttle Scene, this variable is employed
    void Start()
    {
        BGMplayNum++;
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Stage1") { SoundManager.instance.PlayBGM(sceneName); BGMplayNum = 0; }
        else if (sceneName == "Stage2") { SoundManager.instance.PlayBGM(sceneName); BGMplayNum = 0; }
        else if (sceneName == "Stage3") { SoundManager.instance.PlayBGM(sceneName); BGMplayNum = 0; }
        else if (sceneName == "Stage4") { SoundManager.instance.PlayBGM(sceneName); BGMplayNum = 0; }
        else if (sceneName == "Stage5") { SoundManager.instance.PlayBGM(sceneName); BGMplayNum = 0; }
        else if (BGMplayNum == 1) SoundManager.instance.PlayBGM(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadScene(string sceneName)
    {
        SoundManager.instance.PlaySE(0);
        FadeIOManager.instane.FadeOutToIn(() => SceneManager.LoadScene(sceneName));
    }
}
