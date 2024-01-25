using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class CoinsAnimations : Singleton<CoinsAnimations>
{
    public List<ItemCollectableCoin> itens; //Lista que puxa as moedas do jogo

    [Header("Animation")]
    public float scaleDuration = .2f; //Dura��o que ir� demorar para escalar o objeto de tamanho
    public float scaleTimeBetweenCoins = .1f; //Dura��o entre as escaladas
    public Ease ease = Ease.OutBack; //Efeito de anima��o

    private void Start()
    {
        itens = new List<ItemCollectableCoin>(); //Lista com as moedas registradas do jogo
    }

    public void RegisterCoin(ItemCollectableCoin i)
    {
        if (!itens.Contains(i)) //Se a lista n�o cont�m esse objeto [...]
        {
            itens.Add(i); // [...] ele o adiciona na lista; � usado para n�o ter a mesma moeda duplicada na lista.
            i.transform.localScale = Vector3.zero; //o transform fica com tamanho zero.
        }
    }

    public void StartAnimations()
    {
        StartCoroutine(ScaleCoinsByTime());
    }

    //Corrotina que ir� escalar o tamanho das moedas de 0 para 1
    IEnumerator ScaleCoinsByTime()
    {
        //Para cada vari�vel na lista "itens", o transform fica com tamanho zero.
        foreach (var coin in itens)
        {
            coin.transform.localScale = Vector3.zero;
        }

        //Ir� chamar a fun��o sorte para arrumar a lista
        Sort();

        //Retorna o valor nulo para a corrotina
        yield return null;

        //v�riavel index 0, enquanto for menor que a contagem de itens da lista "itens", faz o processo, index +1
        for (int index = 0; index < itens.Count; index++)
        {
            //Ferramenta DOTWEEN, pegar� o transform do item da lista "itens" no index "index" e escalar� o transform dele para o tamanho 1, em "scaleDuration" segundos
            //O tipo de anima��o � definido pela vari�vel SetEase
            itens[index].transform.DOScale(1, scaleDuration).SetEase(ease);

            //Retorna para a corrotina esperar por "scaleTimeBetweenCoins" segundo
            yield return new WaitForSeconds(scaleTimeBetweenCoins);
        }
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartAnimations();
        }
    }
    */

    //A fun��o SORT ir� ordenar a lista conforme algum crit�rio, definido por "OrderBy"
    public void Sort()
    {
        // Lista � igual a ela mesma ordenada por:
        // o item (x) com menor alcance ir� ser ordenado para cima da lista, sendo os primeiros index. Manda isso para a lista com o .ToList()
        itens = itens.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }

}
