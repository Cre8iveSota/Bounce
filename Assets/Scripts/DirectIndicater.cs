using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectIndicater : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject basePoint;
    Vector3 mousePos;

    public Vector3 shieldPosOffset;
    public float shiedTargetAngle;
    float X;

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        X = GetAngle(new Vector2(basePoint.transform.position.x, basePoint.transform.position.z), new Vector2(mousePos.x, mousePos.z));
        // Debug.Log("pos: " + new Vector3(-70 * Mathf.Sin(X), 70 * Mathf.Cos(X) - 15, 0));
        transform.SetLocalPositionAndRotation(new Vector3(-70 * Mathf.Sin(X), 70 * Mathf.Cos(X) - 15, 0), Quaternion.Euler(0, 0, X * Mathf.Rad2Deg));

        shieldPosOffset = new Vector3((float)(1.3 * Mathf.Cos(X + Mathf.PI / 2f)), 0, (float)(1.3 * Mathf.Sin(X + Mathf.PI / 2f)));
        shiedTargetAngle = Mathf.Rad2Deg * Mathf.Atan2((float)(-1.3 * Mathf.Sin(X + Mathf.PI / 2f)), (float)(1.3 * Mathf.Cos(X + Mathf.PI / 2f)));
        // Debug.Log("kakudo: " + shiedTargetAngle);
    }

    float GetAngle(Vector2 start, Vector2 target)
    {
        Vector2 vector = target - start;
        float angle = Mathf.Atan2(vector.y, vector.x);
        angle -= Mathf.PI / 2f;
        return angle;
    }
}
