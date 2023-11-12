using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gipsy : MonoBehaviour
{
    [SerializeField]
    private int health = 10;

    void Start()
    {  
        Player_Health_Controller.initFirst(health);
    }

    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        Player_Health_Controller.damage(1);
    }

        private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy_Type"))
        {
            Player_Health_Controller.damage(1);
            //aSource.PlayOneShot(hitSound);
            //ps.Play();
        }
    }


}
