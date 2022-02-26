using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private DialogSO dialog;
    [SerializeField]
    private TextMeshProUGUI text;
    private Letreiro letreiro;
    private int part = -1;
    private float maxTime = 0f;
    [SerializeField]
    private float time;
    [SerializeField]
    private bool waitingForEvent = false;
    private bool playing = false;

    private int letrasVisiveis;

    public int LetrasVisiveis => letrasVisiveis;

    public void SetLetrasVisiveis(int letrasVisiveis)
    {
        this.letrasVisiveis = letrasVisiveis;
    }

    private void Awake()
    {
        time = maxTime;
        dialog.WarnEventWasSuccessful();
        letreiro = GetComponent<Letreiro>();
    }
    public void PlayDialog()
    {
        if (!playing)
        {
            playing = true;
            StartCoroutine(PlayDialogAsync());
        }
        
    }

    private IEnumerator PlayDialogAsync()
    {
        letrasVisiveis = 0;

        while(part +1 < dialog._textList.Count)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                if (part != -1)
                {
                    if (dialog._textList[part].eventToWaitFor != null)
                    {
                        if(dialog._textList[part].fireAtEnd != null)
                        {
                            dialog._textList[part].fireAtEnd.Raise();
                            Debug.Log("Fired a dialog end event!" + dialog._textList[part].fireAtEnd);
                        }
                        else
                        {
                            Debug.Log("No event found.");
                        }
                        waitingForEvent = true;
                    }
                }
                while (waitingForEvent) yield return null;
                part++;
                if (dialog._textList[part].type == TextType.New)
                {
                    letrasVisiveis = 0;
                    text.text = "";
                    text.text = dialog._textList[part].text;
                }
                else if (dialog._textList[part].type == TextType.Continuous)
                {
                    text.text += dialog._textList[part].text;
                }
                if (dialog._textList[part].text.Contains("[w]"))
                {
                    text.GetComponent<TextAnimator>().ActivateSymbols();
                    text.GetComponent<TextAnimator>().activateAnimation = true;
                }
                else text.GetComponent<TextAnimator>().activateAnimation = false;

                yield return letreiro.EscreverTexto(text, dialog._textList[part].text, this);

                maxTime = dialog._textList[part].timeToWait;
                time = maxTime;
            }
            yield return null;
        }
        while(part +1 >= dialog._textList.Count)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    public void DialogEventRaised()
    {
        waitingForEvent = false;
    }
}
