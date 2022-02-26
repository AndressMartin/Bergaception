using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cena : MonoBehaviour
{
    [SerializeField] private Light[] luzes;

    private void Awake()
    {
        luzes = FindObjectsOfType<Light>();
    }

    public void LigarLuzes()
    {
        foreach (Light luz in luzes)
        {
            luz.enabled = true;
        }
    }

    public void DesligarLuzes()
    {
        foreach(Light luz in luzes)
        {
            luz.enabled = false;
        }
    }
}
