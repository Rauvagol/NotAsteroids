using UnityEngine;

public class BulletController : MonoBehaviour
{
    //how long for bullet to last in seconds
    private const int BulletDuration = 3;

    //runs once on start
    private void Start()
    {
        //applies 1200 units of force to the bullet
        GetComponent<Rigidbody2D>().AddForce(transform.up * 1200);

        //bullet destroys self after "BulletDuration" number of seconds
        Destroy(gameObject, BulletDuration);
    }
}