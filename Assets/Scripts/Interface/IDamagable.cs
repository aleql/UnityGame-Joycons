using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Hit(int damage);
    void Hit(int damage, Vector2 position);
}
