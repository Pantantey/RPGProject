using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camara;

    //cuando un objeto entra al confiner
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //si el jugador es el que está colisionando / entrando al confiner se activa ala camara
        //ponerle el tag al jugador que diga player
        if (collision.CompareTag("Player"))
        {
            camara.gameObject.SetActive(true);
        }
    }

    //cuando un objeto sale del confiner
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camara.gameObject.SetActive(false);
        }
    }
}
