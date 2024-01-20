using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentHelper : MonoBehaviour
{
    //Lista com posições que o objeto irá se movimentar
    public List<GameObject> positions;

    //Duração do movimento
    public float duration = 1f;

    //Irá cuidar de pular para o próximo index da lista para puxar a próxima posição para se movimentar
    private int _index = 0;

    public void Start()
    {
        InicialPosition();
        StartCoroutine(StartMoviment());
        NextIndex();
    }

    //Corrotina que movimentará o objeto
    IEnumerator StartMoviment()
    {
        //Tempo que fará a corrotina funcionar
        float time = 0;

        //Enquanto a corrotina for verdadeira, irá manter a função dentro desse laço
        while (true)
        {
            var currentPosition = transform.position;

            //Enquanto a váriavel time da corrotina for menor que a duration, esse laço se repete.
            while (time < duration)
            {
                //posição = Lerp entre a atual posição para a nova posição no tempo de time/duration
                transform.position = Vector3.Lerp(currentPosition, positions[_index].transform.position, (time/duration));

                //time estabiliziado por deltatime
                time += Time.deltaTime;

                //Encerra o laço para não ficar travado para sempre no While.
                yield return null;
            }

            NextIndex();

            //Redefine a variável time para que execute o próximo movimento.
            time = 0;

            //Encerra o laço para não ficar travado para sempre no While.
            yield return null;
        }
    }

    private void NextIndex()
    {
        //Adiciona 1 na variável index;
        _index++;

        //Se o index chegar no último item da lista ou passar, o index é zerado.
        if (_index >= positions.Count) _index = 0;
    }

    private void InicialPosition()
    {
        //Faz com que o objeto inicie na posição inicial (index 0 da lista)
        transform.position = positions[0].transform.position;
    }
}
