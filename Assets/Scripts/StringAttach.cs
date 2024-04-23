using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringAttach : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    GameObject nearestBall;
    public ParticleSystem particleSystem;
    private float nearestBallDistance = float.MaxValue;
    public bool isAttached;
    public bool isReleased;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // if (isAttached) { isReleased = true; isAttached = false; return; }
            Vector3 direction = (GetNearBall().transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            particleSystem.transform.rotation = rotation;
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }

        if (isAttached)
        {
            particleSystem.Play();
            Vector3 direction = (GetNearBall().transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            particleSystem.transform.rotation = rotation;
        }
    }
    private GameObject GetNearBall()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        nearestBallDistance = float.MaxValue;
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
