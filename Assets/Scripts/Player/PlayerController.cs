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
        //Salva a posição do grafico do player.
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        //Pega a posição do objeto controlável e após o atraso do lerpSpeed, move o gráfico do player para a direção do objeto.
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        //Move o personagem para frente no cenário baseado na velocidade em speed
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    //Checagem de colissão com inimigos
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == enemyTag)
        {
            _canRun = false;
        }
    }
}
