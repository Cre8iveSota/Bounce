using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ImgsFillDynamic ImgsFDplayer, ImgsFDenemy;
    public bool isGameEnd;
    private float yourCurrentHp, enemyCurrentHp;
    [SerializeField] private float yourMaxHp = 10;
    [SerializeField] private float enemyMaxHp = 10;
    void Start()
    {
        yourCurrentHp = yourMaxHp;
        enemyCurrentHp = enemyMaxHp;
        this.ImgsFDplayer.SetValue(yourCurrentHp / yourMaxHp, false);
        this.ImgsFDenemy.SetValue(enemyCurrentHp / enemyMaxHp, false);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public Vector3 CalcurateUnitVector(Vector3 to, Vector3 from)
    {
        // return new Vector3((from.x - to.x) / math.sqrt(math.pow(from.x, 2) + math.pow(to.x, 2)), (from.y - to.y) / math.sqrt(math.pow(from.y, 2) + math.pow(to.y, 2)), (from.z - to.z) / math.sqrt(math.pow(from.z, 2) + math.pow(to.z, 2)));
        Vector3 direction = to - from;
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

            if (yourCurrentHp == 0) { isGameEnd = true; }
        }
        else
        {
            if (isGameEnd) return;
            enemyCurrentHp--;
            Debug.Log("en hp: " + enemyCurrentHp);
            this.ImgsFDenemy.SetValue(enemyCurrentHp / enemyMaxHp);

            if (enemyCurrentHp == 0) { isGameEnd = true; }
        }
    }
}
