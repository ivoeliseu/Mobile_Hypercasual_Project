using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{   
    public Transform container; //Objeto PAI onde esse script ir� spawnar os peda�os do level DENTRO

    [Header("Gerador de Levels Prontos")]
    public List<GameObject> levels; //�r� listar os levels PRONTOS - FACILITAR, CHAMO DE LISTA 1
    [SerializeField] private int _levelIndex; //Index para acessar as pe�as dos levels na lista 1 (inicia em 0)
    
    private GameObject _currentLevel; //Qual level foi spawnado
        
    [Header("Gerador de Levels Randons")] //AS LISTAS GERADAS levelPieces e _spawnedPieces deriv�o do Script LevelPieceBase para que possa ter acesso ao endPiece, que far� com que que spawne as pe�as subsequentes uma atr�s da outra no fim de cada uma delas.
    public List<LevelPieceBase> levelPieces; //Ir� listar PEDA�OS de levels - PARA FACILITAR, CHAMO DE LISTA 2. 
    public int numberOfPiecesToSpawn = 5; //N�mero m�ximo de peda�os

    [SerializeField] private List<LevelPieceBase> _spawnedPieces; //Lista com os peda�os J� SPAWNADOS - PARA FACILITAR, CHAMO DE LISTA 3

    [Header("Animation")]
    public float scaleDuration = .2f; //Tempo que Ir� durar pro objeto sair de escala 0 para 1
    public float scaleTimeBetweenPieces = 1f; //Tempo entre as pe�as surgirem
    public Ease ease; //Estilo da anima��o

    [Header("Setup")]
    public ColorManager.Setups setup;

    /*
    private void Awake() //No Awake j� iniciar� com o Spawn de um level.
    {
        // CreateLevelPiecesBase(); -> Ser� chamado agora pela colis�o com o trigger no script PlayerController.
        // SpawnNextLevel(); -> C�digo comentando pois ser� utilizado o m�todo de randomiza��o.
    }
    */

    #region SPAWNA LEVELS PRONTOS
    private void SpawnNextLevel() //Fun��o que spawna os peda�os dos levels
    {
        if(_currentLevel != null) 
        {
            Destroy(_currentLevel); //Se J� TIVER SPAWNADO um level, destr�i o atual e spawna o pr�ximo.
            _levelIndex++;

            
            if(_levelIndex >= levels.Count) //SE o INDEX for igual ou mais que a contagem total de levels, chama a fun��o ResetLevelIndex - LISTA 1
            {
                ResetLevelIndex();
            }
        }
        
        _currentLevel = Instantiate(levels[_levelIndex], container); //_currentLevel ser� igual a uma instancia gerada. O objeto spawnado ser� referente ao  "index _level" da lista "Level" e ir� spawnar dentro do objeto "container" - LISTA 1
        
        _currentLevel.transform.localPosition = Vector3.zero; //Sempre ir� spawnar o level na posi��o 0,0,0.
    }

    public void ResetLevelIndex() //Fun��o ir� zerar a v�riavel
    {
        _levelIndex = 0;
    }
    #endregion

    #region SPAWNA LEVELS RANDOMIZADOS
    public void CreateLevelPiecesBase()
    {
        //Lista _spawnedPieces (LISTA 3) ser� igual a uma lista gerada a partir do la�o de repeti��o:
        //O la�o se encerra no m�ximo de piecesNumber decidido.
        //Ir� chamar SpawnLevelPiece a cada objeto adicionado na lista.

        _spawnedPieces = new List<LevelPieceBase>();

        CatchColorSetup();

        for(int i = 0; i < numberOfPiecesToSpawn; i++) 
        {
            SpawnLevelPieceBase(setup);
        }
        
        StartCoroutine(ScalePiecesByTime());
    }

    private void CatchColorSetup()
    {
        var values = Enum.GetValues(typeof(ColorManager.Setups));
        var randomValue = (ColorManager.Setups)values.GetValue(Random.Range(0, values.Length));
        setup = randomValue;
    }

    private void SpawnLevelPieceBase(ColorManager.Setups setup)
    {
        //Vari�vel piece � igual a um dos index entre 0 e n�mero m�ximo da lista com os peda�os (LISTA 2).
        //Gera o objeto igual ao index atual da lista na localiza��o de container.
        var piece = levelPieces[Random.Range(0, levelPieces.Count)];
        var spawnedPiece = Instantiate(piece, container);

        //Se o m�ximo de pe�as na lista _spawenedPieces (LISTA 3) for maior que 0:
        //A v�riavel lastPiece ir� diminuir a contagem da lista
        //A v�riavel spawnedPiece ir� colocar o objeto instanciado na posi��o de endPiece (que cada peda�o de level cont�m) para que um fique subsequente ap�s o outro.
        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        //Ir� aplicar as cores do Color manager no 
        var levelPieceBase = spawnedPiece.GetComponent<LevelPieceBase>(); //Vai ter o acesso ao visualElements
        ColorManager.Instance.ApplyColor(setup, levelPieceBase.visualElements); //Ir� encaminhar o setup para aplicar a configura��o da cor no ColorManager

        _spawnedPieces.Add(spawnedPiece); //Adiciona o objeto instanciado acima na lista _spawnedPieces (LISTA 3)


    }

    IEnumerator ScalePiecesByTime() //Corrotina que ir� escalar o tamanho das pe�as de 0 para 1
    {
        foreach(var piece in _spawnedPieces) //Para cada vari�vel na lista "_spawnedPieces" (lista 3), o transform fica com tamanho zero.
        {
            piece.transform.localScale = Vector3.zero;
        }

        yield return null; //Retorna o valor nulo para a corrotina

        for (int index = 0;index < _spawnedPieces.Count; index++) //v�riavel index 0, enquanto for menor que a contagem de itens da lista "_spawnedPieces", faz o processo, index +1(lista 3)
        {
            //Ferramenta DOTWEEN, pegar� o transform do item da lista _spawnedPieces no index "index" e escalar� o transform dele para o tamanho 1, em "scaleDuration" segundos
            //O tipo de anima��o � definido pela vari�vel SetEase
            _spawnedPieces[index].transform.DOScale(1, scaleDuration).SetEase(ease);

            yield return new WaitForSeconds(scaleTimeBetweenPieces); //Retorna para a corrotina esperar por "scaleTimeBetweenPieces" segundo
        }

        CoinsAnimations.Instance.StartAnimations();
    }

    //Serve para debug.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnNextLevel();
        } 
        else if (Input.GetKeyDown(KeyCode.S))
        {
            CreateLevelPiecesBase();
        }
    }
    #endregion
}
