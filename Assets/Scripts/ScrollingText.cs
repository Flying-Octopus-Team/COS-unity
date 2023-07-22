using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour {
    [SerializeField] private float scrollSpeed = 50f;
    private Vector3 startTextPosition;

    private void Start() {
        startTextPosition = transform.position;
    }

    private void Update() {
        transform.Translate(Camera.main.transform.up * scrollSpeed * Time.deltaTime);
        if (transform.localPosition.y > 600) {
            transform.position = startTextPosition;
        }
    }
}
