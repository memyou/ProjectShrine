using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageExitArea : MonoBehaviour
{
    bool isEnter;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
        }
    }

    public bool GetIsEnter() { return isEnter; }
}
