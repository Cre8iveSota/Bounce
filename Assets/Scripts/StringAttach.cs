using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringAttach : MonoBehaviour
{
    [SerializeField] private GameObject[] balls;
    GameObject nearestBall;
    public ParticleSystem particleSystem;
    private float nearestBallDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!particleSystem.isPlaying)
            {

                particleSystem.Play();
            }
            Vector3 direction = (GetNearBall().transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            particleSystem.transform.rotation = rotation;
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }
        }
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
