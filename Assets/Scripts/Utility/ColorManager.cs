using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    //Lista contendo os pr�defi��es de setups de cores.
    public enum Setups
    {
        COLOR01,
        COLOR02,
    }

    //Lista que ir� abrir com as variaveis da fun��o de Color Setup: Setup do enum Setups e as cores.
    public List<ColorSetup> colorSetup;
}

//Essa parte do c�digo ir� abrir uma lista

[System.Serializable]
public class ColorSetup
{
    public ColorManager.Setups setups;
    public List<Color> colors;
}
