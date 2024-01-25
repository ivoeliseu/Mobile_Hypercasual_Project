using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    public enum Setups //Lista contendo os pr�defi��es de setups de cores.
    {
        COLOR01,
        COLOR02,
    }

    public List<ColorSetup> colorSetup; //Lista que ir� abrir com as variaveis da fun��o de Color Setup: Setup do enum Setups e as cores.

    public void ApplyColor(Setups setup, LevelPieceVisualElement[] visualElements) //Esse m�todo pega a lista enum com os Setups e os objetos na lista de LevelPieceVisualElement
    {
        if (visualElements == null || visualElements.Length == 0) return; //Se a lista for nula ou o tamanho for igual a 0, retorna

        for (int i = 0; i < colorSetup.Count; i++) //Ir� passar por todos os itens da lista colorSetup (LISTA 1).
        {
            if (colorSetup[i].setups == setup) //Se o valor passado em setup for igual ao INDEX de colorSetup, ir� adentrar nessa lista.
            {
                for (int j = 0; j < visualElements.Length; j++) //Ir� passar por todos os itens da lista visualElements (LISTA 2), que cont�m a lista com os objetos.
                {
                    for (int k = 0; k < visualElements[j].visualElement.Length; k++) // Ir� passar por todos os objetos na lista de objetos (LISTA 3)
                    {
                        var renderer = visualElements[j].visualElement[k].GetComponent<Renderer>(); // renderer � igual ao Renderer do GameObject daquele index da lista.
                        if (renderer != null) //Se renderer for diferente de Nulo, troca o renderer.
                        {
                            var mat = renderer.material; //material � igual ao material do objeto.
                            mat.color = colorSetup[i].colors[Random.Range(0, colorSetup[i].colors.Count)]; //Altera a cor aleat�riamente entre as disponiveis na lista colorSetup.
                            renderer.material = mat; //Altera o material do objeto para o material com a cor alterada.
                        }
                    }
                }
            }
        }

        return;

    }

}


//Essa parte do c�digo ir� abrir uma lista

[System.Serializable]
public class ColorSetup
{
    public ColorManager.Setups setups;
    public List<Color> colors;
}
