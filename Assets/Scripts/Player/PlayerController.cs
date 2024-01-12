using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Controller")]
    public float speed = 1f;
    private Vector3 _pos;

    [Header("Learp")]
    public Transform target;
    public float lerpSpeed = 1;

    [Header("Game Over")]
    public string enemyTag = "Enemy";
    private bool _canRun;

    private void Start()
    {
        _canRun = true;
    }
    void Update()
    {
        PlayerMovement();
        
    }
    private void PlayerMovement()
    {
        if (!_canRun) return;
        //Salva a posi��o do grafico do player.
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        //Pega a posi��o do objeto control�vel e ap�s o atraso do lerpSpeed, move o gr�fico do player para a dire��o do objeto.
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        //Move o personagem para frente no cen�rio baseado na velocidade em speed
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    //Checagem de coliss�o com inimigos
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == enemyTag)
        {
            _canRun = false;
        }
    }
}
