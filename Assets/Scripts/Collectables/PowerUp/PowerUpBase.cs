using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemCollectableBase
{
    [Header("Power Up")]
    public float duration;

    //Chama a base de OnCollect com o adicional de chamar a função StartPowerUp
    protected override void OnCollect() 
    { 
        base.OnCollect(); 
        StartPowerUp(); 
    }

    //Inicia o power UP e aguardar o tempo determinado em duration para encerra-lo
    protected virtual void StartPowerUp()
    {
        PlayerController.Instance.Bounce();
        Debug.Log("Start Power Up");
        Invoke(nameof(EndPowerUp), duration);
    }

    //Finaliza o power up
    protected virtual void EndPowerUp() 
    { 
        Debug.Log("End Power Up"); 
    }
}
