using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 direction;
    float moveSpeed = 15;
    private float nearestBallDistance = float.MaxValue;
    private float time;
    GameObject[] balls;
    GameObject nearestBall, secondNearestBall, targetBall;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        targetBall = GetNearBall();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        if (!targetBall.GetComponent<Ball>().isAtackingEnemy)
        {
            direction = gameManager.CalcurateUnitVector(targetBall.transform.position, this.transform.position);
        }
        else
        {
            direction = -gameManager.CalcurateUnitVector(targetBall.transform.position, this.transform.position);
        }
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        if (time > 3) { targetBall = GetNearBall(); time = 0; }
    }

    private GameObject GetNearBall()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        {
            float diff = Vector3.Distance(balls[i].transform.position, transform.position);
            if (diff < nearestBallDistance)
            {
                nearestBallDistance = diff;
                nearestBall = balls[i];
            }
        }
        return nearestBall;
    }
}
