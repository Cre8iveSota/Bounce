using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ImgsFillDynamic staminaGauge;
    [SerializeField] private float originalMoveSpeed = 7.5f;
    [SerializeField] private float currentStamina;
    private float maxStamina = 10;
    private float moveSpeed;
    private int speedDashRate;
    bool canRun = true;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaGauge.SetValue(currentStamina / maxStamina);
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        canRun = currentStamina / maxStamina > 0 ? true : false;
        if (gameManager.isGameEnd) return;
        #region PLAYER MOVEMENT
        if (Input.GetKey(KeyCode.Mouse0) && canRun)
        {
            speedDashRate = 3;
            UpdateStamina(false);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !canRun)
        {
            speedDashRate = 1;
            currentStamina = 0;
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            speedDashRate = 1;
            UpdateStamina(true);
        }
        moveSpeed = originalMoveSpeed * speedDashRate;
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

    private void UpdateStamina(bool increase)
    {
        float changeSpeed = increase ? 2f : -5f;
        currentStamina += changeSpeed * Time.deltaTime;

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        staminaGauge.SetValue(currentStamina / maxStamina, true);
    }
}
