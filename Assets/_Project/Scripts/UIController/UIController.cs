using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Player player;

    public TextMeshProUGUI currentAmmo;
    public TextMeshProUGUI maxAmmo;
    public TextMeshProUGUI realTime;

    public Image damageScreen;

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
      

        //currentAmmo.text = gun.currentAmmo.ToString();
        //maxAmmo.text = gun.maxAmmo.ToString();

        realTime.text = System.DateTime.Now.ToString("yy/MM/dd  HH:mm:ss");

        var tempColor = damageScreen.color;
        tempColor.a = (float) player.status.maxLife / player.currentLife - 1; // 50 (vida maxima) / 40 (vida atual) - 1 calculo do alpha
        damageScreen.color = tempColor;

    }
}
