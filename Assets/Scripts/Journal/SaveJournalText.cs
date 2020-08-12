using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveJournalText : MonoBehaviour
{
    // Everything relate to save journal
    // This script is approached to JournalField to get input, and SaveJournal to save input
    // JournalField object (Unity) = journalInput (code)
    private UserData data = new UserData();
    public void ButtonClick_SaveJournal()
    {

    }
    public void LoadJournal()
    {
        data = UserData.LoadUserData();
        this.GetComponent()

    }
}
