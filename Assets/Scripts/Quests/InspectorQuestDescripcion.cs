using TMPro;
using UnityEngine;
public class InspectorQuestDescripcion : QuestDescripcion
{

    [SerializeField] private TextMeshProUGUI questRecompensa;

    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest); //esto llama lo que se puso en el metodo original
        questRecompensa.text = $"-{quest.RecompensaOro} oro" +
            $"\n-{quest.RecompensaExp} exp" + 
            $"\n-{quest.RecompensaItem.Item.Nombre} x{quest.RecompensaItem.Cantidad}";
    }

    public void AceptarQuest()
    {
        if (QuestPorCompletar == null)
        {
            return;
        }

        QuestManager.Instance.AñadirQuest(QuestPorCompletar);
        gameObject.SetActive(false);
    }
}
