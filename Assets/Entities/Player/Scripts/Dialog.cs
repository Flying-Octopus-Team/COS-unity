using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuoteData", menuName = "ScriptableObjects/Dialog")]
public class Dialog : ScriptableObject
{
    public List<Quote> dialogueQuotes;

    public void LoadDialogIntoManager()
    {
        DialogManager dm = DialogManager.instance;
        if (!dm) return;
        for(int i=0;i<dialogueQuotes.Count;i++)
        {
            dm.AddToQuery(dialogueQuotes[i]);
        }
    }
}
