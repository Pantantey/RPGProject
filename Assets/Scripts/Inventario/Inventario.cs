using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    [SerializeField] private Personaje personaje;
    [SerializeField] private InventarioItem[] itemsInventario;
    [SerializeField] private int numeroSlots;

    public Personaje Personaje => personaje;
    public int NumeroSlots => numeroSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroSlots];
    }

    public void A�adirItem(InventarioItem itemPorA�adir, int cantidad)
    {
        if (itemPorA�adir == null)
        {
            return;
        }

        //Verificacion de agregar un item ACUMULABLE que existe previamente en inventario
        List<int> indexes = VerificarExistencias(itemPorA�adir.ID);
        if (itemPorA�adir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i=0; i< indexes.Count ;i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemPorA�adir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemPorA�adir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemPorA�adir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemPorA�adir.AcumulacionMax;
                            A�adirItem(itemPorA�adir, diferencia);
                        }
                        InventarioUI.Instance.DibujarItemEnInventario(itemPorA�adir, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        //Agregar un item NUEVO a inventario
        if (cantidad <= 0)
        {
            return;
        }

        if (cantidad > itemPorA�adir.AcumulacionMax)
        {
            A�adirItemEnSlotDisponible(itemPorA�adir, itemPorA�adir.AcumulacionMax);
            cantidad -= itemPorA�adir.AcumulacionMax;
            A�adirItem(itemPorA�adir,cantidad);
        }
        else
        {
            A�adirItemEnSlotDisponible(itemPorA�adir,cantidad);
        }

    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesDelItem = new List<int>();
        for (int i=0; i <itemsInventario.Length; i++)
        {
            if (itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexesDelItem.Add(i);
                }
            }
        }
        return indexesDelItem;
    }

    private void A�adirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for (int i =0; i < itemsInventario.Length ;i++)
        {
            //si hay un slot vacio
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
        
    }

    private void EliminarItem(int index)
    {
        itemsInventario[index].Cantidad--;
        if (itemsInventario[index].Cantidad<=0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }
    }

    public void MoverItem(int indexInicial, int indexFinal)
    {
        if (itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }

        //copiar el item en el slot final
        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.Cantidad, indexFinal);

        //borramos item del slot inicial
        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null,0,indexInicial);
    }

    private void UsarItem(int index)
    {
        if (itemsInventario[index] == null)
        {
            return;
        }
        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }


    #region Eventos

    private void SlotInteraccionRespuesta(TipoInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoInteraccion.Equipar:
                break;
            case TipoInteraccion.Remover:
                break;
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
}
