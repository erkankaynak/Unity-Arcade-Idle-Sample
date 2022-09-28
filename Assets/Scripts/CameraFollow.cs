using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform player;

    [SerializeField] Vector3 offset = new Vector3(0f,12f, -10f);

    void Update()
    {
        transform.position = player.position + offset;    
    }
}
