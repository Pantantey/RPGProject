using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Personaje personaje;
    [SerializeField]private Transform puntoReaparicion;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //se llama a personaje que tiene personaje vida el cual tiene derrotado
            if (personaje.PersonajeVida.Derrotado)
            {
                personaje.transform.localPosition = puntoReaparicion.position;
                personaje.RestaurarPersonaje();
            }
        }
    }
}
