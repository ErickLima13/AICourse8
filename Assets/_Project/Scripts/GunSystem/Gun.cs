using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;
    private float reloadTime = 1f;

    public int maxAmmo = 10;
    public int currentAmmo;

    public Camera fpsCam;

    public GameObject[] effects;

    public Animator animator;
    public Animator myAnimator;

    private bool isReloading;

    private void Initialization()
    {
        currentAmmo = maxAmmo;
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
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
        myAnimator.Play("ShootPistol");
        
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target.CompareTag("Player")) return;

            if(target != null)
            {
                target.TakeDamage(damage);
            }

            switch (hit.collider.tag)
            {
                case "Scenery":
                    GameObject effectGO = Instantiate(effects[0], hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effectGO, 2f);
                    break;
                case "Enemies":
                    GameObject effectGO2 = Instantiate(effects[1], hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effectGO2, 2f);
                    break;
            }
        }
    }
}
