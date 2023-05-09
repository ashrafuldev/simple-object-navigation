using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLikeCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 0.5f; 
    [SerializeField] private float zoomSpeed = 0.5f;


    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

      
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back * moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left * moveSpeed;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right * moveSpeed;
        }

        // rotate the camera 

        float mouseMoveY = Input.GetAxis("Mouse Y");
        float mouseMoveX = Input.GetAxis("Mouse X");

        // move the camera 

        if (Input.GetKey(KeyCode.Mouse0))
        {
            move += Vector3.up * mouseMoveY * -moveSpeed;
            move += Vector3.right * mouseMoveX * -moveSpeed;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.RotateAround(transform.position, transform.right, mouseMoveY * -rotationSpeed);
            transform.RotateAround(transform.position, transform.up, mouseMoveX * rotationSpeed);
        }
        transform.Translate(move);
    }

    private void LateUpdate()
    {
        // scroll to zome
        float mouseScrool = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * mouseScrool * zoomSpeed);
    }
}
