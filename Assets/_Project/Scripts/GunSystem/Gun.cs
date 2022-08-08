using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 10;

    public Camera fpsCam;
   
    public ParticleSystem muzzleFlash;

    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private float reloadTime = 1f;

    public int currentAmmo;

    private bool isReloading;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if(currentAmmo <= 0 || Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") &&  Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10, Color.blue);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reload", isReloading);
        print("Reloading...");

        yield return new WaitForSeconds(reloadTime);
        

        isReloading = false;

        animator.SetBool("Reload", isReloading);
       
        currentAmmo = maxAmmo;
    }

    private void Shoot()
    {
        //muzzleFlash.Play();

        GetComponent<Animator>().Play("ShootPistol");
        
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);


        }


    }
}
