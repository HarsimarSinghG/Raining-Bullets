using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Transform objectSpawnPoints;
    private void OnTriggerStay(Collider other)
    {
        Player player = other.gameObject.GetComponentInParent<Player>();
        if (player) { Player.Instance.SetCurrentFloor(this.gameObject); }
    }

    public Transform ReturnObjectSpawnPoints() { return objectSpawnPoints; }
}
