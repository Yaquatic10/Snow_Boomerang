using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perdida : MonoBehaviour
{
    public float player = 30;
    public float bull = 50;
    public float tramp = 60;

    private VidBar sliVidaInstance;

    void Start()
    {
        // Busca la instancia de SliVida una sola vez en Start
        sliVidaInstance = FindObjectOfType<VidBar>();

        if (sliVidaInstance == null)
        {
            Debug.LogError("No se encontró la instancia .");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si se encontró el slider previamente
        if (sliVidaInstance == null) return;

        if (collision.gameObject.tag == "Player")
        {
            sliVidaInstance.PerdidaV(player);

        }
        else if (collision.gameObject.tag == "Trampas") //
        {
            sliVidaInstance.PerdidaV(tramp);

        }
        else if (collision.gameObject.tag == "Boomerang 2")
        {
            sliVidaInstance.PerdidaV(bull);
            Destroy(collision.gameObject);
        }
    }
}