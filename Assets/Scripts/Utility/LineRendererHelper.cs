using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public List<Transform> positions; //Lista com objetos que servirão como indicativos de onde o line renderer deverá passar.

    private void Start()
    {
        lineRenderer.positionCount = positions.Count; //O tamanhanho da lista positions determina quantas "arestas" terá o line redenderer.

    }
    private void Update()
    {
        for (int i = 0; i < positions.Count;  i++)
        {
            lineRenderer.SetPosition(i, positions[i].position); //irá setar a posição do lineRenderer com a posição do objeto na lista positions.
        }
    }
}
