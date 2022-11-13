using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for all bullets.
/// </summary>
public interface IBullets
{
    void SetDamage(int damage);
	int GetDamage();
    void Fire(Transform target);
}
