using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatActions : MonoBehaviour {
    public List<AudioClip> meowSounds = new List<AudioClip>();
    public List<CatPlaces> catPlaces = new List<CatPlaces>();
    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    private CatEvents catEvents;
    private AudioSource audioSource;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
    }

    private void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        catEvents.onCatMove += AgentSetDestination;
        catEvents.onCatMakeSound += CatMakeSound;
    }

    public void AgentSetDestination(Vector3 destination) {
        if (agent == null) return;

        agent.SetDestination(destination);
        if (agent.remainingDistance <= 1.5f && agent.hasPath) {
            agent.isStopped = true;
            return;
                
        }
        agent.isStopped = false;
    }

    public void CatMakeSound(AudioClip catSound) {
        if (catSound == null) return;

        audioSource.clip = catSound;
        audioSource.Play();
    }

    
}
