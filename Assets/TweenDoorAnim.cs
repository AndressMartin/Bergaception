using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenDoorAnim : MonoBehaviour
{
    public void DoDoorOpen()
    {
        transform.DOLocalRotate(new Vector3(0, 95, 0), 3);
    }
}
