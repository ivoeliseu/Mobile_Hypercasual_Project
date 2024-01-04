using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpHelper : MonoBehaviour
{
    public Transform target;

    public float lerpSpeed = 1;
    private void Update()
    {
        //Pega a posição do alvo e após o atraso do lerpSpeed, move o objeto.
        transform.position = Vector3.Lerp(transform.position, target.position, lerpSpeed * Time.deltaTime);
    }
}
