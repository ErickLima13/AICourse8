using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : MonoBehaviour, IModifiable
{
    public Player player;

    public void HealthChange(int value)
    {
        if(player.currentLife == player.status.maxLife)
        {
            return;
        }

        //animator.Play("Hit");
        player.currentLife += value;

       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
