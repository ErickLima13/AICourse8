using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerPhysics playerPhysics;
    [SerializeField] private GeneralStatus status;
    [SerializeField] private Animator animator;

    public CharacterController controller;

    public Image damageScreen;

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

    public void ApllyDamage(int damage)
    {
        if (damageScreen.color.a == 1)
        {
            return;
        }

        animator.Play("Hit");
        currentLife -= damage;
        var tempColor = damageScreen.color;
        tempColor.a = status.maxLife / currentLife -1; // 50 (vida maxima) / 40 (vida atual) - 1 calculo do alpha
        damageScreen.color = tempColor;

        if (currentLife <= 0)
        {
            
        }
    }
}
