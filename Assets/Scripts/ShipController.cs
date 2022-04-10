using UnityEngine;

public class ShipController : MonoBehaviour
{
    //how fast the ship turns and accelerates
    private const int RotationSpeed = 150;
    private const int ThrustForce = 12;

    //invulnerability state after getting hit/dying
    private bool _invulnerabilityActive;
    private const int InvulnerabilityDuration = 5;
    private double _invulnerabilityStartTime;
    private SpriteRenderer _playerColor;

    //gives access to the bullet object
    public GameObject Bullet;

    //gives access to the game controller object
    private GameController _gameController;

    //runs one on startup
    private void Start()
    {
        //uses tag to assign the game controller to the appropriate variable
        var gameControllerObject = GameObject.FindWithTag("GameController");
        _gameController = gameControllerObject.GetComponent<GameController>();
        _playerColor = GetComponent<SpriteRenderer>();
    }

    //checks based on physics frame calculations or something?
    private void FixedUpdate()
    {
        //rotate the ship based on the input (a or d) being held down, negative is to flip it because of the way unity is
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * RotationSpeed * Time.deltaTime);

        //accelerate the ship based on the input (w or s) being held down
        GetComponent<Rigidbody2D>().AddForce(transform.up * ThrustForce * Input.GetAxis("Vertical"));
    }

    //check every frame
    private void Update()
    {
        //when resetting, cancel invulnerability
        if (Input.GetKey("r"))
        {
            SetInvulnerability(false);
        }

        //shoot bullet on each space bar press
        if (Input.GetKeyDown("space"))
        {
            //if the player is invulnerable, they cannot shoot
            if (_invulnerabilityActive) return;
            ShootBullet();
        }

        //if player is invincible, sets them to blue
        _playerColor.color = _invulnerabilityActive ? Color.blue : Color.white;
        //if the time has run out on invincibility, remove it
        if (_invulnerabilityStartTime + InvulnerabilityDuration < Time.time)
        {
            SetInvulnerability(false);
        }
    }

    private void SetInvulnerability(bool input)
    {
        _invulnerabilityActive = input;
    }

    //when something collides with the ship
    private void OnTriggerEnter2D(Collider2D c)
    {
        //if it is a bullet, ignore it, because if it isn't a bullet, it's an asteroid
        if (c.gameObject.CompareTag("Bullet")) return;
        //if the player is invulnerable, ignores the collision
        if (_invulnerabilityActive) return;

        InvulnerabilityStart();

        //set ships position to the center
        transform.position = new Vector3(0, 0, 0);

        // stops the ship moving and turning
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);

        //removes one life
        _gameController.DecrementLives();
    }

    private void InvulnerabilityStart()
    {
        SetInvulnerability(true);
        _invulnerabilityStartTime = Time.time;
    }

    //self explanatory method name
    private void ShootBullet()
    {
        //creates a bullet at the ship origin, pointing in the same direction as the ship
        Instantiate(Bullet,
            new Vector3(transform.position.x, transform.position.y), transform.rotation);
    }
}