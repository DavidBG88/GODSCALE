using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int x;
    public int y;
    public int width;
    public int height;

    private void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene");
            return;
        }

        RoomController.instance.RegisterRoom(this);
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(x * width, y * height);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
