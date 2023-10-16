using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecovery : Resource
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.RecoverHealth();
        }
    }
}
