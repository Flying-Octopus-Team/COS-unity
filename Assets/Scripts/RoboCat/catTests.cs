using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catTests : MonoBehaviour {
    private CatEvents catEvents;
    private GameObject player;
    private Cat cat;

    private void Awake() {
        catEvents = CatEvents.current;
        cat = GetComponent<Cat>();
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //StartCoroutine(CatMoveRandomlyTowardsPlayer());
        //StartCoroutine(CatRandomlyMeows());
    }

    private IEnumerator CatMoveRandomlyTowardsPlayer() {
        float randomDelay;
        while (true) {
            randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMove(player.transform.position);

        }
    }


    private IEnumerator CatRandomlyMeows() {
        float randomDelay;
        int randomSoundIndex;

        while (true) {
            randomDelay = Random.Range(0f, 3f);
            randomSoundIndex = Random.Range(0, cat.meowSounds.Count);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMakeSound(cat.meowSounds[randomSoundIndex]);
        }
    }
}
