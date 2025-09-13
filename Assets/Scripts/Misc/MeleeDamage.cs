using UnityEngine;
using System.Collections;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 30f;
    [SerializeField] private float headMultiplier = 1f;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private bool isRocket;

    private bool oneTimeDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (oneTimeDamage) return;
        var zone = other.GetComponent<DamageZone>();
        if (zone == null) return;

        if (isRocket)
            StartCoroutine(RocketDamageTime());

        var target = zone.GetComponentInParent<TargetDamageable>();
        if (target != null)
        {
            target.SetHitPos(transform.position);
            zone.Damage(damageAmount, headMultiplier);
        }

        if (impactEffect != null)
            Instantiate(impactEffect, other.transform.position, other.transform.rotation);
    }

    private IEnumerator RocketDamageTime()
    {
        yield return new WaitForSeconds(0.5f);
        oneTimeDamage = true;
    }
}