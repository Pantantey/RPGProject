using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesPersonaje : MonoBehaviour
{
    //para activar o desactivar los layer de las animaciones
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerCaminar;


    private Animator _animator;

    //se referencia la otra clase para sacar la direccion del personaje
    private MovimientoPersonaje _movimientoPersonaje;

    //Hacer una variable para usarla en todos los lugares
    public readonly int direccionX = Animator.StringToHash("X");
    public readonly int direccionY = Animator.StringToHash("Y");
    public readonly int derrotado = Animator.StringToHash("Derrotado");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movimientoPersonaje = GetComponent<MovimientoPersonaje>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //para que siempre se esté actualizando entre moverse o quieto
        ActualizarLayers();

        //para saber si estamos en movimiento y quedarnos en la ultima animacion cuando soltamos el boton
        if (_movimientoPersonaje.EnMovimiento)
        {
            //animator se compone por (X, numero) o (Y, numero)
            _animator.SetFloat(direccionX, _movimientoPersonaje.DireccionMovimiento.x);
            _animator.SetFloat(direccionY, _movimientoPersonaje.DireccionMovimiento.y);
        }
    }

    private void ActivarLayer(string nombreLayer)
    {
        //para activar un layer hay que desactivar todos los demas. se usa un for para eso
        for (int i = 0; i< _animator.layerCount; i++)
        {
            //este le da un valor a los layer (0 =desactivado o 1 =activado)
            _animator.SetLayerWeight(i, 0);
        }

        //se busca el index del layer por el nombre y luego se activa
        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }

    private void ActualizarLayers()
    {
        if (_movimientoPersonaje.EnMovimiento)
        {
            ActivarLayer(layerCaminar);
        }
        else
        {
            ActivarLayer(layerIdle);
        }
    }


    //cuando se revive el personaje no queda con la animacion de derrotado y se llama en la clase personaje
    public void RevivirPersonaje()
    {
        ActivarLayer(layerIdle);
        _animator.SetBool(derrotado, false);
    }

    //para q una clase se sobreescriba a un evento se usan los metodos OnEnable y OnDisable
    private void PersonajeDerrotadoRespuesta()
    {
        //debemos ver si estamos dentro del Layer de Idle (donde se encuentra la animacion de personaje derrotado)
        //GetLayerWeight devuelve 1 si está activado y dentro recibe el index para saber si está activado | getLayerIndex dentro recibe el layer para devolver el index del layer
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1){
            _animator.SetBool(derrotado, true);
        }
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(layerCaminar)) == 1)
        {
            _animator.SetBool(derrotado, true);
        }
    }

    //OnEnable se usa para suscribirnos a un evento
    private void OnEnable()
    {
        PersonajeVida.EventoPersonajeDerrotado += PersonajeDerrotadoRespuesta;
    }

    //OnDisable se usa para desuscribirse a un evento
    private void OnDisable()
    {
        PersonajeVida.EventoPersonajeDerrotado -= PersonajeDerrotadoRespuesta;
    }


}
