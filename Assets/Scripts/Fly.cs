using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private bool isFlying = false;
    private Opener opener;
    private Vector3 targetPosition;

    public void FlyTo(Opener target)
    {
        if (target != null)
        {
            // Debug.Log("LETS FLY!");
            gameObject.tag = "Untagged";
            gameObject.transform.SetParent(null);

            opener = target;
            targetPosition = target.transform.position;
            isFlying = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isFlying)
        {
            var moveVector = Vector3.MoveTowards(transform.position, targetPosition, .3f);
            transform.position = moveVector;

            if  (Vector3.Distance(transform.position,targetPosition) < .5f)
            {
                opener.cost--;
                Destroy(gameObject);
            }
        }
        
    }


}
