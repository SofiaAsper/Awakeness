using UnityEngine;

public class ExplodeWithTime : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float explosionDelay = 3f;
    [SerializeField] private Transform explosionPosition;

    public void ExplodeStart()
    {
        CancelInvoke(nameof(ExplodeEnd));
        Invoke(nameof(ExplodeEnd), explosionDelay);
    }

    private void ExplodeEnd()
    {
        if (explosionEffect == null || explosionPosition == null) return;
        var explosion = Instantiate(explosionEffect, explosionPosition.position, explosionPosition.rotation);
        var ps = explosion.GetComponent<ParticleSystem>();
        if (ps) ps.Play();
    }
}