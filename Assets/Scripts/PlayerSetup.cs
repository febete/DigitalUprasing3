using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon.StructWrapping;
using UnityStandardAssets.Characters.ThirdPerson.PunDemos;
using StarterAssets;

public class PlayerSetup : MonoBehaviourPunCallbacks 
{
    [SerializeField]
    GameObject FPSCamera1,FPSCamera2;   
    [SerializeField]
    TextMeshProUGUI playerNameText;
    public SkinnedMeshRenderer mesh;
    
    // Start is called before the first frame update
    void Start()
    {
        if(photonView.IsMine)
        {
            transform.GetComponent<ThirdPersonController>().enabled = true;
            FPSCamera1.SetActive(true);
            FPSCamera2.SetActive(true);
            mesh.enabled = false;
          
        }
        else
        {
            transform.GetComponent<ThirdPersonController>().enabled = false;
            FPSCamera1.SetActive(false);
            FPSCamera2.SetActive(false);
            mesh.enabled = true;
          
        }


        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if(playerNameText != null)
        {
        playerNameText.text = photonView.Owner.NickName;
        }

    }
}
