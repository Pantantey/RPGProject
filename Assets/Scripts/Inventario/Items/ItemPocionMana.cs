using UnityEngine;

[CreateAssetMenu(menuName = "Items/PocionMana")] //agrega en el menu de crear en unity un apartado llamado items y dentro la pocion mana
public class ItemPocionMana : InventarioItem
{
    [Header("PocionInfo")]
    public float MPRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MPRestauracion);
            return true;
        }
        return false;
    }

}
