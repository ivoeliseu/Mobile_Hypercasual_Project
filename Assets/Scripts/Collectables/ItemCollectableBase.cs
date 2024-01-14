using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide = 3f;

    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;


    //CASO DETECTAR TRIGGER, SE O OBJETO QUE COLIDIU TIVER A compareTag, usa a fun��o COLLECT
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    //FUN��O COLLECT, QUE PODE SER CHAMADA EM OUTRAS CONDI��ES
    protected virtual void Collect()
    {
        if(graphicItem != null) graphicItem.SetActive(false); //SE EXISTIR UM GRAPHIC ITEM, O DESABILITA.
        Invoke("HideObject", timeToHide); //INVOCA A FUN��O HIDE OBJECT AP�S O TEMPO DE timeToHide
        OnCollect(); //ATIVA A FUN��O OnCollect()
    }

    //FUN��O HideObject DESATIVA O OBJETO.
    private void HideObject()
    {
        gameObject.SetActive(false); //Desativa o GameObject
    }

    //OnCollect REPRODUZ AS PARTICULAS E AUDIOS VINCULADOS AO OBJETO.
    protected virtual void OnCollect()
    {
        if (GetComponent<ParticleSystem>() != null) GetComponent<ParticleSystem>().Play();
        if (audioSource != null) audioSource.Play();
    }
}
