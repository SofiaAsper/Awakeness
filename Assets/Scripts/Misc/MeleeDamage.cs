using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public float damageAmount = 30;
    float headMulti = 1;
    [SerializeField] GameObject ImpactEffect;
    bool OneTimeDamage = false;
    [SerializeField] bool IsRocket;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DamageZone>()!=null)
        {
            if (OneTimeDamage==true)
            {
                return;
            }
            if (IsRocket)
            {
                StartCoroutine(RocketDamageTime());
            }
           
            other.GetComponent<DamageZone>().GetComponentInParent<TargetDamageable>().SetHitPos(this.transform.position);
            other.GetComponent<DamageZone>().Damage(damageAmount, headMulti);
            if (ImpactEffect != null)
            {
                Instantiate(ImpactEffect, other.transform.position, other.transform.rotation);
            }
        }
    }
    IEnumerator RocketDamageTime()
    {
        yield return new WaitForSeconds(0.5f);
        OneTimeDamage = true;
    }
}