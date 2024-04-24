using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float magneticFieldDistance = 3;
    [SerializeField] GameObject magnetParticle;
    [SerializeField] GameObject[] affectedBalls;
    public bool enable;
    MagnetShoot magnetShoot;
    GameObject player;
    PlayerController playerController;
    Vector3 lastPos;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<MagnetShoot>() != null)
        {
            magnetShoot = player.GetComponent<MagnetShoot>();
        }
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // When it comes to collision to wall, getting direction is neccessary to make back the bumped character
        if (magnetShoot != null) transform.rotation = Quaternion.LookRotation(magnetShoot.direction);

        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        if (enable)
        {
            foreach (Transform obj in this.transform)
            {
                if (obj.tag != "Ball") continue;
                obj.gameObject.GetComponent<BallController>().isPermittedControlPlayer = true;
            }
            if (playerController.currentStamina > .3f)
            {
                magnetParticle.SetActive(true);
                GetBallsInMagneticField();
            }
            lastPos = transform.position;
        }
        else
        {
            magnetParticle.SetActive(false);
            foreach (Transform obj in this.transform)
            {
                if (obj.tag != "Ball") continue;
                obj.gameObject.GetComponent<BallController>().isPermittedControlPlayer = false;
                obj.transform.SetParent(this.transform.parent, false);
                obj.transform.localScale = Vector3.one * 2;

                obj.GetComponent<BallController>().vfxPlayer.SetActive(false);
                obj.transform.position = lastPos;
            }
        }
    }
    void GetBallsInMagneticField()
    {
        affectedBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in affectedBalls)
        {
            if (Vector3.Distance(ball.transform.position, this.transform.position) < magneticFieldDistance && enable)
            {
                ball.transform.SetParent(this.transform, false);
                ball.transform.localScale = Vector3.one * 4;
                ball.GetComponent<BallController>().ChangeOwnerPlayer(true);
                ball.GetComponent<BallController>().isAtackingPlayer = false;
                ball.GetComponent<BallController>().vfxEnemy.SetActive(false);
            }
            else
            {
                ball.transform.SetParent(this.transform.parent, false);
                ball.transform.localScale = Vector3.one * 2;
                ball.GetComponent<BallController>().isPermittedControlPlayer = false;
                ball.GetComponent<BallController>().vfxPlayer.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            transform.position = transform.position - transform.forward * .5f; // forward 方向に少し戻る
        }
    }
}
