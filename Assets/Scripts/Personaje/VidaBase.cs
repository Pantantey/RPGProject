using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    //se usa protected en lugar de private para utilizar las variables en clases hijas como PersonajeVida 
    [SerializeField]protected float saludInicial;
    [SerializeField]protected float saludMax;

    //esta es una propiedad que puede ser enviada y regresada, sirve para modificar los private
    public float Salud { get; protected set; }

    // Se le llama protected virtual para poder llamar este metodo desde otra clase (Ejemplo: personajeVida)
    protected virtual void Start()
    {
        Salud = saludInicial;
    }

    //para recibir daño la cantidad tiene q ser mayor a 0 y debemos tener vida
    public void RecibirDaño(float cantidad)
    {
        if(cantidad <= 0f)
        {
            return;
        }
        if (Salud > 0f)
        {
            Salud -= cantidad;
            ActualizarBarraVida(Salud, saludMax);
            if (Salud <= 0f)
            {
                ActualizarBarraVida(Salud, saludMax);
                PersonajeDerrotado();
            }
        }
    }

    //Virtal se utiliza para poder sobreescribir
    protected virtual void ActualizarBarraVida(float vidaActual, float vidaMax)
    {

    }

    protected virtual void PersonajeDerrotado()
    {

    }


}
