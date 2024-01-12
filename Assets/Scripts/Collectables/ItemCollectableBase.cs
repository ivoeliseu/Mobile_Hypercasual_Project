using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public float timeToHide = 3f;

    public ParticleSystem particleSystem;
    public GameObject graphicItem;

    [Header("Sounds")]
    public AudioSource audioSource;


    //CASO DETECTAR TRIGGER, SE O OBJETO QUE COLIDIU TIVER A compareTag, usa a função COLLECT
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }

    //FUNÇÃO COLLECT, QUE PODE SER CHAMADA EM OUTRAS CONDIÇÕES
    protected virtual void Collect()
    {
        if(graphicItem != null) graphicItem.SetActive(false); //SE EXISTIR UM GRAPHIC ITEM, O DESABILITA.
        Invoke("HideObject", timeToHide); //INVOCA A FUNÇÃO HIDE OBJECT APÓS O TEMPO DE timeToHide
        OnCollect(); //ATIVA A FUNÇÃO OnCollect()
    }

    //FUNÇÃO HideObject DESATIVA O OBJETO.
    private void HideObject()
    {
        gameObject.SetActive(false); //Desativa o GameObject
    }

    //OnCollect REPRODUZ AS PARTICULAS E AUDIOS VINCULADOS AO OBJETO.
    protected virtual void OnCollect()
    {
        if (particleSystem != null) particleSystem.Play();
        if (audioSource != null) audioSource.Play();
    }
}
