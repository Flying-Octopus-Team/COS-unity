using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catTests : MonoBehaviour {
    private CatEvents catEvents;
    private GameObject player;
    private CatActions catActions;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        catActions = GetComponent<CatActions>();

        //StartCoroutine(CatMoveRandomlyTowardsPlayer());
    }

    public IEnumerator CatMoveRandomlyTowardsPlayer() {
        float randomDelay;
        while (true) {
            randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMove(player.transform.position);

        }
    }
    
    public IEnumerator CatMoveRandomlyTowardsRandomPoint() {
        float randomDelay;
        // int randomCatPlace = catActions // choose random cat action with random animation and make it

        while (true) {
            randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMove(player.transform.position);

        }
    }
}
