using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBackHelper : MonoBehaviour
{
    GameObject playerGoback;
    Vector3 position = new Vector3(10f ,0f ,10f);
    Vector3 playerPosition;

    private void Start() {
        playerGoback = GameObject.FindGameObjectWithTag("Player");
        playerPosition = playerGoback.transform.position;

    }

    // public Vector3 teleportTo = playerPosition;
    private void OnTriggerEnter(Collider other)
    {
        InterpolatedTransform movable = null;
        if ((movable = other.GetComponent<InterpolatedTransform>()) == null) return;
        if (movable as PlayerMovement)
            movable.ResetPositionTo(playerPosition-position);
    }

}
