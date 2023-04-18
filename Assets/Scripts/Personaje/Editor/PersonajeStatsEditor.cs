using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//se le pregunta cual es la clase que va a editar
[CustomEditor(typeof(PersonajeStats))]
public class PersonajeStatsEditor : Editor
{
    //para acceder a la informacion de la clase PersonajesStats hay que obtener el objetivo de este editor
    public PersonajeStats StatsTarget => target as PersonajeStats;

    //para agregar un boton al inspector
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Resetear Valores"))
        {
            StatsTarget.ResetearValores();
        }
    }
}
