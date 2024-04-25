using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectManagement : MonoBehaviour
{
    PlayerController playerController;
    MagnetShoot magnetShoot;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        magnetShoot = GetComponent<MagnetShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentStamina < 0.1)
        {
            magnetShoot.magnet.GetComponent<MagnetEffect>().enable = false;
            magnetShoot.magnet.transform.position = playerController.transform.position;
            magnetShoot.magnet.SetActive(false);
            magnetShoot.enabled = false;
        }
        else
        {
            magnetShoot.enabled = true;
        }
    }
}
