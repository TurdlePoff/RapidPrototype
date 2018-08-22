//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class NotesManager : MonoBehaviour
//{
//    #region Singleton

//    public static NotesManager instance;
    
//    public GameObject notesUI;
//    public TextMeshProUGUI textUI;

//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Debug.LogWarning("More than one instance of notes manager found!");
//        }
//        instance = this;
//    }

//    #endregion
    
//    void UseNote(int noteNumber)
//    {
//        notesUI.SetActive(true);
//        textUI.text = description;
//    }

//    void CloseNote()
//    {
//        notesUI.SetActive(false);
//        textUI.text = "";
//    }

//}
