using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public Transform container; //Objeto PAI onde esse script irá spawnar os pedaços do level DENTRO

    [Header("Gerador de Levels Prontos")]
    public List<GameObject> levels; //Írá listar os levels PRONTOS - FACILITAR, CHAMO DE LISTA 1
    [SerializeField] private int _levelIndex; //Index para acessar as peças dos levels na lista 1 (inicia em 0)
    
    private GameObject _currentLevel; //Qual level foi spawnado

    //AS LISTAS GERADAS levelPieces e _spawnedPieces derivão do Script LevelPieceBase para que poça ter acesso ao endPiece, que fará com que que spawne as peças subsequentes uma atrás da outra no fim de cada uma delas.
    [Header("Gerador de Levels Randons")]
    public List<LevelPieceBase> levelPieces; //Irá listar PEDAÇOS de levels - PARA FACILITAR, CHAMO DE LISTA 2. 
    public int numberOfPiecesToSpawn = 5; //Número máximo de pedaços

    [SerializeField] private List<LevelPieceBase> _spawnedPieces; //Lista com os pedaços JÁ SPAWNADOS - PARA FACILITAR, CHAMO DE LISTA 3

    //No Awake já iniciará com o Spawn de um level.
    private void Awake()
    {
        // SpawnNextLevel(); -> Código comentando pois será utilizado o método de randomização.
        CreateLevelPiecesBase();
    }
    #region SPAWNA LEVELS PRONTOS
    //Função que spawna os pedaços dos levels
    private void SpawnNextLevel()
    {
        //Se JÁ TIVER SPAWNADO um level, destrói o atual e spawna o próximo.
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _levelIndex++;

            //SE o INDEX for igual ou mais que a contagem total de levels, chama a função ResetLevelIndex - LISTA 1
            if(_levelIndex >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        //_currentLevel será igual a uma instancia gerada. O objeto spawnado será referente ao  "index _level" da lista "Level" e irá spawnar dentro do objeto "container" - LISTA 1
        _currentLevel = Instantiate(levels[_levelIndex], container);
        //Sempre irá spawnar o level na posição 0,0,0.
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    //Função irá zerar a váriavel
    public void ResetLevelIndex()
    {
        _levelIndex = 0;
    }
    #endregion

    #region SPAWNA LEVELS RANDOMIZADOS
    private void CreateLevelPiecesBase()
    {
        //Lista _spawnedPieces (LISTA 3) será igual a uma lista gerada a partir do laço de repetição:
        //O laço se encerra no máximo de piecesNumber decidido.
        //Irá chamar SpawnLevelPiece a cada objeto adicionado na lista.

        _spawnedPieces = new List<LevelPieceBase>();

        for(int i = 0; i < numberOfPiecesToSpawn; i++) 
        {
            SpawnLevelPieceBase();
        }

    }

    private void SpawnLevelPieceBase()
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

        //Adiciona o objeto instanciado acima na lista _spawnedPieces (LISTA 3)
        _spawnedPieces.Add(spawnedPiece);
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
