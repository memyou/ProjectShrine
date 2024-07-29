using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEndTrigger : MonoBehaviour
{
    public GameObject[] ihen;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < ihen.Length; i++)
            {
                ihen[i].SetActive(false);
            }
        }
    }

    void OnDestroy() { ihen = null; }
}
