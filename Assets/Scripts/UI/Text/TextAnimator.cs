using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    TMP_Text text;
    public bool activateAnimation;
    [Range(1.0f, 20.0f)]
    public float animValue;
    [Range(1.0f, 20.0f)]
    public float speed;
    public TextAnimation textAnimation;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activateAnimation) return;

        if(textAnimation == TextAnimation.Wobbly)
        {
            WobblyAnimation();
        }
    }

    public void WobblyAnimation()
    {
        text.ForceMeshUpdate();
        var textInfo = text.textInfo;

        for (int i = 0; i < textInfo.characterCount; ++i)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * speed + orig.x * 0.01f) * animValue, 0);
            }
        }

        for (int l = 0; l < textInfo.meshInfo.Length; ++l)
        {
            var meshInfo = textInfo.meshInfo[l];
            meshInfo.mesh.vertices = meshInfo.vertices;
            text.UpdateGeometry(meshInfo.mesh, l);
        }
    }

    public enum TextAnimation
    {
        Wobbly
    }
}
