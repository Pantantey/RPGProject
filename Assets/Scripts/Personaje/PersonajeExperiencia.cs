using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Config")]




    [SerializeField]private int nivelMax;
    //expBase es la experiencia necesaria para subir al prox nivel
    [SerializeField]private int expBase;
    [SerializeField]private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expRequeridaSiguienteNivel;

    // Start is called before the first frame update
    void Start()
    {
        //se utiliza el personajeStats para guardar el nivel ahi
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
        ActualizarBarraExp();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(10);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida > 0f)
        {
            //experiencia q falta para subir de nivel
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expActual += expObtenida;
                expObtenida -= expRestanteNuevoNivel;
                ActualizarNivel();
                //recursividad, se llama al metodo y se agrega exp luego de que se sube de nivel
                AñadirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                //esta parte de abajo del codigo creo q no es necesaria
                if (expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }
        stats.ExpActual = expActual;
        ActualizarBarraExp();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            expActualTemp = 0f;
            expRequeridaSiguienteNivel *= valorIncremental;
            stats.ExpRequeridaSiguienteNivel = expRequeridaSiguienteNivel;
            stats.PuntosDisponibles += 3;
        }
    }

    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp, expRequeridaSiguienteNivel);
    }

}
