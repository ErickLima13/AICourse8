using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage;

    public float range;
    public float fireRate;

    private float nextTimeToFire = 0f;
    private float reloadTime;

    public int maxAmmo;
    public int currentAmmo;

    public AttributesWeapons attributesWeapons;

    public Camera fpsCam;

    public GameObject[] effects;

    public Animator handAnimator;
    public Animator myAnimator;

    private bool isReloading;

    private void Initialization()
    {
        maxAmmo = attributesWeapons.maxAmmo;
        damage = attributesWeapons.damage;
        range = attributesWeapons.range;
        fireRate = attributesWeapons.fireRate;
        reloadTime = attributesWeapons.reloadTime;

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

        if (currentAmmo <= 0 || Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        handAnimator.SetBool("Reload", isReloading);

        yield return new WaitForSeconds(reloadTime);

        isReloading = false;

        handAnimator.SetBool("Reload", isReloading);
       
        currentAmmo = maxAmmo;
    }

    private void Shoot()
    {
        myAnimator.SetTrigger("Shoot");

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            if(hit.collider.TryGetComponent(out Player player))
            {
                return;
            }

            if (hit.collider.TryGetComponent(out IModifiable modifiable))
            {
                modifiable.HealthChange(damage);
                GameObject effectGO2 = Instantiate(effects[1], hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(effectGO2, 2f);
            }
            else
            {
                GameObject effectGO = Instantiate(effects[0], hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(effectGO, 2f);
            }
        }
    }
}
