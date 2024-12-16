using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class contD : MonoBehaviour
{
    public float vida = 100;
    public int vidasRestantes = 3;
    public TextMeshProUGUI textMesh1;
    public VidBarD vidaBarD; 
    public PlayerController jugador;

    private void Start()
    {
        UpdateVidaTexto();
    }

    public void PerderVida(float vidaPerdida)
    {
        vida -= vidaPerdida;
        if (vida <= 0)
        {
            RespawnearJugador();
        }
        UpdateVidaTexto();
    }

    private void RespawnearJugador()
    {
        if (vidasRestantes > 0)
        {
            vidasRestantes--;
            vida = vidaBarD.vidaMaxima;
            vidaBarD.LlenarBarra();
            jugador.Respawn();
            UpdateVidaTexto();
        }
        else
        {
            Debug.Log("El Jugador 2 se quedó sin vidas.");
            //meter fin de juego
            SceneManager.LoadScene("MainMenu");
            
        }
    }

    private void UpdateVidaTexto()
    {
        textMesh1.text = $"{vidasRestantes}";
    }
}

