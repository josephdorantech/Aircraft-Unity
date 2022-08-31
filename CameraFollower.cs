using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject cam;
    public GameObject marker;
    public float followSpeed = 2f;

    private bool alwaysVertical = true;

    void LateUpdate()
    {
        cam.transform.position = Vector3.Lerp(
                    cam.transform.position,
                    marker.transform.position,
                    1 / followSpeed * Time.deltaTime);

        float currentXangle = cam.transform.eulerAngles.x;
        float wantedXangle = marker.transform.eulerAngles.x;

        float currentYangle = cam.transform.eulerAngles.y;
        float wantedYangle = marker.transform.eulerAngles.y;

        float currentZangle = cam.transform.eulerAngles.z;

        float wantedZangle = 0;


        if (alwaysVertical)
        {
            wantedZangle = marker.transform.rotation.z;
        }
        else
        {
            wantedZangle = marker.transform.eulerAngles.z;
        }

        currentXangle = Mathf.LerpAngle(currentXangle, wantedXangle, 1 / followSpeed);
        currentYangle = Mathf.LerpAngle(currentYangle, wantedYangle, 1 / followSpeed);
        currentZangle = Mathf.LerpAngle(currentZangle, wantedZangle, 1 / followSpeed);

        cam.transform.rotation = Quaternion.Euler(currentXangle, currentYangle, currentZangle);
    }
}
