using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IModifiable
{
    [SerializeField] private PlayerPhysics playerPhysics;
   
    [SerializeField] private Animator animator;

    public GeneralStatus status;

    public CharacterController controller;

    public int currentLife;

    private void Initialization()
    {
        currentLife = status.maxLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        playerPhysics.Move();
        playerPhysics.Jump();
    }

    public void HealthChange(int value)
    {
        if(currentLife == 0)
        {
            return;
        }

        animator.Play("Hit");
        currentLife -= value;

        if (currentLife <= 0)
        {

        }
    }
}
