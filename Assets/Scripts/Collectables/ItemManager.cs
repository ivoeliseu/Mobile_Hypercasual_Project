using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;

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
    }

}
