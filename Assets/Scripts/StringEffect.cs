using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEffect : MonoBehaviour
{
    BallController ballController;
    StringAttach stringAttach;
    private void Start()
    {
        stringAttach = GameObject.FindGameObjectWithTag("Player").GetComponent<StringAttach>();
    }
    private void OnParticleCollision(GameObject obj)
    {
        Debug.Log("Particle collided with: " + obj.name);
        if (obj.tag == "string")
        {
            stringAttach.isAttached = true;
            ballController = GetComponent<BallController>();
            ballController.isPermittedExControl = true;
        }
    }
}
