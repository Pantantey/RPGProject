using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hacer enumeracion para saber cual boton estamos clickeando
public enum TipoAtributo
{
    Fuerza,
    Inteligencia,
    Destreza
}
public class AtrubutoBoton : MonoBehaviour
{
    //para lanzar el evento y saber cual mejora de atributo clickeamos
    public static Action<TipoAtributo> EventoAgregarAtributo;

    [SerializeField] private TipoAtributo tipo;

    public void AgregarAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }


}