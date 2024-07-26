using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NearDoorCat : MonoBehaviour {
    private NavMeshAgent agent;
    private DoorController dc;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("DoorNavMesh")) {
            dc = null;
            StartCoroutine(WaitForDoor());
            dc = other.GetComponentInParent<DoorController>();
            if (dc != null && !dc.GetDoorState()) {
                dc.SwitchState(); 
            }
        }
    }

    private IEnumerator WaitForDoor() {
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        yield return new WaitForSeconds(4);
        dc.SwitchState();
    }
}
