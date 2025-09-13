using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyWithTime : MonoBehaviour
{
    [SerializeField] float destroyTimer = 2f;
    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}