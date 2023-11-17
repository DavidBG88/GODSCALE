using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;

    public Room currentRoom;

    private float moveSpeedWhenRoomChange = 300;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void SetCurrentRoom(Room room)
    {
        this.currentRoom = room;
    }

    public Room GetCurrentRoom()
    {  
        return this.currentRoom;
    }

    void UpdatePosition()
    {
        if(this.currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeedWhenRoomChange);
    }

    Vector3 GetCameraTargetPosition()
    {
        if(this.currentRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = currentRoom.GetRoomCenter();
        targetPosition.z = transform.position.z;

        return targetPosition;
    }

    public bool isSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }
}
