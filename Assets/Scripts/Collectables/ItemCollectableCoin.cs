using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public int coinValue = 1;
    public Collider coinCollider;

    private void Start()
    {
        CoinsAnimations.Instance.RegisterCoin(this); //Ao iniciar, regista o objeto com esse script na lista do singleton de CoinsAnimations.
    }

    //Executa OnCollect do Script ItemCollectableBase com algumas linhas adicionais
    protected override void OnCollect()
    {
        base.OnCollect(); //Pega a base de OnCollect
        ItemManager.Instance.AddCoins(coinValue); //Adiciona a pontução da moeda
        coinCollider.enabled = false; //Desativa o colisor para evitar do player pontuar a mesma moeda mais de uma vez
        PlayerController.Instance.Bounce();
    }
}
