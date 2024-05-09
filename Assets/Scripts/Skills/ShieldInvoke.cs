using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldInvoke : MonoBehaviour
{
    [SerializeField] GameObject shieldPrefabObj;
    DirectIndicater directIndicater;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        directIndicater = GameObject.FindGameObjectWithTag("direction").GetComponent<DirectIndicater>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerController.currentStamina > 2f)
        {
            playerController.UpdateStamina(false, -3 / Time.deltaTime);
            SoundManager.instance.PlaySE(7);
            GameObject shildPrefab = Instantiate(shieldPrefabObj, new Vector3(
            transform.position.x + directIndicater.shieldPosOffset.x,
            transform.position.y + directIndicater.shieldPosOffset.y + 2,
            transform.position.z + directIndicater.shieldPosOffset.z
            ), Quaternion.identity);

            shildPrefab.transform.localScale = new Vector3(3, 3, 3);
            shildPrefab.transform.rotation = Quaternion.Euler(30, -90 + directIndicater.shiedTargetAngle, 0);
            Destroy(shildPrefab, 1);


        }
    }
}
