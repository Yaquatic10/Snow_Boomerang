using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VidBar : MonoBehaviour
{
    public Slider vida;
    public float vidaMaxima = 100;
    private float _vidaActual;
    public cont contScript; 

    private void Start()
    {
        vida.maxValue = vidaMaxima;
        _vidaActual = vidaMaxima;
        vida.value = _vidaActual;
    }

    public void PerdidaV(float dano)
    {
        _vidaActual -= dano;
        _vidaActual = Mathf.Clamp(_vidaActual, 0, vidaMaxima);
        vida.value = _vidaActual;

        if (_vidaActual <= 0)
        {
            contScript.PerderVida(vidaMaxima);
        }
    }

    public void LlenarBarra()
    {
        _vidaActual = vidaMaxima;
        vida.value = _vidaActual;
    }
}