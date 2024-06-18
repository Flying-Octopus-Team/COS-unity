using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatActions : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private List<CatPlaces> catPlaces = new List<CatPlaces>();
    private NavMeshAgent agent;
    private CatEvents catEvents;
    private AudioSource audioSource;
    private NavMeshPath path;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
    }

    private void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        catEvents.onCatMove += AgentSetDestination;
        catEvents.onCatMakeSound += CatMakeSound;
        path = new NavMeshPath();
    }

    private void Update() {
        catEvents.CatMove(player.transform.position);
    }

    public void AgentSetDestination(Vector3 destination) {
        Debug.Log(agent.hasPath);
        Debug.Log(agent.remainingDistance);

        if (agent.remainingDistance < 2f && agent.hasPath) {
            agent.isStopped = true;
            return;
                
        }
        //agent.isStopped = false;
        agent.CalculatePath(destination, path);
        agent.SetPath(path);
    }

    public void CatMakeSound(AudioClip catSound) {
        audioSource.clip = catSound;
        audioSource.Play();
    }

    
}
