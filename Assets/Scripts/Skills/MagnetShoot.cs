using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetShoot : MonoBehaviour
{
    public GameObject magnet;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        magnet.SetActive(true);
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (this.GetComponent<PlayerController>().currentStamina < 0.1f)
        {
            magnet.GetComponent<MagnetEffect>().enable = false;
            magnet.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
        else if (Input.GetKey(KeyCode.Mouse1))
        {
            magnet.GetComponent<MagnetEffect>().enable = true;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);

                direction = new Vector3(x, 0, z);

                magnet.transform.position += (direction - new Vector3(transform.position.x, 0, transform.position.z)) * Time.deltaTime * 1.0f;
                this.GetComponent<PlayerController>().UpdateStamina(false, -4);
            }
        }
        else
        {
            magnet.GetComponent<MagnetEffect>().enable = false;
            magnet.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
