using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TipoInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}

public class InventarioSlot : MonoBehaviour
{
    public static Action<TipoInteraccion, int> EventoSlotInteraccion;


    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;

    public int Index { get; set; }
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        itemIcono.sprite = item.Icono;
        cantidadTMP.text = cantidad.ToString();
    }

    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }

    public void SeleccionarSlot()
    {
        _button.Select();
    }

    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TipoInteraccion.Click, Index);

        //Mover item
        if (InventarioUI.Instance.IndexSlotInicialPorMover != -1)
        {
            if (InventarioUI.Instance.IndexSlotInicialPorMover != Index)
            {
                //mover
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotInicialPorMover, Index);
            }
        }
    }

    public void SlotUsarItem()
    {
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoInteraccion.Usar, Index);
        }
    }
}
