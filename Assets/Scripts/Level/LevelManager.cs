using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{   
    public Transform container; //Objeto PAI onde esse script ir� spawnar os peda�os do level DENTRO

    [Header("Gerador de Levels Prontos")]
    public List<GameObject> levels; //�r� listar os levels PRONTOS - FACILITAR, CHAMO DE LISTA 1
    [SerializeField] private int _levelIndex; //Index para acessar as pe�as dos levels na lista 1 (inicia em 0)
    
    private GameObject _currentLevel; //Qual level foi spawnado

    //AS LISTAS GERADAS levelPieces e _spawnedPieces deriv�o do Script LevelPieceBase para que po�a ter acesso ao endPiece, que far� com que que spawne as pe�as subsequentes uma atr�s da outra no fim de cada uma delas.
    [Header("Gerador de Levels Randons")]
    public List<LevelPieceBase> levelPieces; //Ir� listar PEDA�OS de levels - PARA FACILITAR, CHAMO DE LISTA 2. 
    public int numberOfPiecesToSpawn = 5; //N�mero m�ximo de peda�os

    [SerializeField] private List<LevelPieceBase> _spawnedPieces; //Lista com os peda�os J� SPAWNADOS - PARA FACILITAR, CHAMO DE LISTA 3

    //No Awake j� iniciar� com o Spawn de um level.
    private void Awake()
    {
        // SpawnNextLevel(); -> C�digo comentando pois ser� utilizado o m�todo de randomiza��o.
        CreateLevelPiecesBase();
    }
    #region SPAWNA LEVELS PRONTOS
    //Fun��o que spawna os peda�os dos levels
    private void SpawnNextLevel()
    {
        //Se J� TIVER SPAWNADO um level, destr�i o atual e spawna o pr�ximo.
        if(_currentLevel != null)
        {
            Destroy(_currentLevel);
            _levelIndex++;

            //SE o INDEX for igual ou mais que a contagem total de levels, chama a fun��o ResetLevelIndex - LISTA 1
            if(_levelIndex >= levels.Count)
            {
                ResetLevelIndex();
            }
        }

        //_currentLevel ser� igual a uma instancia gerada. O objeto spawnado ser� referente ao  "index _level" da lista "Level" e ir� spawnar dentro do objeto "container" - LISTA 1
        _currentLevel = Instantiate(levels[_levelIndex], container);
        //Sempre ir� spawnar o level na posi��o 0,0,0.
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    //Fun��o ir� zerar a v�riavel
    public void ResetLevelIndex()
    {
        _levelIndex = 0;
    }
    #endregion

    #region SPAWNA LEVELS RANDOMIZADOS
    private void CreateLevelPiecesBase()
    {
        //Lista _spawnedPieces (LISTA 3) ser� igual a uma lista gerada a partir do la�o de repeti��o:
        //O la�o se encerra no m�ximo de piecesNumber decidido.
        //Ir� chamar SpawnLevelPiece a cada objeto adicionado na lista.

        _spawnedPieces = new List<LevelPieceBase>();

        for(int i = 0; i < numberOfPiecesToSpawn; i++) 
        {
            SpawnLevelPieceBase();
        }

    }

    private void SpawnLevelPieceBase()
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
