using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShowUI : MonoBehaviour
{
    public GameObject showMessage;
    void Start()
    {
        showMessage.SetActive(false);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.tag == "Player")
        {
            showMessage.SetActive(true);
            StartCoroutine(ShowMessageWithDelay());
        }
    }

    void OnTriggerExit()
    {
        HideMessage();
    }

    public void HideMessage()
    {
        showMessage.SetActive(false);
    }
    
    IEnumerator ShowMessageWithDelay()
    {
        yield return new WaitForSeconds(2);
    }
}
