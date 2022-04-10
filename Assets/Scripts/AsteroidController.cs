using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    //used for spawning medium from large, and small from medium
    public GameObject MediumAsteroid;
    public GameObject SmallAsteroid;

    //for accessing the game controller object
    private GameController _gameController;

    //run once on start
    private void Start(){
        
        //uses tag to find the game controller and associate it with the variable GameController
        var gameControllerObject = GameObject.FindWithTag("GameController");
        _gameController = gameControllerObject.GetComponent<GameController>();

        // Push the asteroid in the direction it is facing with a force between 200 and 300
        GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(200, 300));

        // Give a random angular velocity/rotation
        GetComponent<Rigidbody2D>()
            .angularVelocity = Random.Range(0, 360);
    }

    //check when one object collides with another
    private void OnCollisionEnter2D(Collision2D c){
        
        //if the asteroid collides with something that isn't a bullet, don't bother continuing the method 
        if (!c.gameObject.CompareTag("Bullet")) return;

        //immediately destroy the asteroid responsible
        Destroy(gameObject);
        //destroy the impacted bullet
        Destroy(c.gameObject);

        //large asteroid splits into 3 medium ones, going off in different directions
        if (tag.Equals("Large Asteroid"))
        {
            
            //medium asteroid the first
            Instantiate(MediumAsteroid,
                //pushes asteroids away from the center of large asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 60));

            //medium asteroid junior
            Instantiate(MediumAsteroid,
                //pushes asteroids away from the center of large asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 180));

            //medium asteroid III
            Instantiate(MediumAsteroid,
                //pushes asteroids away from the center of large asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 300));
            
            //tells the almighty game controller that an asteroid was split
            _gameController.SplitAsteroid();
            //a large asteroid is worth 3 points
            _gameController.IncrementScore(3);
        }
        
        //medium asteroid splits into 3 small ones, going off in different directions
        else if (tag.Equals("Medium Asteroid"))
        {
            //small asteroid number one
            Instantiate(SmallAsteroid,
                //pushes asteroids away from the center of medium asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 0));

            //small asteroid b
            Instantiate(SmallAsteroid,
                //pushes asteroids away from the center of medium asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 120));

            //small asteroid three
            Instantiate(SmallAsteroid,
                //pushes asteroids away from the center of medium asteroid, to stop weird as hell collisions
                new Vector3(transform.position.x + .5f, transform.position.y),
                //rotates the asteroid velocity vector by an amount
                Quaternion.Euler(0, 0, 240));
            
            //tells the almighty game controller that an asteroid was split
            _gameController.SplitAsteroid(); // +2
            //a medium asteroid is worth 2 points
            _gameController.IncrementScore(2);
        }
        else
        {
            //tells the almighty game controller that an asteroid was destroyed
            _gameController.DecrementAsteroids();
            //a small asteroid is worth 1 point
            _gameController.IncrementScore(2);
        }

    }
}