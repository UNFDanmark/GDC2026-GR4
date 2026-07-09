using Unity.VisualScripting;
using UnityEngine;

public class SlashAttacker : Attacker
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
        Attack();
    }

    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Attack()
    {
        if (enemy.dead) return;
        if (OnCooldown()) return;
        if (!targetVisible) return;
        
        SetCooldown();
        enemy.sound.PlayOneShot(enemy.attackSound);
        if (target == p.transform)
        {
            GameManager.instance.damage(attackDamage);
        }
    }
}
