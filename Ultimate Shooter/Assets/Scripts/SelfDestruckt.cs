using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruckt : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 3f;
    private void Start() 
    {
        Destroy(gameObject, timeToDestroy);
    }
}
