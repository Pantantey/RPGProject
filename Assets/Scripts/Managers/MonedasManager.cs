using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasManager : Singleton<MonedasManager>
{
    [SerializeField] private int monedasTest;
    public int MonedasTotales { get; set; }

    private string KEY_MONEDAS = "MiJuego_Monedas";

    private void Start()
    {
        PlayerPrefs.DeleteKey(KEY_MONEDAS); //borrar si no quiero q se reinicie cada que abro el juego igual que el monedas test probablemente
        CargarMonedas();
    }

    private void CargarMonedas()
    {
        MonedasTotales = PlayerPrefs.GetInt(KEY_MONEDAS, monedasTest);
    }

    public void AñadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }

    public void RemoverMonedas(int cantidad)
    {
        if (MonedasTotales < cantidad)
        {
            return;
        }

        MonedasTotales -= cantidad;
        PlayerPrefs.SetInt(KEY_MONEDAS, MonedasTotales);
        PlayerPrefs.Save();
    }


}
