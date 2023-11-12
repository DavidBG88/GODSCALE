using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health_Controller : MonoBehaviour
{
    static int health;
    static int character_health;

    public static void initFirst(int h)
    {
        character_health = h;
        init();
    }

    public static void init()
    {
        health = character_health;
    }

    public static void damage(int quantity)
    {
        int temp_health = health - quantity;

        if(temp_health > 0) health = temp_health;
        else health = 0;
    }

    public static void heal(int quantity, int max_health)
    {
        int temp_health = health + quantity;

        if(temp_health < max_health) health = temp_health;
        else health = max_health;
    }

    public static bool dead()
    {
        return health == 0;
    }
}
