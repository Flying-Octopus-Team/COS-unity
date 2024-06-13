using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {
    private NavMeshAgent agent;
    public GameObject player;
    private CatEvents catEvents;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
    }

    private void Start() {
        catEvents.onCatMove += AgentSetDestination;
    }

    private void Update() {
        catEvents.CatMove();
    }

    public void AgentSetDestination() {
        agent.SetDestination(player.transform.position);
    }
}
