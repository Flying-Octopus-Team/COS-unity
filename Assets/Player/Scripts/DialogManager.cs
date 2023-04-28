using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    [SerializeField] private TextMeshProUGUI dialogOutput;
    [SerializeField] private GameObject noteOutput;

    private Queue<Quote> dialogQueue = new Queue<Quote>();

    private void Start()
    {
        instance = this;
        StartCoroutine(DialogLoop());
    }
    public void AddToQuery(Quote q)
    {
        dialogQueue.Enqueue(q);
    }
    public void ShowNoteQuery(Note n)
    {
        noteOutput.SetActive(true);
        TextMeshProUGUI tmp = noteOutput.GetComponentInChildren<TextMeshProUGUI>();
        if(tmp)
        {
            tmp.SetText(n.noteData);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    IEnumerator DialogLoop()
    {
        while(true)
        {
            if(dialogQueue.TryDequeue(out Quote result))
            {
                Debug.Log($"{result.text}");
                dialogOutput.SetText(result.text);
                if (result.audio)
                {
                    yield return new WaitForSeconds(result.audio.length);
                }
                else
                {
                    yield return new WaitForSeconds(result.text.Length*0.2f);
                }
            }
            else
            {
                dialogOutput.SetText("");
                yield return new WaitWhile(()=> dialogQueue.Count<=0);
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
