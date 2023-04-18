using UnityEngine;

[CreateAssetMenu(menuName = "Items/PocionVida")]
public class ItemPocionVida : InventarioItem
{
    [Header("PocionInfo")]
    public float HPRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeSerCurado)
        {
            //si el personaje puede ser curado y se cura se devuelve un true
            Inventario.Instance.Personaje.PersonajeVida.RestaurarSalud(HPRestauracion);
            return true;
        }
        return false;
    }
}
