using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon.StructWrapping;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;
    public float fireRate = 0.1f;
    float fireTimer;
    public float amountOfDamage = 10f;

    public int totalBulletCount = 60;
    public int inMagazineBulletCount = 30;

    public LayerMask layer;
    bool isReloading;

    public int remainBullet;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && inMagazineBulletCount < 30)
        {
            StartCoroutine(ReloadMagazine());
            print("basıldı");
        }


        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer > fireRate && inMagazineBulletCount > 0 && !isReloading)
        {

            //Reset fireTimer 
            inMagazineBulletCount--;
            fireTimer = 0.0f;

            RaycastHit _hit;
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            if (Physics.Raycast(ray, out _hit, 100, layer))
            {
                Debug.Log(_hit.collider.gameObject.name);

                if (_hit.collider.gameObject.CompareTag("Player") && !_hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    _hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, amountOfDamage); // this will sent the RPC everyone in the room, 3. parametreyi gösterir
                }

            }

        }

        PixelGunGameManager.instance.bulletText.text = inMagazineBulletCount.ToString() + "/" + totalBulletCount.ToString();
    }




    public IEnumerator ReloadMagazine()
    {
        isReloading = true;
        yield return new WaitForSeconds(3);
        remainBullet = 30 - inMagazineBulletCount;
        print(remainBullet);
        remainBullet = Mathf.Min(remainBullet, totalBulletCount);
        print(remainBullet);
        inMagazineBulletCount += remainBullet;
        totalBulletCount -= remainBullet;
        isReloading = false;

    }
}
