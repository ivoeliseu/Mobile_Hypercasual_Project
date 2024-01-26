using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceHelper : MonoBehaviour
{
    [Header("Animation")]
    public float scaleDuration = .05f;
    public float scaleBounce = .1f;
    public Ease ease = Ease.Linear;

    //Faz animação de aumentar o objeto. Aumenta pelo valor de "scaleBounce" por duração de "scaleDuration".O tipo de animação é setada por "ease" e faz 2 loops, do tipo Yoyo, então aumenta e diminui.
    public void Bounce()
    {
        DOTween.Kill(scaleDuration, scaleBounce);
        transform.DOScale(scaleBounce, scaleDuration).SetEase(ease).SetLoops(2, LoopType.Yoyo).WaitForCompletion();
    }
}
