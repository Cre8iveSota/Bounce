using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSourceSE; // SE source
    public AudioClip[] audioClipsSE; // item


    public AudioSource audioSourceBGM; // BGM source
    public AudioClip[] audioClipsBGM; // item of BGM(0:Menu 1:Game)
    public static SoundManager instance;
    private readonly int totalPlayingTweens = DOTween.TotalPlayingTweens();
    int seCount = 0;
    float volume;


    //SoundManager.instance.PlaySE(4);
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //Ensure that sound is not interrupted when transitioning between scenes
            DontDestroyOnLoad(this.gameObject);
            // // DOTweenの初期化
            // DOTween.Init();

            // // トゥイーンの容量を手動で設定
            // DOTween.SetTweensCapacity(2000, 100);
        }
        else
        {
            // If this SoundManager is prefabricated elsewhere, remove it to prevent the same sound from being played back and creating a multiplicity of sounds.
            Destroy(this.gameObject);
        }
    }

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void PlayBGM(string sceneName)
    {
        float fadeOutDuration = 1f;
        audioSourceBGM.DOFade(0f, fadeOutDuration).OnComplete(() =>
        {
            // BGM 停止
            audioSourceBGM.Stop();

            // 新しい BGM を設定
            switch (sceneName)
            {
                default:
                case "Start":
                    audioSourceBGM.clip = audioClipsBGM[0];
                    break;
                case "Stage1":
                    audioSourceBGM.clip = audioClipsBGM[1];
                    break;
                case "Stage2":
                    audioSourceBGM.clip = audioClipsBGM[2];
                    break;
            }

            // BGM フェードイン
            float fadeInDuration = 1f;
            audioSourceBGM.volume = 0f;
            audioSourceBGM.Play();
            audioSourceBGM.DOFade(.5f, fadeInDuration);
        });
    }
    // public void PlayBGM(int sceneIndex)
    // {
    //     float fadeOutDuration = 1f;
    //     audioSourceBGM.DOFade(0f, fadeOutDuration).OnComplete(() =>
    //     {
    //         // BGM 停止
    //         audioSourceBGM.Stop();

    //         // 新しい BGM を設定
    //         switch (sceneIndex)
    //         {
    //             default:
    //             case 0:
    //                 audioSourceBGM.clip = audioClipsBGM[0];
    //                 break;
    //             case 1:
    //                 audioSourceBGM.clip = audioClipsBGM[1];
    //                 break;
    //         }

    //         // BGM フェードイン
    //         float fadeInDuration = 1f;
    //         audioSourceBGM.volume = 0f;
    //         audioSourceBGM.Play();
    //         audioSourceBGM.DOFade(.5f, fadeInDuration);
    //     });
    // }

    public void PlaySE(int index)
    {
        seCount++;
        if (index == 3)
        {
            volume = .5f;
        }
        else
        {
            volume = 1;
        }
        audioSourceSE.DOFade(volume, 0f).OnComplete(() =>
        {
            audioSourceSE.PlayOneShot(audioClipsSE[index]);
            seCount--;
        });
    }



    public void StopAllSE()
    {
        Debug.Log("totalPlayingTweens se stop" + totalPlayingTweens);

        if (audioSourceSE.isPlaying)
        {
            DOTween.KillAll(true);
            audioSourceSE.Stop();
            seCount = 0;
        }
    }

    public void PlayClearBGM()
    {
        Debug.Log("totalPlayingTweens clear" + totalPlayingTweens);

        audioSourceBGM.Stop();
        StopAllSE();
        float fadeOutDuration = 1f;
        audioSourceBGM.DOFade(0f, fadeOutDuration).OnComplete(() =>
        {
            // BGM 停止

            audioSourceBGM.clip = audioClipsBGM[2];
            float fadeInDuration = 1f;
            audioSourceBGM.volume = 0f;
            audioSourceBGM.Play();
            audioSourceBGM.DOFade(1f, fadeInDuration);
        });
    }
}
