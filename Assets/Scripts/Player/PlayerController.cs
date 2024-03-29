using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : Singleton<PlayerController>
{
    [Header("Player Controller")]
    public float startSpeed = 1f;
    public float _currentSpeed;
    public float limit = 4f;
    
    private Vector3 _pos;

    [Header("Learp")]
    public Transform target;
    public float lerpSpeed = 1;

    [Header("Start and Game Over")]
    public string enemyTag = "Enemy";
    public string endGameTag = "EndGame";
    public PauseController pauseController;
    public LevelManager levelManager;
    public string levelSpawnerTag;

    [Header("HUD Configuration")]
    public GameObject failScreen;
    public GameObject endGameScreen;
    public GameObject inGameHud;

    private bool _canRun;
    private bool _fail;

    [Header("Power Ups")]
    public TextMeshPro TextPowerUp;
    public bool invencible = false; //Vari�vel utilizada para o PowerUp de Invencibilidade
    
    private Vector3 _startPosition;

    [Header("Animation")]
    public AnimatorManager animatorManager;
    [SerializeField] private BounceHelper _bounceHelper;
    [SerializeField] private GameObject _startAnimationGrowUp;

    [Header("VFX")]
    public ParticleSystem endGameParticles;



    private void Start()
    {
        StartAnimationGrowUp();
        //Usado para capturar a posi��o inicial do objeto.
        _startPosition = transform.position; 
        ResetSpeed();
        //No Start, cena iniciar� pausada aguardando o click no start.
        //pauseController.Pause();
        _canRun = false;
        _fail = false;
    }
    void Update()
    {
        PlayerMovement();
    }

    public void Bounce()
    {
        if (_bounceHelper != null)
        {
            _bounceHelper.Bounce();
        }
    }

    public void StartAnimationGrowUp()
    {
        _startAnimationGrowUp.transform.localScale = Vector3.zero;
        _startAnimationGrowUp.transform.DOScale(1f, 1f);
    }

    public void StartGameplay() //Quando clicar no bot�o de iniciar, _canRun ser� true para permitir o movimento e ir� iniciar a anima��o de RUN
    {
        _canRun = true;
        //Ativa o trigger da anima��o RUN, passando o valor igual a velocidade atual do player dividido pelo start speed para acelerar a anima��o
        animatorManager.Play(AnimatorManager.AnimationType.RUN);
    }
    private void PlayerMovement()
    {
        if (!_canRun) return;
        //Salva a posi��o do grafico do player.
        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        //Define os limites que o player pode andar no level para os lados.
        if (_pos.x < -limit) _pos.x = -limit;
        else if (_pos.x > limit) _pos.x = limit;
        

        //Pega a posi��o do objeto control�vel e ap�s o atraso do lerpSpeed, move o gr�fico do player para a dire��o do objeto.
        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        //Move o personagem para frente no cen�rio baseado na velocidade em _currentSpeed
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

   
    private void OnCollisionEnter(Collision collision)
    {
        if (invencible) return; //Se invencible for TRUE, n�o executa o c�digo abaixo.
        //Se colidir com inimigo, _fail ser� true
        if (collision.transform.tag == enemyTag)
        {
            _fail = true; //Se invencible for false, _fail ser� true
            animatorManager.Play(AnimatorManager.AnimationType.DEATH); //Ir� tocar a anima��o de morte
        }
        //Checagem de coliss�o com inimigos OU o fim de jogo; Se colidir, jogo encerra com a tela de falha.
        if (collision.transform.tag == enemyTag || collision.transform.tag == endGameTag)
        {
            _canRun = false;
            //pauseController.Pause();
            EndGame();
            if (_fail == false) animatorManager.Play(AnimatorManager.AnimationType.IDLE);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == levelSpawnerTag)
        {
            levelManager.CreateLevelPiecesBase();
        }
    }

    private void EndGame()
    {
        //Desativa o hud ingame para ativar o de fim de jogo ou de falha.
        inGameHud.SetActive(false);

        //Se _fail for true, ir� ativar a tela de falha
        if (_fail == true)
        {
            failScreen.SetActive(true);
        }
        //Caso contr�tio, ativar� a tela de fim de jogo
        else
        {
            endGameScreen.SetActive(true);
            if (endGameParticles != null) endGameParticles.Play();
        }
    }
    #region POWERUPS
    public void SetPowerUpText(string s) //V�riavel recebendo o texto de descri��o dos Power Ups
    { 
        TextPowerUp.text = s; 
    }
    public void PowerUpSpeedUp(float f) //Vari�vel boleana recebendo valor true ou false do script PowerUpSpeedUp
    { 
        _currentSpeed = f; 
    }
    public void ResetSpeed() //Retorna o speed a velocidade original.
    { 
        _currentSpeed = startSpeed; 
    }

    public void SetPowerUpInvencible(bool b) //Vari�vel boleana recebendo valor true ou false do script PowerUpInvencible
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
