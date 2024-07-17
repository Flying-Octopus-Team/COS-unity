using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {
    public List<AudioClip> meowSounds = new List<AudioClip>();
    public List<POI> objectsToInteractWith = new List<POI>();
    //public POI[] objectsToInteractWith;
    public POI player;
    [SerializeField] private string POIName;
    private NavMeshAgent agent;
    private CatEvents catEvents;
    private AudioSource audioSource;
    [SerializeField] private bool lookForInteraction;
    private Animator animator;
    public POI currentPOI;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        catEvents = CatEvents.current;
        animator = GetComponent<Animator>();
    }

    private void Start() {
        lookForInteraction = true;

        catEvents.onCatMakeSound += CatMakeSound;

        GetAllPOI();
        GetClosestPOI();
    }

    private void GetAllPOI() {
        objectsToInteractWith = FindObjectsOfType<POI>().ToList();
    }

    public void AnimationOut() {
        animator.Play(currentPOI.outAnimation);
        objectsToInteractWith.Remove(currentPOI);
        Destroy(currentPOI);
        agent.isStopped = false;
        GetClosestPOI();
    }

    private void AnimationIn() {
        animator.Play(currentPOI.inAnimation);
    }

    private void GetClosestPOI() {
        float smallestDistance = 20;

        foreach (POI poi in objectsToInteractWith) {
            float distance = Vector3.Distance(transform.position, poi.transform.position);
            if(distance < smallestDistance) {
                smallestDistance = distance;
                currentPOI = poi;
            }
        }

        if (smallestDistance > 20) {
            currentPOI = player; 
            animator.SetBool("HasAnimation", false);
        }

        GoToCurrentPOI();

    }

    private void GoToCurrentPOI() {
        Debug.Log(currentPOI);
        StartCoroutine(AgentSetDestination(currentPOI.transform.position));
        //catEvents.CatMove(currentPOI.transform.position);
    }

    public IEnumerator AgentSetDestination(Vector3 destination) {
        if (agent == null) yield return null;

        agent.SetDestination(destination);
        yield return new WaitForSeconds(1);
        while (agent.remainingDistance > 0.5f) {
            yield return null;
        }

        if (agent.remainingDistance <= 0.5f) { 
            agent.isStopped = true;
            agent.ResetPath();
            AnimationIn();
        }
        //if (agent.remainingDistance <= 1.5f && agent.hasPath) {
        //    agent.isStopped = true;
        //    return;
        //        
        //}
        //agent.isStopped = false;
    }

    private IEnumerator AgentStop() {
        yield return new WaitWhile(() => agent.remainingDistance > 0.5f);
        agent.isStopped = true;
        agent.ResetPath();
        AnimationIn();
    }

    public void CatMakeSound(AudioClip catSound) {
        if (catSound == null) return;

        audioSource.clip = catSound;
        audioSource.Play();
    }

    
}
