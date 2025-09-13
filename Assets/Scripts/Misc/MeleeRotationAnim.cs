using UnityEngine;

public class MeleeRotationAnim : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = -90f; // degrees per second
    [SerializeField] private Vector3 rotationAxis = Vector3.right;

    private void Update()
    {
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime, Space.Self);
    }
}