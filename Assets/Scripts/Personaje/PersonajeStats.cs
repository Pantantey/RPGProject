using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//es para decirle a unity q este scriptableObject se pueda crear en el men�
[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    [Header("Stats")]
    //en los ScriptableObject si es seguro a�adir variables de tipo publico
    public float Da�o = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequeridaSiguienteNivel;
    [Range(0f, 100f)] public float PorcentajeCr�tico;
    [Range(0f, 100f)] public float PorcentajeBloqueo;

    [Header("Atributos")]
    public int Fuerza;
    public int Inteligencia;
    public int Destreza;

    //ocultarlo en el unity
    [HideInInspector]public int PuntosDisponibles;

    //estos 3 metodos se llaman en personaje para aumentarle los stats
    //que hacer cuando se le da un punto al atributo fuerza
    public void A�adirBonusPorAtributoFuerza()
    {
        Da�o += 2f;
        Defensa += 1f;
        PorcentajeBloqueo += 0.03f;
        PorcentajeBloqueo = (float)(Math.Round(PorcentajeBloqueo, 2));
    }

    //que hacer cuando se le da un punto al atributo inteligencia
    public void A�adirBonusPorAtributoInteligencia()
    {
        Da�o += 3f;

        PorcentajeCr�tico += 0.2f;
        PorcentajeCr�tico = (float)(Math.Round(PorcentajeCr�tico, 2));
    }

    //que hacer cuando se le da un punto al atributo destreza
    public void A�adirBonusPorAtributoDestreza()
    {
        Velocidad += 0.1f;
        //redondear a 2 decimales
        Velocidad = (float)(Math.Round(Velocidad, 2));

        
        PorcentajeBloqueo += 0.05f;
        //redondear a 2 decimales
        PorcentajeBloqueo = (float)(Math.Round(PorcentajeBloqueo, 2));
    }

    public void ResetearValores()
    {
        Da�o = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequeridaSiguienteNivel = 0f;
        PorcentajeBloqueo = 0f;
        PorcentajeCr�tico = 0f;

        Fuerza = 0;
        Inteligencia = 0;
        Destreza = 0;

        PuntosDisponibles =  0;
    }

}


