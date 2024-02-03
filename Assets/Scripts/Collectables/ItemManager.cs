using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI coinsScore;
    public TextMeshProUGUI coinsScoreFail;
    public TextMeshProUGUI coinsScoreFinish;

    private void Start()
    {
        Reset();
    }

    // Reinicia o valor das moedas
    private void Reset()
    {
        coins.value = 0;
    }

    //Adiciona valor nas moedas
    public void AddCoins(int value)
    {
        coins.value += value;
        HudUpdate();
    }

    //Atualiza no placar as moedas coletadas.
    public void HudUpdate()
    {
        coinsScore.text = coins.value.ToString();
        coinsScoreFail.text = coins.value.ToString();
        coinsScoreFinish.text = coins.value.ToString();
    }

}
