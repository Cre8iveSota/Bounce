using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ImgsFillDynamic staminaGauge;
    [SerializeField] private float originalMoveSpeed = 7.5f;
    public float currentStamina;
    private float maxStamina = 10;
    private float moveSpeed;
    private int speedDashRate;
    bool canRun = true;
    float lastBumpedTime;
    GameManager gameManager;
    CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaGauge.SetValue(currentStamina / maxStamina);
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        vcam = GameObject.FindGameObjectWithTag("VC").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        canRun = currentStamina / maxStamina > 0 ? true : false;
        if (gameManager.isGameEnd) return;
        if (gameManager.gameTime - lastBumpedTime > .15f) vcam.enabled = true; // This is necessary for stopping camera shaking when player get refrection against from walls
        #region PLAYER MOVEMENT
        if (Input.GetKey(KeyCode.Mouse0) && canRun)
        {
            speedDashRate = 3;
            UpdateStamina(false, 0);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && !canRun)
        {
            speedDashRate = 1;
            currentStamina = 0;
        }
        else if (!Input.GetKey(KeyCode.Mouse0))
        {
            speedDashRate = 1;
            UpdateStamina(true, 0);
        }
        moveSpeed = originalMoveSpeed * speedDashRate;
        // Move the player w/ keyboard
        Vector3 direction = new Vector3(0, 0, 0);
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction = Vector3.ClampMagnitude(direction, 1f);
        transform.position += direction * moveSpeed * Time.deltaTime;
        // RestrectionMovement();
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
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

    public void UpdateStamina(bool increase, float skillcost)
    {
        float changeSpeed = increase ? 2f : -5f;
        if (skillcost != 0f)
        {
            changeSpeed = skillcost;
        }
        currentStamina += changeSpeed * Time.deltaTime;

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        staminaGauge.SetValue(currentStamina / maxStamina, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            lastBumpedTime = gameManager.gameTime;
            vcam.enabled = false;
            transform.position = transform.position - transform.forward * 1f; // forward 方向に少し戻る
        }
    }
}
