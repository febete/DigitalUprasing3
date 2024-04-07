using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{

    public static LaunchManager instance;

    #region Unity Methods

    //first method that is executed
    private void Awake() {
        DontDestroyOnLoad(gameObject);

        if(instance != null) Destroy (gameObject);
        else instance = this;



        PhotonNetwork.AutomaticallySyncScene = true;    
    }




    #endregion


    #region  Public Methods

    public void ConnectToPhotonServer()
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            CanvasManager.instance.ConnectionStatusPanel.SetActive(true);
            CanvasManager.instance.EnterGamePanel.SetActive(false);

        }
    }    


    public void joinRandomRoom()
    {
        //Photon will try to find a room to join if there is at least one room
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion






    #region Photon Callbacks

    //this method calls when we connected to  photon servers.
    public override void OnConnectedToMaster()
    {
       Debug.Log(PhotonNetwork.NickName + " Connected to Photon Server");
       CanvasManager.instance.LobbyPanel.SetActive(true);
       CanvasManager.instance.ConnectionStatusPanel.SetActive(false);
    }

    //this method will call when we have the internet connection or before onConnectedToMaster method
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet!");
    }

    //call this method if the user failed to join a random room
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        //who joines to which room, we will see
        Debug.Log(PhotonNetwork.NickName+ " joined to " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameScene");
    }

    //it is called when a remote player joins the room that we are in.
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName+ " joined to "+ PhotonNetwork.CurrentRoom.Name+ " "+ PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion




    #region Private methods

    void CreateAndJoinRoom()
    {
        string randomRoomName = "Room" + Random.Range(0,10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.CreateRoom(randomRoomName,roomOptions);
    }

    #endregion
}
