using UnityEngine;

public class DestroyWithTime : MonoBehaviour
{
    [SerializeField] private float destroyTimer = 2f;
    private void Start()
    {
        Destroy(gameObject, destroyTimer);
    }
}