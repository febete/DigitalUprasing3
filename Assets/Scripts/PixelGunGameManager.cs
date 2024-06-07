using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject playerPrefab;

    public static PixelGunGameManager instance;    //singleton design pattern
    public TMP_Text playerName1, playerName2;
    public TMP_Text player1Score, player2Score;

    bool isSettingScreenOpen;
    public GameObject settingScreen;

    public TMP_Text hpText, bulletText;




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AutomaticallySyncScene = true;



            if (playerPrefab != null)
            {
                float randomPointx = Random.Range(-5f, 5f);
                float randomPointz = Random.Range(0f, -10f);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPointx, 5, randomPointz), Quaternion.identity);
                Debug.Log("*-*-Instantiate");
            }

        }


        int i = 0;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (i == 0) playerName1.text = player.NickName;
            if (i == 1) playerName2.text = PhotonNetwork.CurrentRoom.Players[2].NickName;
            i++;
        }

        PlayerProperty.instance.playerLevel += 0.5f;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettingScreenOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                settingScreen.gameObject.SetActive(false);
                isSettingScreenOpen = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                settingScreen.gameObject.SetActive(true);
                isSettingScreenOpen = true;


            }
        }
    }


    public void CloseSettingScreen()
    {
        Cursor.lockState = CursorLockMode.Locked;
        settingScreen.gameObject.SetActive(false);
        isSettingScreenOpen = false;

    }






    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to --" + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to --" + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }



    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("LaunchScene");
    }


    public void LeaveRoom()
    {
        PlayerProperty.instance.SaveData();
        PhotonNetwork.LeaveRoom();
    }

    public void QuitMatch()
    {
        Application.Quit();
    }



    private void OnApplicationQuit()
    {
        PlayerProperty.instance.SaveData();
    }
}
