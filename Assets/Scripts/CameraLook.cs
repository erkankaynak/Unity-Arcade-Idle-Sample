using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float lookSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.x < 30f && transform.rotation.x > -10f)
            transform.Rotate(Vector3.left, Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime);

        
    }
}
