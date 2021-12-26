using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailureUI : MonoBehaviour
{
    public GameObject FailurePanel;
    public Text Message;

    public void ShowMessage(string message) 
    {
        FailurePanel.SetActive(true);
        Message.text = message;
    }

    public void Close()
    { 
        FailurePanel.SetActive(false);
    }
}
