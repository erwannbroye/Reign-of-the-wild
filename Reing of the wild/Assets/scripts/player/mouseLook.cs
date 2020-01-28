using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    // Start is called before the first frame update
    public float mouseSensivity = 100f;
    public Transform player;
    public Transform weapon;
    float xRotation = 0f;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70, 70);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        weapon.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
          if (Input.GetButtonDown("Cancel")) {
             Cursor.lockState = CursorLockMode.None;
         }
    }
}
