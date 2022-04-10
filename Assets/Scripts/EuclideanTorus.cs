using UnityEngine;

public class EuclideanTorus : MonoBehaviour
{
    //x and y dimensions of the screen size from center in unity units, the ratio of screen height:width should be the same as the ratio of screenHeight:screenWidth
    private const int ScreenHeight = 18;
    private const int ScreenWidth = 32;

    //radii of the asteroids, large is 2x med which is 2x small
    private const int LargeAsteroidRadius = 4;
    private const int MediumAsteroidRadius = 2;
    private const int SmallAsteroidRadius = 1;

    //runs every frame each frame
    private void Update()
    {
        //switch based on the tag assigned to each object
        switch (gameObject.tag)
        {
            //ship wraps as soon as origin passes, less aesthetically pleasing than waiting for it to fully leave screen, but much more playable
            case "Ship":

                //if the x coordinate of the origin is offscreen, it is set to just onscreen on the other side
                if (transform.position.x > ScreenWidth)
                {
                    transform.position = new Vector3(-ScreenWidth, transform.position.y);
                }
                else if (transform.position.x < -ScreenWidth)
                {
                    transform.position = new Vector3(ScreenWidth, transform.position.y);
                }
                //if the y coordinate of the origin is offscreen, it is set to just onscreen on the other side
                else if (transform.position.y > ScreenHeight)
                {
                    transform.position = new Vector3(transform.position.x, -ScreenHeight);
                }
                else if (transform.position.y < -ScreenHeight)
                {
                    transform.position = new Vector3(transform.position.x, ScreenHeight);
                }

                break;

            case "Large Asteroid":

                //if the x coordinate of the origin is offscreen, it is set to just onscreen on the other side
                if (transform.position.x > ScreenWidth + LargeAsteroidRadius)
                {
                    transform.position = new Vector3(-(ScreenWidth + LargeAsteroidRadius), transform.position.y);
                }
                else if (transform.position.x < -(ScreenWidth + LargeAsteroidRadius))
                {
                    transform.position = new Vector3(ScreenWidth + LargeAsteroidRadius, transform.position.y);
                }
                //if the y coordinate of the origin is offscreen, it is set to just onscreen on the other side
                else if (transform.position.y > ScreenHeight + LargeAsteroidRadius)

                {
                    transform.position = new Vector3(transform.position.x, -(ScreenHeight + LargeAsteroidRadius));
                }
                else if (transform.position.y < -(ScreenHeight + LargeAsteroidRadius))
                {
                    transform.position = new Vector3(transform.position.x, (ScreenHeight + LargeAsteroidRadius));
                }

                break;

            case "Medium Asteroid":

                //if the x coordinate of the origin is offscreen, it is set to just onscreen on the other side
                if (transform.position.x > ScreenWidth + MediumAsteroidRadius)
                {
                    transform.position = new Vector3(-(ScreenWidth + MediumAsteroidRadius), transform.position.y);
                }
                else if (transform.position.x < -(ScreenWidth + MediumAsteroidRadius))
                {
                    transform.position = new Vector3(ScreenWidth + MediumAsteroidRadius, transform.position.y);
                }
                //if the y coordinate of the origin is offscreen, it is set to just onscreen on the other side
                else if (transform.position.y > ScreenHeight + MediumAsteroidRadius)
                {
                    transform.position = new Vector3(transform.position.x, -(ScreenHeight + MediumAsteroidRadius));
                }
                else if (transform.position.y < -(ScreenHeight + MediumAsteroidRadius))
                {
                    transform.position = new Vector3(transform.position.x, ScreenHeight + MediumAsteroidRadius);
                }

                break;

            case "Small Asteroid":
                //if the x coordinate of the origin is offscreen, it is set to just onscreen on the other side
                if (transform.position.x > ScreenWidth + SmallAsteroidRadius)
                {
                    transform.position = new Vector3(-(ScreenWidth + SmallAsteroidRadius), transform.position.y);
                }
                else if (transform.position.x < -(ScreenWidth + SmallAsteroidRadius))
                {
                    transform.position = new Vector3(ScreenWidth + SmallAsteroidRadius, transform.position.y);
                }
                //if the y coordinate of the origin is offscreen, it is set to just onscreen on the other side
                else if (transform.position.y > ScreenHeight + SmallAsteroidRadius)
                {
                    transform.position = new Vector3(transform.position.x, -(ScreenHeight + SmallAsteroidRadius));
                }
                else if (transform.position.y < -(ScreenHeight + SmallAsteroidRadius))
                {
                    transform.position = new Vector3(transform.position.x, ScreenHeight + SmallAsteroidRadius);
                }

                break;

            default:
                //if the x coordinate of the origin is offscreen, it is set to just onscreen on the other side
                if (transform.position.x > ScreenWidth)
                {
                    transform.position = new Vector3(-ScreenWidth, transform.position.y);
                }
                else if (transform.position.x < -ScreenWidth)
                {
                    transform.position = new Vector3(ScreenWidth, transform.position.y);
                }
                //if the y coordinate of the origin is offscreen, it is set to just onscreen on the other side
                else if (transform.position.y > ScreenHeight)
                {
                    transform.position = new Vector3(transform.position.x, -ScreenHeight);
                }
                else if (transform.position.y < -ScreenHeight)
                {
                    transform.position = new Vector3(transform.position.x, ScreenHeight);
                }

                break;
        }
    }
}