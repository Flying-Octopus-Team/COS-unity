using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {
    public List<AudioClip> meowSounds = new List<AudioClip>();
    public List<POI> objectsToInteractWith = new List<POI>();
    [SerializeField] private GameObject player;
    [SerializeField] private string POIName;
    private NavMeshAgent agent;
    private CatEvents catEvents;
    private AudioSource audioSource;
    private bool lookForInteraction;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
    }

    private void Start() {
        lookForInteraction = true;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        catEvents.onCatMove += AgentSetDestination;
        catEvents.onCatMakeSound += CatMakeSound;
    }

    public void ToggleLookingForObjects() {
        lookForInteraction = !lookForInteraction;
    } 

    private void Update() {
        if (lookForInteraction) {
            Test();
        }
    }

    public void Test() {
        Collider[] placesToInteract = Physics.OverlapSphere(transform.position, 6);
        if (placesToInteract.Length > 0) {
            foreach (Collider places in placesToInteract) {
                if (places.TryGetComponent<IPOI>(out IPOI poi)) {
                    objectsToInteractWith.Add(places.GetComponent<POI>());
                }
            }
        }

        int randomAction = Random.Range(0, objectsToInteractWith.Count);
        POIName = objectsToInteractWith[randomAction].POIName;

        objectsToInteractWith[randomAction].InEvent();
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
