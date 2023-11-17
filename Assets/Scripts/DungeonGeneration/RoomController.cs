using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class  RoomInfo
{
    public string name;

    public int x; 

    public int y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Prueba";

    RoomInfo currentLoadRoomData;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    private Room currentRoom;

    private void Awake()
    {
        instance = this;
    }
    
    public bool DoesRoomExists(int x, int y)
    {
        return loadedRooms.Find(room => room.x == x && room.x == y) != null;
    }

    public void LoadRoom(string roomName, int x, int y)
    {
        if (DoesRoomExists(x, y))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = roomName;
        newRoomData.x = x;
        newRoomData.y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string name = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExists(currentLoadRoomData.x, currentLoadRoomData.y))
        {
            room.transform.position = new Vector3(currentLoadRoomData.x * room.width,
            currentLoadRoomData.y * room.height, 0);

            room.x = currentLoadRoomData.x;
            room.y = currentLoadRoomData.y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + ", " + room.y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currentRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
    }

    void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }
        else if(loadRoomQueue.Count == 0)
        {
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.SetCurrentRoom(room);
        currentRoom = room;
    }
}
