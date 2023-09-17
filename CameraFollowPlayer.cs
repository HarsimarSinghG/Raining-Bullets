using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //private float xFollowDis = 0f;
    //private float zFollowDis = -7f;
    //private float followThreshold = 0.02f;
    //private float followSpeed = 7;
    //private void Update()
    //{
    //    float xDis = transform.position.x - Player.Instance.transform.position.x;
    //    float zDis = transform.position.z - Player.Instance.transform.position.z;
    //    Vector3 newPosition = transform.position;
    //    float xThreshold = Mathf.Abs(xDis - xFollowDis);
    //    float zThreshold = Mathf.Abs(zDis - zFollowDis);
    //    if (xThreshold > followThreshold)
    //    {
    //        if (xDis > xFollowDis)
    //        {
    //            newPosition.x -= transform.right.x;
    //        }

    //        else if (xDis < followThreshold)
    //        {
    //            newPosition.x += transform.right.x;
    //        }
    //    }

    //    if (zThreshold > followThreshold)
    //    {
    //        if (zDis > zFollowDis) { newPosition.z -= transform.forward.z; }
    //        else { newPosition.z += transform.forward.z; }
    //    }

    //    transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);

    //}


    //private void LateUpdate()
    //{
    //    if (Player.Instance != null) { transform.position = Player.Instance.transform.position + new Vector3(0, 0, -13); }
    //}

    private void Start()
    {
        transform.position = new Vector3(0, 0, -25);
    }

    private void Update()
    {
        if(Player.Instance!=null || !Player.Instance.ReturnIsDead())
        HandleMovement();
    }

    private void HandleMovement()
    {
        //if (GetCameraFollowPositionFunc == null) return;
        Vector3 cameraFollowPosition = Player.Instance.transform.position;
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 3f;

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

}
