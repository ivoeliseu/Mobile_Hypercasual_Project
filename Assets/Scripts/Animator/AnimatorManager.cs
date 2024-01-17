using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator; //Váriavel para vincular o objeto que está com o animator
    public List<AnimatorSetup> animatorSetups; //Lista da Unity que permite realizar o setup de triggers

    public enum AnimationType  //Lista com os tipos de animação
    {
        IDLE,
        RUN,
        DEATH
    }

    public void Play(AnimationType type, float _speedTimes = 1f) //Função que irá tocar a animação, irá aceitar um dos elementos da lista acima.
    {
        animatorSetups.ForEach(i => //Busca na lista de animatorSetups por cada váriavel.
        {
            if(i.type == type) //Se o AnimationType passado para play na váriavel i for igual a um dos elementos da lista, entra na função.
            {
                animator.SetTrigger(i.trigger); //Ativa o trigger da animação de i.
                animator.speed = i.animationSpeed * _speedTimes; //Velocidade da animação será igual ao trigger i e a variável de animationSpeed multiplicado pelo valor passado na variável _speedTimes
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
        //Se detectar o botão 1 pressionado, Manda para a função PLAY: IDLE, o mesmo com os abaixo e suas variáveis.
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

    [System.Serializable] //Permite que a Unity deixe nós colocarmos a variável
    public class AnimatorSetup
    {
        public AnimatorManager.AnimationType type; //Váriavel que busca a animação na lista.
        public string trigger;
        public float animationSpeed = 1f;
    }
}
