using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator; //V�riavel para vincular o objeto que est� com o animator
    public List<AnimatorSetup> animatorSetups; //Lista da Unity que permite realizar o setup de triggers

    public enum AnimationType  //Lista com os tipos de anima��o
    {
        IDLE,
        RUN,
        DEATH
    }

    public void Play(AnimationType type, float _speedTimes = 1f) //Fun��o que ir� tocar a anima��o, ir� aceitar um dos elementos da lista acima.
    {
        animatorSetups.ForEach(i => //Busca na lista de animatorSetups por cada v�riavel.
        {
            if(i.type == type) //Se o AnimationType passado para play na v�riavel i for igual a um dos elementos da lista, entra na fun��o.
            {
                animator.SetTrigger(i.trigger); //Ativa o trigger da anima��o de i.
                animator.speed = i.animationSpeed * _speedTimes; //Velocidade da anima��o ser� igual ao trigger i e a vari�vel de animationSpeed multiplicado pelo valor passado na vari�vel _speedTimes
            }
        }
        );

        /* Significa o mesmo de:

        foreach (var animation in animatorSetups)
        {
            if (animation.type == type)
            {
                animator.SetTrigger(animation.trigger);
                break;
            }
        }

        */

        
    }
    private void Update()
    {
        //Se detectar o bot�o 1 pressionado, Manda para a fun��o PLAY: IDLE, o mesmo com os abaixo e suas vari�veis.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Play(AnimationType.IDLE);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Play(AnimationType.RUN);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Play(AnimationType.DEATH);
        }
    }

    [System.Serializable] //Permite que a Unity deixe n�s colocarmos a vari�vel
    public class AnimatorSetup
    {
        public AnimatorManager.AnimationType type; //V�riavel que busca a anima��o na lista.
        public string trigger;
        public float animationSpeed = 1f;
    }
}
