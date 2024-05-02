using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAffect : MonoBehaviour
{
    [SerializeField] GameObject shieldVfxPreObj;
    BallController experimentPermittedControlBall;
    // Update is called once per frame
    void Update()
    {
        // experimentPermittedControlBall.isPermittedControlPlayer = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            SoundManager.instance.PlaySE(7);
            GameObject prefabVfx = Instantiate(shieldVfxPreObj, transform.position, Quaternion.identity);
            Destroy(prefabVfx, 1);
            BallController bumpedBall = other.GetComponent<BallController>();
            bumpedBall.ChangeOwnerPlayer(true, true);
            // bumpedBall.isPermittedControlPlayer = true;
            bumpedBall.isAtackingPlayer = false;
            bumpedBall.isAtackingEnemy = true;
            bumpedBall.vfxEnemy.SetActive(false);
            experimentPermittedControlBall = bumpedBall;
        }
    }
}
