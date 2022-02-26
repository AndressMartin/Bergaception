using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letreiro : MonoBehaviour
{
    [SerializeField] private int velocidadeDoTexto;

    public IEnumerator EscreverTexto(TextMeshProUGUI texto, string textoParaEscrever, DialogManager dialogManager)
    {
        float tempo = dialogManager.LetrasVisiveis;
        int charIndex = dialogManager.LetrasVisiveis;

        do
        {
            tempo += Time.deltaTime * velocidadeDoTexto;

            charIndex = Mathf.FloorToInt(tempo);
            charIndex = Mathf.Clamp(charIndex, 0, textoParaEscrever.Length);

            texto.maxVisibleCharacters = charIndex;

            yield return null;

        } while (charIndex < textoParaEscrever.Length);

        texto.maxVisibleCharacters = texto.text.Length;

        dialogManager.SetLetrasVisiveis(charIndex);
    }
}
