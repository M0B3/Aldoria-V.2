
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("isWalking", playerAnimator.GetBool("isWalking"));
        animator.SetBool("IsRunning", playerAnimator.GetBool("IsRunning"));
        animator.SetBool("IsShooting", playerAnimator.GetBool("IsShooting"));
    }
}
