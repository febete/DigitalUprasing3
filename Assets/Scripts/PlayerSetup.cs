using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using ExitGames.Client.Photon.StructWrapping;
using UnityStandardAssets.Characters.ThirdPerson.PunDemos;
using StarterAssets;
using Cinemachine;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject FPSCamera1, FPSCamera2;
    [SerializeField]
    TextMeshProUGUI playerNameText;
    public SkinnedMeshRenderer mesh1, mesh2;
    public GameObject HiddenWeapon;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<ThirdPersonController>().enabled = true;
            FPSCamera1.SetActive(true);
            FPSCamera2.SetActive(true);
            mesh1.enabled = false;
            mesh2.enabled = false;
            HiddenWeapon.SetActive(false);
            gameObject.layer = 6;

        }
        else
        {
            transform.GetComponent<ThirdPersonController>().enabled = false;
            FPSCamera1.SetActive(false);
            FPSCamera2.SetActive(false);
            mesh1.enabled = true;
            mesh2.enabled = true;
            HiddenWeapon.SetActive(true);
            gameObject.layer = 8;

        }


        SetPlayerUI();
    }

    void SetPlayerUI()
    {
        if (playerNameText != null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }

    }
}
