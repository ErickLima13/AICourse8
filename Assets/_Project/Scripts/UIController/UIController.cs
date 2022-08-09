using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;

    public Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }

    private void ShowUI()
    {
        currentAmmo.text = gun.currentAmmo.ToString();
        maxAmmo.text = gun.maxAmmo.ToString();
    }
}
