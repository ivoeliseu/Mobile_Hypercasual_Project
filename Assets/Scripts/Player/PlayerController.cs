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

    [Header("Start and Game Over")]
    public string enemyTag = "Enemy";
    public string endGameTag = "EndGame";
    public PauseController pauseController;
    public GameObject failScreen;
    public GameObject endGameScreen;
    private bool _canRun;
    private bool _fail;

    private void Start()
    {
        //No Start, cena iniciar� pausada aguardando o click no start.
        pauseController.Pause();
        _canRun = true;
        _fail = false;
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

   
    private void OnCollisionEnter(Collision collision)
    {
        //Se colidir com inimigo, _fail ser� true
        if (collision.transform.tag == enemyTag)
        {
            _fail = true;
        }
        //Checagem de coliss�o com inimigos OU o fim de jogo; Se colidir, jogo encerra com a tela de falha.
        if (collision.transform.tag == enemyTag || collision.transform.tag == endGameTag)
        {
            _canRun = false;
            pauseController.Pause();
            EndGame();
        }
    }

    private void EndGame()
    {
        //Se _fail for true, ir� ativar a tela de falha
        if (_fail == true)
        {
            failScreen.SetActive(true);
        }
        //Caso contr�tio, ativar� a tela de fim de jogo
        else
        {
            endGameScreen.SetActive(true);
        }
    }
}
