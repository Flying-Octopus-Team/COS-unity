using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catTests : MonoBehaviour {
    private CatEvents catEvents;
    private GameObject player;
    private CatActions catActions;

    private void Awake() {
        catEvents = CatEvents.current;
        catActions = GetComponent<CatActions>();
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        
        //StartCoroutine(CatMoveRandomlyTowardsPlayer());
        //StartCoroutine(CatRandomlyMeows());
        //CatMoveRandomlyTowardsRandomPoint();
    }

    private IEnumerator CatMoveRandomlyTowardsPlayer() {
        float randomDelay;
        while (true) {
            randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMove(player.transform.position);

        }
    }

    private void CatMoveRandomlyTowardsRandomPoint() {
        int randomCatPlace = Random.Range(0, catActions.catPlaces.Count);
        Vector3 placeToMove = catActions.catPlaces[randomCatPlace].GetDestination().transform.position;

        catEvents.CatMove(placeToMove);
        //catActions.catPlaces[randomCatPlace].animation.Play();
    }

    private IEnumerator CatRandomlyMeows() {
        float randomDelay;
        int randomSoundIndex;

        while (true) {
            randomDelay = Random.Range(0f, 3f);
            randomSoundIndex = Random.Range(0, catActions.meowSounds.Count);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMakeSound(catActions.meowSounds[randomSoundIndex]);
        }
    }
}
