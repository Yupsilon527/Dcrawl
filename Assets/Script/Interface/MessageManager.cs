using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    TMPro.TextMeshProUGUI tmpro;
    public float DefaultMessageDuration = 10;
    public static MessageManager main;
    private void Awake()
    {
        main = this;
        if (tmpro == null)
        { tmpro = GetComponentInChildren<TMPro.TextMeshProUGUI>(); }
    }
    public static void ShowMessage(string message)
    {
        if (main != null && main.tmpro != null)
        {
            main.ChangeMessage( message);
        }
    }
    public static void ClearMessage()
    {
        if (main!=null && main.tmpro != null)
        {
            main.tmpro.gameObject.SetActive(false);
        }
    }
    Coroutine messageCoroutine;
    public void ChangeMessage(string text)
    {
        ChangeMessage(text, DefaultMessageDuration);
    }
    public void ChangeMessage(string text, float duration)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(ShowErrorForDuration(text, duration));
    }
    IEnumerator ShowErrorForDuration(string text, float dur)
    {
        tmpro.text = text;
        tmpro.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(dur);
        tmpro.gameObject.SetActive(false);
        messageCoroutine = null;
    }
}
