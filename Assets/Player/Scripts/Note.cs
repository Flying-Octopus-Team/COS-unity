using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "ScriptableObjects/Note")]
public class Note : ScriptableObject
{
    [Multiline]
    public string noteData;

    public void pushNote()
    {
        DialogManager dm = DialogManager.instance;
        if (dm)
        {
            dm.ShowNoteQuery(this);
        }
    }
}
