using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perdidaD : MonoBehaviour
{
    public float player = 30;
    public float bull = 50;
    public float tramp = 60;

    private VidBarD sliVidaInstance;

    void Start()
    {

        sliVidaInstance = FindObjectOfType<VidBarD>();

        if (sliVidaInstance == null)
        {
            Debug.LogError("No se encontró la instancia .");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (sliVidaInstance == null) return;

        if (collision.gameObject.tag == "Player")
        {
            sliVidaInstance.PerdidaV(player);

        }
        else if (collision.gameObject.tag == "Trampas") //
        {
            sliVidaInstance.PerdidaV(tramp);

        }
        else if (collision.gameObject.tag == "Boomerang")
        {
            sliVidaInstance.PerdidaV(bull);
            Destroy(collision.gameObject);
        }
    }
}