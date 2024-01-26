using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{   
    public Transform container; //Objeto PAI onde esse script irá spawnar os pedaços do level DENTRO

    [Header("Gerador de Levels Prontos")]
    public List<GameObject> levels; //Írá listar os levels PRONTOS - FACILITAR, CHAMO DE LISTA 1
    [SerializeField] private int _levelIndex; //Index para acessar as peças dos levels na lista 1 (inicia em 0)
    
    private GameObject _currentLevel; //Qual level foi spawnado
        
    [Header("Gerador de Levels Randons")] //AS LISTAS GERADAS levelPieces e _spawnedPieces derivão do Script LevelPieceBase para que possa ter acesso ao endPiece, que fará com que que spawne as peças subsequentes uma atrás da outra no fim de cada uma delas.
    public List<LevelPieceBase> levelPieces; //Irá listar PEDAÇOS de levels - PARA FACILITAR, CHAMO DE LISTA 2. 
    public int numberOfPiecesToSpawn = 5; //Número máximo de pedaços

    [SerializeField] private List<LevelPieceBase> _spawnedPieces; //Lista com os pedaços JÁ SPAWNADOS - PARA FACILITAR, CHAMO DE LISTA 3

    [Header("Animation")]
    public float scaleDuration = .2f; //Tempo que Irá durar pro objeto sair de escala 0 para 1
    public float scaleTimeBetweenPieces = 1f; //Tempo entre as peças surgirem
    public Ease ease; //Estilo da animação

    [Header("Setup")]
    public ColorManager.Setups setup;

    /*
    private void Awake() //No Awake já iniciará com o Spawn de um level.
    {
        // CreateLevelPiecesBase(); -> Será chamado agora pela colisão com o trigger no script PlayerController.
        // SpawnNextLevel(); -> Código comentando pois será utilizado o método de randomização.
    }
    */

    #region SPAWNA LEVELS PRONTOS
    private void SpawnNextLevel() //Função que spawna os pedaços dos levels
    {
        if(_currentLevel != null) 
        {
            Destroy(_currentLevel); //Se JÁ TIVER SPAWNADO um level, destrói o atual e spawna o próximo.
            _levelIndex++;

            
            if(_levelIndex >= levels.Count) //SE o INDEX for igual ou mais que a contagem total de levels, chama a função ResetLevelIndex - LISTA 1
            {
                ResetLevelIndex();
            }
        }
        
        _currentLevel = Instantiate(levels[_levelIndex], container); //_currentLevel será igual a uma instancia gerada. O objeto spawnado será referente ao  "index _level" da lista "Level" e irá spawnar dentro do objeto "container" - LISTA 1
        
        _currentLevel.transform.localPosition = Vector3.zero; //Sempre irá spawnar o level na posição 0,0,0.
    }

    public void ResetLevelIndex() //Função irá zerar a váriavel
    {
        _levelIndex = 0;
    }
    #endregion

    #region SPAWNA LEVELS RANDOMIZADOS
    public void CreateLevelPiecesBase()
    {
        //Lista _spawnedPieces (LISTA 3) será igual a uma lista gerada a partir do laço de repetição:
        //O laço se encerra no máximo de piecesNumber decidido.
        //Irá chamar SpawnLevelPiece a cada objeto adicionado na lista.

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
        //Variável piece é igual a um dos index entre 0 e número máximo da lista com os pedaços (LISTA 2).
        //Gera o objeto igual ao index atual da lista na localização de container.
        var piece = levelPieces[Random.Range(0, levelPieces.Count)];
        var spawnedPiece = Instantiate(piece, container);

        //Se o máximo de peças na lista _spawenedPieces (LISTA 3) for maior que 0:
        //A váriavel lastPiece irá diminuir a contagem da lista
        //A váriavel spawnedPiece irá colocar o objeto instanciado na posição de endPiece (que cada pedaço de level contém) para que um fique subsequente após o outro.
        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }

        //Irá aplicar as cores do Color manager no 
        var levelPieceBase = spawnedPiece.GetComponent<LevelPieceBase>(); //Vai ter o acesso ao visualElements
        ColorManager.Instance.ApplyColor(setup, levelPieceBase.visualElements); //Irá encaminhar o setup para aplicar a configuração da cor no ColorManager

        _spawnedPieces.Add(spawnedPiece); //Adiciona o objeto instanciado acima na lista _spawnedPieces (LISTA 3)


    }

    IEnumerator ScalePiecesByTime() //Corrotina que irá escalar o tamanho das peças de 0 para 1
    {
        foreach(var piece in _spawnedPieces) //Para cada variável na lista "_spawnedPieces" (lista 3), o transform fica com tamanho zero.
        {
            piece.transform.localScale = Vector3.zero;
        }

        yield return null; //Retorna o valor nulo para a corrotina

        for (int index = 0;index < _spawnedPieces.Count; index++) //váriavel index 0, enquanto for menor que a contagem de itens da lista "_spawnedPieces", faz o processo, index +1(lista 3)
        {
            //Ferramenta DOTWEEN, pegará o transform do item da lista _spawnedPieces no index "index" e escalará o transform dele para o tamanho 1, em "scaleDuration" segundos
            //O tipo de animação é definido pela variável SetEase
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
