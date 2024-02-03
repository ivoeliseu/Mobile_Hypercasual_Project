using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.PlayerSettings;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition; //Salva a última posição do mouse.
    public float velocity = 1;  //Determina a velocidade que o objeto se move.

    private Vector3 _pos;
    public float limit = 4f;

    private void Update()
    {

        


        //Quando detectar o click do mouse
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - pastPosition.x); //Move o objeto para o lugar clicado
        }

        pastPosition = Input.mousePosition; //Salva a última posição
    }

    //Função que faz o objeto se mover se mover.
    public void Move(float speed)
    {
        transform.position += Vector3.right * Time.deltaTime * speed * velocity;
    }
}
