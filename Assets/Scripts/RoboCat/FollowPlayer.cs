using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private List<CatPlaces> catPlaces = new List<CatPlaces>();
    private NavMeshAgent agent;
    private CatEvents catEvents;


    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
    }

    private void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        catEvents.onCatMove += AgentSetDestination;
    }

    public void AgentSetDestination(Vector3 destination) {
        agent.SetDestination(destination);
    }
}
