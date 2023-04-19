using UnityEngine;

public class MeleeRotationAnim : MonoBehaviour
{
    [SerializeField] float rotationSpeed = -10f;
    private void FixedUpdate()
    {
        transform.Rotate(rotationSpeed, 0.0f, 0.0f, Space.Self);
    }
}