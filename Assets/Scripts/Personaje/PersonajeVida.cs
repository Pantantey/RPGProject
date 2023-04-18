using System;
//unityEngine se usa para el input.GetKeyDown
using UnityEngine;
//Así se hereda para poder utilizar todo lo de VidaBase
public class PersonajeVida : VidaBase
{
    private PersonajeMana _personajeMana;

    //para crear un evento se usa el Action de la clase System
    public static Action EventoPersonajeDerrotado;
    public bool PuedeSerCurado => Salud < saludMax;

    //para saber si el personaje ya fue derrotado
    public bool Derrotado { get; private set; }

    //Se crea la referencia de boxcollider2D
    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        //se inicializa
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _personajeMana = GetComponent<PersonajeMana>();
    }

    protected override void Start()
    {
        //esto se deja xq llama lo que tiene la clase base (vidaBase)
        base.Start();
        ActualizarBarraVida(Salud, saludMax);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarSalud(10);
        }
    }




    public void RestaurarSalud(float cantidad)
    {
        //si el personaje está muerto no se cura
        if (Derrotado)
        {
            return;
        }

        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if(Salud > saludMax){
                Salud = saludMax;
            }
            ActualizarBarraVida(Salud, saludMax);
        }
    }



    //se escribe override tab y se pueden utilizar los metodos
    protected override void PersonajeDerrotado()
    {
        //se desactiva el collider del personaje para q no interfiera con otras cosas
        _boxCollider2D.enabled = false;
        //decimos que el personaje ha sido derrotado
        Derrotado = true;

        //se le quita el mana
        _personajeMana.QuitarMana();

        //se pregunta si EventoPersonajeDerrotado es null y si no es así se invoca
        EventoPersonajeDerrotado?.Invoke();
    }

    //para revivir el personaje luego de que muere
    public void RestaurarPersonaje()
    {
        //primero activar el collider
        _boxCollider2D.enabled = true;
        //decir que ya no está derrotado
        Derrotado = false;
        Salud = saludInicial;
        ActualizarBarraVida(Salud, saludInicial);
    }


    protected override void ActualizarBarraVida(float vidaActual, float vidaMax)
    {
        //podemos utilizar el metodo de UIManager buscandolo ya que tiene una instancia y se busca dentro
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
}
