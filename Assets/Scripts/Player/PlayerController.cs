using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Player Controller")]
    public float startSpeed = 1f;
    public float _currentSpeed;
    
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

    [Header("Power Ups")]
    public TextMeshPro TextPowerUp;
    public bool invencible = false; //Variável utilizada para o PowerUp de Invencibilidade
    
    private Vector3 _startPosition;

    [Header("Animation")]
    public AnimatorManager animatorManager;



    private void Start()
    {
        //Usado para capturar a posição inicial do objeto.
        _startPosition = transform.position; 
        ResetSpeed();
        //No Start, cena iniciará pausada aguardando o click no start.
        //pauseController.Pause();
        _canRun = false;
        _fail = false;
    }
    void Update()
    {
        PlayerMovement();
    }

    public void StartGameplay() //Quando clicar no botão de iniciar, _canRun será true para permitir o movimento e irá iniciar a animação de RUN
    {
        _canRun = true;
        //Ativa o trigger da animação RUN, passando o valor igual a velocidade atual do player dividido pelo start speed para acelerar a animação
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
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
        //Move o personagem para frente no cenário baseado na velocidade em _currentSpeed
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (invencible) return; //Se invencible for TRUE, não executa o código abaixo.
        //Se colidir com inimigo, _fail será true
        if (collision.transform.tag == enemyTag)
        {
            _fail = true; //Se invencible for false, _fail será true
            animatorManager.Play(AnimatorManager.AnimationType.DEATH); //Irá tocar a animação de morte
        }
        //Checagem de colissão com inimigos OU o fim de jogo; Se colidir, jogo encerra com a tela de falha.
        if (collision.transform.tag == enemyTag || collision.transform.tag == endGameTag)
        {
            _canRun = false;
            //pauseController.Pause();
            EndGame();
            if (_fail == false) animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        }
    }

    private void EndGame()
    {
        //Se _fail for true, irá ativar a tela de falha
        if (_fail == true)
        {
            failScreen.SetActive(true);
        }
        //Caso contrátio, ativará a tela de fim de jogo
        else
        {
            endGameScreen.SetActive(true);
        }
    }
    #region POWERUPS
    public void SetPowerUpText(string s) //Váriavel recebendo o texto de descrição dos Power Ups
    { 
        TextPowerUp.text = s; 
    }
    public void PowerUpSpeedUp(float f) //Variável boleana recebendo valor true ou false do script PowerUpSpeedUp
    { 
        _currentSpeed = f; 
    }
    public void ResetSpeed() //Retorna o speed a velocidade original.
    { 
        _currentSpeed = startSpeed; 
    }

    public void SetPowerUpInvencible(bool b) //Variável boleana recebendo valor true ou false do script PowerUpInvencible
    {
        invencible = b;
    }
    public void PowerUpChangeHeight(float amount, float duration, float animationDuration, Ease ease) 
    {
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
    }
    public void ResetHeight(float animationDuration) //Encerra o POWERUP ChangeHeight
    {
        transform.DOMoveY(_startPosition.y, animationDuration);
    }
    #endregion
}
