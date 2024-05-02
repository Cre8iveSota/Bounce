using UnityEngine;

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
    [SerializeField] private GameObject hitPrefabObj;
    ParticleSystem particle;
    GameObject player, enemy;
    public float gameTime;
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

        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    private void Update()
    {
        gameTime += Time.deltaTime;
        if (isGameEnd) return;
        time += Time.deltaTime;
        if (time < 3 * nextBallTime) return;
        time = 0;
        nextBallTime *= 3f;
        Instantiate(ballObj, new Vector3(Random.Range(-13, 12), 1, Random.Range(-8, 7)), Quaternion.identity);
    }
    private void CharacterInsert(int num)
    {
        foreach (GameObject eachPlayer in players)
        {
            int index = System.Array.IndexOf(players, eachPlayer);
            if (index == num)
            {
                players[index].SetActive(true);
            }
            else
            {
                players[index].SetActive(false);
            }
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
            SoundManager.instance.PlaySE(2);
            yourCurrentHp--;
            Debug.Log("pl hp: " + yourCurrentHp);
            this.ImgsFDplayer.SetValue(yourCurrentHp / yourMaxHp);
            GameObject hitPrefab = Instantiate(hitPrefabObj, player.transform);
            if (charaNum == 0) { hitPrefab.transform.localScale = new Vector3(4f, 4f, 4f); }
            else { hitPrefab.transform.localScale = new Vector3(2f, 2f, 2f); }
            Destroy(hitPrefab, 1);
            if (yourCurrentHp == 0)
            {
                gameOverPanel.SetActive(true);
                SoundManager.instance.PlaySE(6);
                isGameEnd = true;
            }
        }
        else
        {
            if (isGameEnd) return;
            SoundManager.instance.PlaySE(2);
            enemyCurrentHp--;
            Debug.Log("en hp: " + enemyCurrentHp);
            this.ImgsFDenemy.SetValue(enemyCurrentHp / enemyMaxHp);
            GameObject hitPrefab = Instantiate(hitPrefabObj, enemy.transform);
            Destroy(hitPrefab, 1);
            if (enemyCurrentHp == 0)
            {
                gameClearPanel.SetActive(true);
                SoundManager.instance.PlaySE(5);
                isGameEnd = true;
            }
        }
    }
}
