using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandParticle : MonoBehaviour
{
    [SerializeField] GameObject handParticle;

    void Start()
    {
        handParticle.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        handParticle.SetActive(true);
    }

    void OnDestroy() { handParticle = null; }
}
