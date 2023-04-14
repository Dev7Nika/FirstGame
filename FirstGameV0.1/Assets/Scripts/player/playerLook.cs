using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    private PlayerInput.OnFootActions onFoot;
    public Camera cam;
    private float xRotation = 0f;

    public float xSensivity = 30f;
    public float ySensivity = 30f;


    public void ProcessLook(Vector2 input)
    {
        float iX = input.x;
        float iY = input.y;

        xRotation -= (iY * Time.deltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * (iX * Time.deltaTime) * xSensivity);
    }

}
