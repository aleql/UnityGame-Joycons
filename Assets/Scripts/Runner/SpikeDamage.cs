using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public int damage = 1;
    [SerializeField] private bool isWall;


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player")){
        if (collider.gameObject.TryGetComponent(out IDamagable pickable))
        {
                if (isWall)
                {
                    pickable.Hit(damage, collider.GetContact(0).normal);
                }
                else
                {
                    Vector2 upwardDirection = Vector2.up; // This is a unit vector pointing directly up
                    pickable.Hit(damage, upwardDirection);
                }
                // Health the object that collide with me
                //

            }
        }
    }
}
