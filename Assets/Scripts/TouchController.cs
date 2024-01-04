using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastPosition; //Salva a �ltima posi��o do mouse.
    public float velocity = 1;  //Determina a velocidade que o objeto se move.

    private void Update()
    {
        //Quando detectar o click do mouse
        if (Input.GetMouseButton(0))
        {
            //mousePosition agora - mousePosition passado
            Move(Input.mousePosition.x - pastPosition.x);
        }

        //Salva a �ltima posi��o
        pastPosition = Input.mousePosition;
    }

    //Fun��o que faz o objeto se mover se mover.
    public void Move(float speed)
    {
        transform.position += Vector3.right * Time.deltaTime * speed * velocity;
    }
}
