using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Opener : MonoBehaviour
{

    public int cost;
    public GameObject targetObject;
    public TextMeshPro textCost;

    private void Update()
    {
        if (cost >= 0) textCost.SetText(cost.ToString());

        if (cost <= 0)
        {
            Open();
        }
    }

    private void Open()
    {
        Destroy(textCost);
        Destroy(gameObject);
        targetObject.SetActive(!targetObject.activeInHierarchy);
    }

}
