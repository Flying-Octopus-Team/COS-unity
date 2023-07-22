using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SzkiceChangingImage : MonoBehaviour {
    [SerializeField] private List<Sprite> szkiceImages = new List<Sprite>();
    private Image image;
    int i = 0;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Start() { 
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage() {
        while (true) {
            if (i >= szkiceImages.Count) {
                i = 0;
            }

            yield return new WaitForSeconds(4);
            image.sprite = szkiceImages[i];
            i++;
        }
    }
}
