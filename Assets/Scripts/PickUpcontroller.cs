using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using StarterAssets;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;
public class PickUpcontroller : MonoBehaviourPunCallbacks
{
    //public ProjectileGun gunScript;
    public  Rigidbody rb;
    public BoxCollider coll;
    public Transform player, fpsCam,gunContainer;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;

    
    void Start()
    {
        //Setup
        if(!equipped)
        {
            //gunscript.enabled = false;
            rb.isKinematic =false;
            coll.isTrigger = false;
        }
        if(equipped)
        {
             //gunscript.enabled = true;
            rb.isKinematic =true;
            coll.isTrigger = true;
        }
        
    }

    private void OnTriggerStay(Collider other) {

        if(other.gameObject.tag != "Player" || !other.GetComponent<PhotonView>().IsMine  ) return;

        if(!equipped &&   player == null && Input.GetKeyDown(KeyCode.F) ) 
        {
          photonView.RPC("PickUp" , RpcTarget.AllBuffered , other.transform.GetComponent<PhotonView>().ViewID );
        }

    }

    private void Update() {

        if(player == null || !player.GetComponent<PhotonView>().IsMine) return;

        if(equipped && Input.GetKeyDown(KeyCode.G) && player != null )
        {
          photonView.RPC("Drop",RpcTarget.AllBuffered);
        }
        
    }


    [PunRPC]
    public void PickUp(int playerViewId)
    {
        
        player = PhotonView.Find(playerViewId).transform;
        gunContainer = player.GetComponent<ThirdPersonController>().gunContainer;

        equipped = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one; 

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        //enable script
        //gunscript.enabled = true;

        //player atama
        
    }

    [PunRPC]
    public void Drop()
    {
        equipped = false;

        //Set parent to null
        transform.SetParent(null);



        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum  of player
        rb.velocity = player.GetComponent<CharacterController>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);
        //AddRandom rotation
        float random = Random.Range(-1f,1f);
        rb.AddTorque(new Vector3(random, random,random)*10);


        //disable script
        //gunscript.enabled = false;

        //weaponun atalı olduğu player ı null etme
        player = null;

        
    }



}
