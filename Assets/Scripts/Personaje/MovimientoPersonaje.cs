using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    //para poder modificar la variable en unity
    [SerializeField] private float velocidad;

    //para saber si estamos en movimiento
    public bool EnMovimiento => _direccionMovimiento.magnitude > 0f;

    //se usa para hacer publico el valor de direccion movimiento y usarlo en otra clase
    public Vector2 DireccionMovimiento => _direccionMovimiento;

    //utilizar alguna variable de personajeVida
    private PersonajeVida _personajeVida;

    private Rigidbody2D _rigidbody2D;
    private Vector2 _direccionMovimiento;
    private Vector2 _input;

    

    private void Awake()
    {
        //se inicializan las clases
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _personajeVida = GetComponent<PersonajeVida>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_personajeVida.Derrotado)
        {
            //saber si se presiona una tecla para moverse a algun sitio
            _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //saber la direccion en la que se quiere mover en X
            if (_input.x > 0f)
            {
                _direccionMovimiento.x = 1f;
            }
            else if (_input.x < 0f)
            {
                _direccionMovimiento.x = -1f;
            }
            else
            {
                _direccionMovimiento.x = 0f;
            }
            //saber la direccion en la que se quiere mover en Y
            if (_input.y > 0f)
            {
                _direccionMovimiento.y = 1f;
            }
            else if (_input.y < 0f)
            {
                _direccionMovimiento.y = -1f;
            }
            else
            {
                _direccionMovimiento.y = 0f;
            }
        }
        else if (_personajeVida.Derrotado)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        _input.y = 0f;
        _input.x = 0f;
    }

    //se usa xq se está usando un rigidbody
    private void FixedUpdate()
    {
        if (!_personajeVida.Derrotado)
        {
            //mover el personaje
            _rigidbody2D.MovePosition(_rigidbody2D.position + _direccionMovimiento * velocidad * Time.fixedDeltaTime);
        }
    }


}
