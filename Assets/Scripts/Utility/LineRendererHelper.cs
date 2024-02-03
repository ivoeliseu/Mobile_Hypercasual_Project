using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> positions; //Lista com objetos que servir�o como indicativos de onde o line renderer dever� passar.

    private void Start()
    {
        lineRenderer.positionCount = positions.Count; //O tamanhanho da lista positions determina quantas "arestas" ter� o line redenderer.

    }
    private void Update()
    {
        for (int i = 0; i < positions.Count;  i++)
        {
            lineRenderer.SetPosition(i, positions[i].position); //ir� setar a posi��o do lineRenderer com a posi��o do objeto na lista positions.
        }
    }
}
