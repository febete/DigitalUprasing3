using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;  //we do this because by default there is no previous item. because we havent switched yet


    
    void Start()
    {
        if(photonView.IsMine)
        {
            EquipItem(0);
        }
    }

   
    void Update()
    {
        for (int i = 0; i < items.Length ; i++)
        {
            if(Input.GetKeyDown((i+1).ToString()))
            {
                if(!gameObject.GetComponent<PhotonView>().IsMine) return;
                
                gameObject.GetComponent<PhotonView>().RPC("EquipItem",RpcTarget.AllBuffered,i);
                //EquipItem(i);
                break;
            }
        }
    }


    [PunRPC]
    void EquipItem(int _index)
    {
        if(_index == previousItemIndex)   //aynı tuşa basıldığında değişmesin diye 
        {
            return;
        }
        itemIndex = _index;
        items[itemIndex].itemGameObject.SetActive(true);

        if(previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }

        previousItemIndex = itemIndex;
    }

}
