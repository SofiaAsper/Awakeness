using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeWithTime : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] float ExplosionTime = 3f;
    [SerializeField] Transform explosionPosition;
    public void ExplodeStart()
    {
        Invoke("ExplodeEnd", ExplosionTime);
    }
    void ExplodeEnd()
    {
        GameObject explosion= Instantiate(explosionEffect, explosionPosition.position, explosionPosition.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
    }
}