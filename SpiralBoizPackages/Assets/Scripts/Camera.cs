using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private Vector3 original_pos;
    private bool shake_camera = false;
    private float shake_strength = 0;

    private float time_passed = 0;

    //Camera Following GameObject
    private bool isFollowing = false;
    public float smoothSpeed = 0.125f;
    public GameObject obj; //Object that the camera follows
    private Vector3 offset;

    void Start()
    {
        if(obj)
        {
            isFollowing = true;
            offset = transform.position - obj.transform.position;
        }

        original_pos = transform.position;
    }

    void Update()
    {
        if (shake_camera)
        {
            ShakeCamera();
        }
    }

    private void FixedUpdate()
    {
        if (isFollowing)
        {
            Follow();
        }
    }

    //Follow function
    private void Follow()
    {
        Vector3 desiredPoisition = obj.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPoisition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    public void setObjectToFollow(GameObject newObj)
    {
        obj = newObj;
    }

    //Camera Shake Functions 
    private void ShakeCamera()
    {
        transform.position = original_pos + Random.insideUnitSphere * shake_strength;
        time_passed += time_passed + Time.deltaTime;
        if (time_passed > 1.0f)
        {
            shake_camera = false;
            transform.position = original_pos;
            time_passed = 0;
        }
    }

    public void CameraShake(float strength)
    {
        shake_strength = strength;
        shake_camera = true;

        transform.LookAt(obj.transform);
    }


}
