using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : MonoBehaviour
{
    [SerializeField] private PersonajeStats stats;

    //hacerle un set y get a PersonajeVida y AnimacionesPersonaje para poder utilizar sus metodos
    public PersonajeVida PersonajeVida { get; private set; }
    public AnimacionesPersonaje AnimacionesPersonaje { get; private set; }
    public PersonajeMana PersonajeMana { get; private set; }
    public PersonajeExperiencia PersonajeExperiencia { get; private set; }

    private void Awake()
    {
        PersonajeMana = GetComponent<PersonajeMana>();
        PersonajeVida = GetComponent<PersonajeVida>();
        AnimacionesPersonaje = GetComponent<AnimacionesPersonaje>();
        PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
    }

    public void RestaurarPersonaje()
    {
        PersonajeMana.RestablecerMana();
        PersonajeVida.RestaurarPersonaje();
        AnimacionesPersonaje.RevivirPersonaje();
    }


    //Para escuchar eventos se usan los siguientes 2 metodos ademas del atributo respuesta
    private void AtributoRespuesta(TipoAtributo tipo)
    {
        if (stats.PuntosDisponibles <= 0)
        {
            //si se usa return no devuelve la lo que continua abajo (el switch)
            return;
        }

        //dependiendo de cual tipo de atributo se llama se aumentan los atributos que mejoran
        switch (tipo)
        {
            case TipoAtributo.Fuerza:
                //aumentar el numero del panel de stats
                stats.Fuerza++;

                stats.AñadirBonusPorAtributoFuerza();
                break;
            case TipoAtributo.Inteligencia:
                stats.Inteligencia++;

                stats.AñadirBonusPorAtributoInteligencia();
                break;
            case TipoAtributo.Destreza:
                stats.Destreza++;

                stats.AñadirBonusPorAtributoDestreza();
                break;
        }
        //restar los puntos luego de que se ha aumentado algun stat
        stats.PuntosDisponibles--;
    }

    private void OnEnable()
    {
        AtrubutoBoton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtrubutoBoton.EventoAgregarAtributo -= AtributoRespuesta;
    }


}
