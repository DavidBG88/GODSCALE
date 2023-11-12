using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Canvas GameOver;
    private bool reiniciar = false;

    void Update()
    {
        if(Player_Health_Controller.dead() && !reiniciar)
        {
            Instantiate(GameOver, transform.position, transform.rotation);
            reiniciar = true;
        }

        if(reiniciar && Input.GetButtonDown("Jump"))
        {
            Player_Health_Controller.init();
            reiniciar = false;
            //Instanciar zona de transición o lobby
        }
        
    }

}
