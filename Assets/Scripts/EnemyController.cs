using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Vector3 direction, lastPos;
    float moveSpeed = 12;
    private float nearestBallDistance = float.MaxValue;
    private float time;
    bool isOverriding;
    GameObject[] balls;
    GameObject nearestBall, secondNearestBall, targetBall;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameEnd) return;
        time += Time.deltaTime;
        if (isOverriding)
        {
            direction = gameManager.CalcurateUnitVector(GameObject.FindGameObjectWithTag("Player").transform.position, this.transform.position);
        }
        else if (targetBall != null && !targetBall.GetComponent<BallController>().isAtackingEnemy)
        {
            direction = gameManager.CalcurateUnitVector(targetBall.transform.position, this.transform.position);
        }
        else if (targetBall != null)
        {
            direction = -gameManager.CalcurateUnitVector(targetBall.transform.position, this.transform.position);
        }
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        lastPos = transform.position;

        if (time > 1f) { targetBall = GetNearBall(); time = 0; }
    }

    private GameObject GetNearBall()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        nearestBallDistance = float.MaxValue;
        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i].GetComponent<BallController>().isPermittedControlPlayer) continue;
            float diff = Vector3.Distance(balls[i].transform.position, transform.position);
            if (diff < nearestBallDistance)
            {
                nearestBallDistance = diff;
                nearestBall = balls[i];
            }
        }
        return nearestBall;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            transform.position = transform.position - transform.forward * 1f; // forward 方向に少し戻る
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            isOverriding = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isOverriding = false;
    }
}
