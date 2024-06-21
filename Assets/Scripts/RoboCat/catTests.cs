using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catTests : MonoBehaviour {
    private CatEvents catEvents;
    private GameObject player;
    private CatActions catActions;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        catEvents = CatEvents.current;
        catActions = GetComponent<CatActions>();

        CatMoveRandomlyTowardsRandomPoint();
    }

    public IEnumerator CatMoveRandomlyTowardsPlayer() {
        float randomDelay;
        while (true) {
            randomDelay = Random.Range(0f, 3f);
            yield return new WaitForSeconds(randomDelay);

            catEvents.CatMove(player.transform.position);

        }
    }

    public void CatMoveRandomlyTowardsRandomPoint() {
        int randomCatPlace = Random.Range(0, catActions.catPlaces.Count);

        catEvents.CatMove(catActions.catPlaces[randomCatPlace].destination.transform.position);
        Debug.Log(catActions.catPlaces[randomCatPlace].destination.transform.position);
        Debug.Log("Cat: " + transform.position);
        //catActions.catPlaces[randomCatPlace].animation.Play();
    }
}
