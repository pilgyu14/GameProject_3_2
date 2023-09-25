using System;
using UnityEngine;

namespace _01.Scripts
{
    public class CameraMovement : MonoBehaviour
    {
        private float speed = 0.06f;
        private float zoomSpeed = 10.0f;
        private float rotateSpeed = 0.1f;

        private Vector2 p1;
        private Vector2 p2;
        
        private float maxHeight = 60;
        private float minHeight = 10f;

        private float hsp;
        private float vsp;
        private float scrollSpeed;

        private Vector3 verticalMove;
        private Vector3 lateralMove;
        private Vector3 forwardMove;

        private Vector3 move;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.06f;
                zoomSpeed = 20.0f;
            }
            else
            {
                speed = 0.035f;
                zoomSpeed = 10.0f;
            }
            
            hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
            vsp = transform.position.y * speed * Input.GetAxis("Vertical");
            scrollSpeed = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

            if ((transform.position.y) >= maxHeight && (scrollSpeed > 0))
            {
                scrollSpeed = 0;
            }
            else if((transform.position.y)<= minHeight && (scrollSpeed < 0))
            {
                scrollSpeed = 0;
            }

            if ((transform.position.y + scrollSpeed) > maxHeight)
            {
                scrollSpeed = maxHeight - transform.position.y;
            }
            else if ((transform.position.y + scrollSpeed) < minHeight)
            {
                scrollSpeed = minHeight - transform.position.y;
            }

            verticalMove = new Vector3(0, scrollSpeed, 0);
            lateralMove = hsp * transform.right;
            forwardMove = transform.forward;
            forwardMove.y = 0;
            forwardMove.Normalize();
            forwardMove *= vsp;

            move = verticalMove + lateralMove + forwardMove;

            transform.position += move;
            
            getCameraRotation();
        }

        private void getCameraRotation()
        {
            if (Input.GetMouseButtonDown(1))
            {
                p1 = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                p2 = Input.mousePosition;

                float dx = (p2 - p1).x * rotateSpeed;
                float dy = (p2 - p1).y * rotateSpeed;
                
                transform.rotation *= Quaternion.Euler(new Vector3(0,dx, 0));
                transform.GetChild(0).transform.rotation *= Quaternion.Euler(new Vector3(-dy,0,0));

                p1 = p2;
            }
        }
    }
}