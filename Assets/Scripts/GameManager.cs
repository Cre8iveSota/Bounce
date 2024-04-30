using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ImgsFillDynamic ImgsFDplayer, ImgsFDenemy;
    public bool isGameEnd;
    private float yourCurrentHp, enemyCurrentHp;
    [SerializeField] private float yourMaxHp = 10;
    [SerializeField] private float enemyMaxHp = 10;
    [SerializeField] GameObject ballObj, gameClearPanel, gameOverPanel;
    [SerializeField] GameObject[] players = new GameObject[2];
    [SerializeField] private YourCharacter yourCharacter;
    int charaNum;

    float time;
    private float nextBallTime = 0.5f;

    void Start()
    {
        // yourCharacter = AssetDatabase.LoadAssetAtPath<YourCharacter>("Assets/Scripts/YourCharacter.asset");
        yourCharacter = Resources.Load<YourCharacter>("YourCharacter");
        if (yourCharacter != null && yourCharacter.charaNum < 10)
        {
            charaNum = yourCharacter.charaNum;
            Debug.Log("testtttt: " + charaNum);
            CharacterInsert(charaNum);
        }
        else
        {
            Debug.Log("failedxxxxxx");
            charaNum = Utilities.altCharaNum;
            CharacterInsert(charaNum);
        }


        time = 0;
        yourCurrentHp = yourMaxHp;
        enemyCurrentHp = enemyMaxHp;
        this.ImgsFDplayer.SetValue(yourCurrentHp / yourMaxHp, false);
        this.ImgsFDenemy.SetValue(enemyCurrentHp / enemyMaxHp, false);
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }


    private void Update()
    {
        if (isGameEnd) return;
        time += Time.deltaTime;
        if (time < 3 * nextBallTime) return;
        time = 0;
        nextBallTime *= 3f;
        Instantiate(ballObj, new Vector3(Random.Range(-13, 12), 1, Random.Range(-8, 7)), Quaternion.identity);
    }
    private void CharacterInsert(int num)
    {
        if (num == 0)
        {
            players[0].SetActive(true);
            players[1].SetActive(false);
        }
        else if (num == 1)
        {
            players[0].SetActive(false);
            players[1].SetActive(true);
        }
    }
    public Vector3 CalcurateUnitVector(Vector3 to, Vector3 from)
    {
        // return new Vector3((from.x - to.x) / math.sqrt(math.pow(from.x, 2) + math.pow(to.x, 2)), (from.y - to.y) / math.sqrt(math.pow(from.y, 2) + math.pow(to.y, 2)), (from.z - to.z) / math.sqrt(math.pow(from.z, 2) + math.pow(to.z, 2)));
        Vector3 direction = new Vector3(to.x, 1, to.z) - new Vector3(from.x, 1, from.z);
        float magnitude = direction.magnitude;
        if (magnitude > 0)
        {
            return direction / magnitude;
        }
        else
        {
            return Vector3.zero; // ゼロベクトルを返すか、エラー処理を行います。
        }
    }
    public void Damage(bool isTargetPlaer)
    {
        if (isTargetPlaer)
        {
            if (isGameEnd) return;
            yourCurrentHp--;
            Debug.Log("pl hp: " + yourCurrentHp);
            this.ImgsFDplayer.SetValue(yourCurrentHp / yourMaxHp);

            if (yourCurrentHp == 0)
            {
                gameOverPanel.SetActive(true);
                isGameEnd = true;
            }
        }
        else
        {
            if (isGameEnd) return;
            enemyCurrentHp--;
            Debug.Log("en hp: " + enemyCurrentHp);
            this.ImgsFDenemy.SetValue(enemyCurrentHp / enemyMaxHp);

            if (enemyCurrentHp == 0)
            {
                gameClearPanel.SetActive(true);
                isGameEnd = true;
            }
        }
    }
}
