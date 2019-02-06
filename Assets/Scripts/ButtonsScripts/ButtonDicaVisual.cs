using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class ButtonDicaVisual : MonoBehaviour/*, IPointerEnterHandler, IPointerExitHandler*/
{

    public static ButtonDicaVisual instance = null;
    private Button BotaoDicaVisual;
    private StageManager stageManager;
    private Text [] TelaSilabaDigitada;
    private SilabaControl silabaControl;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        BotaoDicaVisual = GameObject.FindGameObjectWithTag("Button Eye").GetComponent<UnityEngine.UI.Button>();
    }

    private void Start()
    {
        stageManager = StageManager.instance;
        TelaSilabaDigitada = stageManager.GetTelaSilabaDigitada();
        silabaControl = SilabaControl.instance;

        // Reseta a marcação da dica visual nas estatísticas
        DataManager.statisticsData.dicaVisual = false;

        if (SaveManager.player.CompletouPlaneta(stageManager.currentPlanet))
        {
            DeactiveButton();
        }
    }


    public void ActiveButton()
    {
        if (!SaveManager.player.CompletouPlaneta(stageManager.currentPlanet)) { 
        BotaoDicaVisual = GameObject.FindGameObjectWithTag("Button Eye").GetComponent<UnityEngine.UI.Button>(); //Como o canvas é destruído entre as scenes, é necessário reestabelecer a referência.
        BotaoDicaVisual.interactable = true;
        }
    }

    public void DeactiveButton()
    {
        BotaoDicaVisual.interactable = false;
    }

    public void AcionaDicaVisual()//botao dica visual
    {
        if(!LevelController.bloqueiaBotao)
        {
            // Marca que o jogador utilizou a dica visual na palavra
            DataManager.statisticsData.dicaVisual = true;
            BotaoDicaVisual.interactable = false;//desabilita dica visual
            StartCoroutine(MostraDica());
        }
    }

    IEnumerator MostraDica()
    {
        LevelController.DicaVisualAtiva = true;
        // Sorteia um numero aleátorio entre todos os valores possíveis válidos
        int randomNumber = Random.Range(0, silabaControl.numberOfValidSlots);
        // A var posDica mostra o local da dica na tela (posRelDica é utilizado para verificar a posição randomica gerada)
        int posDica = 0, posRelDica = 0;
        // Varre todos os textSlots a procura da posição correta
        for (posDica = 0; posDica < stageManager.textSlots; posDica++)
        {
            if (silabaControl.isPlanetLetter[posDica])
            {
                if (posRelDica == randomNumber)
                {
                    break;
                }
                else
                {
                    posRelDica++;
                }
            }
        }
        // Mostra a dica, espera por um tempo e apaga a dica
        TelaSilabaDigitada[posDica].text = LevelController.originalText[posDica];
        yield return new WaitForSeconds(1);
        TelaSilabaDigitada[posDica].text = LevelController.inputText[posDica];
        LevelController.DicaVisualAtiva = false;
        
    }
}
