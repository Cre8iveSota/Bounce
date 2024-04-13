using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameEnd) return;
        #region PLAYER MOVEMENT
        // Move the player w/ keyboard
        Vector3 direction = new Vector3(0, 0, 0);
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction = Vector3.ClampMagnitude(direction, 1f);
        transform.position += direction * moveSpeed * Time.deltaTime;
        RestrectionMovement();
        #endregion
    }

    private void RestrectionMovement()
    {
        if (14 < transform.position.x)
        {
            transform.position = new Vector3(14, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -14)
        {
            transform.position = new Vector3(-14, transform.position.y, transform.position.z);
        }
        if (9 < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 9);
        }
        else if (transform.position.z < -9)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -9);
        }
    }
}
