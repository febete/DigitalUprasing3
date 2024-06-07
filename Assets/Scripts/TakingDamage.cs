using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    private float health;
    public float startHealth = 100;

    [SerializeField]
    Image healthBar;

    public TMP_Text score;


    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
        print("asd");
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        Debug.Log("Can Miktari: " + health);

        healthBar.fillAmount = health / startHealth;

        if (photonView.IsMine)
        {
            PixelGunGameManager.instance.hpText.text = health.ToString();
        }

        if (health <= 0f)
        {
            //Die
            Die();

        }
    }


    void Die()
    {

        if (photonView.OwnerActorNr == 1)
        {
            int score = int.Parse(PixelGunGameManager.instance.player1Score.text);
            score++;
            PixelGunGameManager.instance.player1Score.text = score.ToString();
        }

        else
        {
            int score = int.Parse(PixelGunGameManager.instance.player2Score.text);
            score++;
            PixelGunGameManager.instance.player2Score.text = score.ToString();
        }

        health = startHealth;
        healthBar.fillAmount = health / startHealth;

        if (photonView.IsMine)
        {
            float randomPointx = Random.Range(-38f, 30f);
            float randomPointz = Random.Range(-20f, 72f);
            transform.position = new Vector3(randomPointx, 5, randomPointz);
            PixelGunGameManager.instance.hpText.text = health.ToString();

        }
    }


}
