using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {
    [SerializeField] private List<AudioClip> meowSounds = new List<AudioClip>();
    private POI player;
    private List<POI> objectsToInteractWith = new List<POI>();
    private NavMeshAgent agent;
    private AudioSource audioSource;
    private Animator animator;
    private POI currentPOI;


    private IEnumerator Start() {
        do { 
            GetAllComponents();
            yield return null;
        } while (player == null);
        StartCoroutine(RandomlyMakeSound());
        GetAllPOI();
        GetClosestPOI();
    }

    private void FixedUpdate() {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) > 40) {
            transform.position = player.transform.position + Vector3.one;
        }
    }

    private void GetAllComponents() {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("PlayerCharacter").GetComponent<POI>();
    }

    private void GetAllPOI() {
        // Add every POI to the list but skip player
        foreach (POI poi in FindObjectsOfType<POI>()) {
            if (poi.POIName == "Player") continue;
            objectsToInteractWith.Add(poi);
        }
    }

    private void AnimationIn() {
        animator.SetBool("HasAnimation", true);

        // Check if the current POI is null
        if (currentPOI == null) {
            GetClosestPOI();
            return;
        }

        // Play animation on the current POI
        animator.Play(currentPOI.inAnimation);
    }

    // This is called when it is triggeerd by event on animation
    public void AnimationOut() {
        // Play the out animation
        animator.Play(currentPOI.outAnimation);

        if (currentPOI.POIName != "Player") {
            // Remove the POI that the cat interacted with already from the list of POIs
            objectsToInteractWith.Remove(currentPOI);

            // Destroy the POI that the cat interacted with already
            Destroy(currentPOI, 0.1f);

            // Make the agent possible to move
            agent.isStopped = false;
        }

        animator.SetBool("HasAnimation", false);
        GetClosestPOI();
    }

    private void GetClosestPOI() {
        if (!HavePOIAvailable()) {
            currentPOI = player;
            StartCoroutine(CatSetDestination());
            return;
        }

        // Set smallest distance to biggest possible
        float smallestDistance = 3;
        float distance = 0;

        // Iterate through very POI in the list
        foreach (POI poi in objectsToInteractWith) {

            // Calculate the distance from cat to the poi
            distance = Vector3.Distance(transform.position, poi.transform.position);

            // Check if the distance is smaller than another poi on the map
            if(distance < smallestDistance) {
                smallestDistance = distance;
                currentPOI = poi;
            }
        }

        // check if the smallest distance is more than 3
        if (smallestDistance > 3) {

            // Set the current POI to be player
            currentPOI = player; 
        }

        GoToCurrentPOI();
    }

    private void GoToCurrentPOI() {
        StartCoroutine(CatSetDestination());
    }

    private bool HavePOIAvailable() {
        // Return true is there is more than 0 POI on the list
        return objectsToInteractWith.Count != 0;
    }

    private IEnumerator CatSetDestination() {
        if (agent == null) yield return null;

        // Set the destination of the cat to the current POI
        agent.SetDestination(currentPOI.transform.position); 
        yield return new WaitForSeconds(1);

        // Until the remaining distance is bigger than 1f return
        while (agent.remainingDistance > 1f) {

            // Keep setting the destination in case the object moved
            agent.SetDestination(currentPOI.transform.position);
            yield return null;
        }

        if (agent.remainingDistance <= 1f) { 

            // Stop the cat
            agent.isStopped = true;

            // Reset the cat path
            agent.ResetPath();
            AnimationIn();
        }
    }

    private IEnumerator RandomlyMakeSound() {
        float randomDelay = 0;

        while (true) {
            // Wait for random amount of time
            randomDelay = Random.Range(1f, 10f);
            yield return new WaitForSeconds(randomDelay);

            CatMakeSound();
        }
    }

    private void CatMakeSound() {
        // Return if there is no sounds available
        if (meowSounds.Count == 0) return;

        // Choose random mewo sound
        int randomSound = Random.Range(0, meowSounds.Count);

        // Play the meow sound
        audioSource.clip = meowSounds[randomSound];
        audioSource.Play();
    }

    
}
