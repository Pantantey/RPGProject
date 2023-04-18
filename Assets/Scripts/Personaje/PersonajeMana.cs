using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float regeneracionPorSegundo;

    public float ManaActual { get; private set; }
    public bool SePuedeRestaurar => ManaActual < manaMax;

    //se crea para utilizar los metodos y variables
    private PersonajeVida _personajeVida;

    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }


    // Start is called before the first frame update
    void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();

        //este metodo sirve para llamar otro metodo y se le puede poner cada cuantos segundos se llama y de ultimo la cantidad de repeticiones
        InvokeRepeating(nameof(RegenerarMana), 2, 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10);
        }
    }

    public void UsarMana(float cantidad)
    {
        if(ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    public void RestaurarMana(float cantidad)
    {
        if (ManaActual >= manaMax)
        {
            return;
        }

        ManaActual += cantidad;
        if (ManaActual > manaMax)
        {
            ManaActual = manaMax;
        }

        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }

    private void RegenerarMana()
    {
        if(_personajeVida.Salud > 0 && ManaActual < manaMax)
        {
            ManaActual += regeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    //se llama este metodo en personaje
    public void RestablecerMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }

    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }

    public void QuitarMana()
    {
        ManaActual = 0f;
        ActualizarBarraMana();
    }
}
