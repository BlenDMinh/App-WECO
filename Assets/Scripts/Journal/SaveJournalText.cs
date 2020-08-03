using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveJournalText : MonoBehaviour
{
    // Everything relate to save journal
    // This script is approached to JournalField to get input, and SaveJournal to save input
    // JournalField object (Unity) = journalInput (code)
    public Text journalInput;
    public void ButtonClick_SaveJournal()
    {
        UserData data = new UserData();
        data.SaveUserJournal(journalInput.text);
    }
}
