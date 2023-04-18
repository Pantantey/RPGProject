using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//agregar UnityEngine.UI y TMPro
using UnityEngine.UI;
using TMPro;


//se crea un llamado a Singleton para poder utilizar los metodos de esta clase en otras clases
public class UIManager : Singleton<UIManager>
{
    //para actualizar el panel se ocupa una referencia al panel y otra al sriptable object
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStats;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelInspectorQuests;
    [SerializeField] private GameObject panelPersonajeQuests;

    //agrega un header a unity para diferenciar los campos
    [Header("Barras")]
    [SerializeField] private Image vidaPlayer;
    [SerializeField] private Image manaPlayer;
    [SerializeField] private Image expPlayer;

    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI nivelTMP;
    [SerializeField] private TextMeshProUGUI monedasTMP;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statBloqueoTMP;
    [SerializeField] private TextMeshProUGUI statVelocidadTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statExpRequeridaTMP;

    [SerializeField] private TextMeshProUGUI atributosDisponiblesTMP;
    [SerializeField] private TextMeshProUGUI atributoFuerzaTMP;
    [SerializeField] private TextMeshProUGUI atributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI atributoDestrezaTMP;
    


    private float vidaActual;
    private float vidaMax;

    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expRequeridaNuevoNivel;

    // Update is called once per frame
    void Update()
    {
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }

    private void ActualizarUIPersonaje()
    {
        //con esto se modifica la barra, ya que es por porcentaje de llenado de imagen se usa fillAmount
        //modificar la cantidad de llenado de vida y mana y experiencia
        vidaPlayer.fillAmount = Mathf.Lerp(vidaPlayer.fillAmount, vidaActual / vidaMax, 10f * Time.deltaTime);
        manaPlayer.fillAmount = Mathf.Lerp(manaPlayer.fillAmount, manaActual / manaMax, 10f * Time.deltaTime);
        expPlayer.fillAmount = Mathf.Lerp(expPlayer.fillAmount, expActual / expRequeridaNuevoNivel, 10f * Time.deltaTime);

        //este se utiliza para modificar el texto
        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        manaTMP.text = $"{manaActual}/{manaMax}";
        expTMP.text = $"{expActual}/{expRequeridaNuevoNivel}";
        nivelTMP.text = $"Nivel {stats.Nivel}";
        monedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();
    }

    private void ActualizarPanelStats()
    {
        //preguntar si el panel stats está activo
        if (panelStats.activeSelf == false)
        {
            return;
        }

        statDañoTMP.text = stats.Daño.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        //para mostrar critico y el bloqueo en porcentaje, ya que va en rango de 0 a 100
        statCriticoTMP.text = $"{stats.PorcentajeCrítico}%";
        statBloqueoTMP.text = $"{stats.PorcentajeBloqueo}%";

        statVelocidadTMP.text = stats.Velocidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statExpRequeridaTMP.text = stats.ExpRequeridaSiguienteNivel.ToString();

        //actualizar los atributos
        atributosDisponiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}";
        atributoFuerzaTMP.text = stats.Fuerza.ToString();
        atributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        atributoDestrezaTMP.text = stats.Destreza.ToString();

    }


    //estos metodos se utilizan para poder obtener sus valores, se llama cada metodo desde su respectiva clase
    public void ActualizarVidaPersonaje(float pVidaActual, float pVidaMax)
    {
        vidaActual = pVidaActual;
        vidaMax = pVidaMax;
    }

    public void ActualizarManaPersonaje(float pManaActual, float pManaMax)
    {
        manaActual = pManaActual;
        manaMax = pManaMax;
    }
    public void ActualizarExpPersonaje(float pExpActual, float pExpRequeridaNuevoNivel)
    {
        expActual = pExpActual;
        expRequeridaNuevoNivel = pExpRequeridaNuevoNivel;
    }

    #region Paneles

    public void AbrirCerrarPanelStats()
    {
        panelStats.SetActive(!panelStats.activeSelf);
    }

    public void AbrirCerrarPanelInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }

    public void AbrirCerrarPanelPersoanjeQuest()
    {
        panelPersonajeQuests.SetActive(!panelPersonajeQuests.activeSelf);
    }

    public void AbrirCerrarPanelIspectorQuests()
    {
        panelInspectorQuests.SetActive(!panelInspectorQuests.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
            case InteraccionExtraNPC.Quest:
                AbrirCerrarPanelIspectorQuests();
                break;
            case InteraccionExtraNPC.Tienda:
                break;
            case InteraccionExtraNPC.Crafting:
                break;
        }
    }

    #endregion

}
