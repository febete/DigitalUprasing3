using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    private float health;
    public float startHealth = 100;

    [SerializeField]
    Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health/startHealth;
        print("asd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void TakeDamage( float _damage)
    {
        health -= _damage;
        Debug.Log("Can Miktari: " + health);

        healthBar.fillAmount = health / startHealth;

        if(health <= 0f)
        {
            //Die
            Die();

        }
    }


    void Die()
    {
        if(photonView.IsMine)
        {
            PixelGunGameManager.instance.LeaveRoom();
        }
    }


}
