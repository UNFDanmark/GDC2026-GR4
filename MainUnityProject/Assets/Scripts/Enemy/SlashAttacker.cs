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
    }

    new void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
