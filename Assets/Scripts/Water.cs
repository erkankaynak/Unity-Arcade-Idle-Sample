using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 moveDirection = Vector3.up;
    void Start()
    {
        StartCoroutine(RiseAndFall());
    }


    IEnumerator RiseAndFall()
    {
        while (true)
        {

            transform.Translate(moveDirection * .3f * Time.deltaTime);

            if (transform.position.y > 1f) moveDirection = Vector3.down;
            if (transform.position.y < 0f) moveDirection = Vector3.up;

            yield return null;
        }
    }
}
