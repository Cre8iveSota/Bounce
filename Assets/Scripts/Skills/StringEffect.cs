using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringEffect : MonoBehaviour
{
    BallController ballController;
    StringAttach stringAttach;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;
    private float radius = 4f;
    private float speed = 10f;
    private float time;
    float angle;
    bool isCatched;
    private void Start()
    {
        ball = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (ballController != null
        && ballController.isPermittedControlPlayer
        && ballController.isAtackingEnemy
        && player.GetComponent<PlayerController>().currentStamina > 0.1f
        // && !player.GetComponent<StringAttach>().isReleased
        )
        {
            angle = time * speed;
            Vector3 offset = new Vector3(Mathf.Sin(angle), .2f, Mathf.Cos(angle)) * radius;
            ball.transform.position = player.transform.position + offset;
            player.GetComponent<StringAttach>().isAttached = true; ;
            ballController.ChangeOwnerPlayer(true);
            player.GetComponent<PlayerController>().UpdateStamina(false, -3);
        }
        else if (ballController != null)
        {
            ballController.isPermittedControlPlayer = false;
            stringAttach.isAttached = false;
            player.GetComponent<StringAttach>().isReleased = false;
        }
    }
    private void OnParticleCollision(GameObject obj)
    {
        Debug.Log("Particle collided with: " + obj.name);
        if (!player.GetComponent<StringAttach>().isAttached && obj.tag == "string")
        {
            stringAttach = obj.gameObject.transform.parent.GetComponent<StringAttach>();
            stringAttach.isAttached = true;
            ballController = GetComponent<BallController>();
            ballController.isPermittedControlPlayer = true;
            ballController.isAtackingEnemy = true;
            ballController.vfxPlayer.SetActive(true);
        }
    }
}
