using UnityEngine;

public class PlayerAniParameter
{
    public static int speed = Animator.StringToHash("speed");
    public static int isGrounded = Animator.StringToHash("isGrounded");
    public static int jump = Animator.StringToHash("jump");
    public static int attackTrigger = Animator.StringToHash("attack");
    public static int currentAttack = Animator.StringToHash("currentAttack");
    public static int impact = Animator.StringToHash("impact");
    public static int death = Animator.StringToHash("death");
}
