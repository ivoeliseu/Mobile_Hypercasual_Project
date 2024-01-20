using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentHelper : MonoBehaviour
{
    //Lista com posi��es que o objeto ir� se movimentar
    public List<GameObject> positions;

    //Dura��o do movimento
    public float duration = 1f;

    //Ir� cuidar de pular para o pr�ximo index da lista para puxar a pr�xima posi��o para se movimentar
    private int _index = 0;

    public void Start()
    {
        InicialPosition();
        StartCoroutine(StartMoviment());
        NextIndex();
    }

    //Corrotina que movimentar� o objeto
    IEnumerator StartMoviment()
    {
        //Tempo que far� a corrotina funcionar
        float time = 0;

        //Enquanto a corrotina for verdadeira, ir� manter a fun��o dentro desse la�o
        while (true)
        {
            var currentPosition = transform.position;

            //Enquanto a v�riavel time da corrotina for menor que a duration, esse la�o se repete.
            while (time < duration)
            {
                //posi��o = Lerp entre a atual posi��o para a nova posi��o no tempo de time/duration
                transform.position = Vector3.Lerp(currentPosition, positions[_index].transform.position, (time/duration));

                //time estabiliziado por deltatime
                time += Time.deltaTime;

                //Encerra o la�o para n�o ficar travado para sempre no While.
                yield return null;
            }

            NextIndex();

            //Redefine a vari�vel time para que execute o pr�ximo movimento.
            time = 0;

            //Encerra o la�o para n�o ficar travado para sempre no While.
            yield return null;
        }
    }

    private void NextIndex()
    {
        //Adiciona 1 na vari�vel index;
        _index++;

        //Se o index chegar no �ltimo item da lista ou passar, o index � zerado.
        if (_index >= positions.Count) _index = 0;
    }

    private void InicialPosition()
    {
        //Faz com que o objeto inicie na posi��o inicial (index 0 da lista)
        transform.position = positions[0].transform.position;
    }
}
