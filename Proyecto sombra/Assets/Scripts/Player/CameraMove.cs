using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    public bool fixOnPlayer;

    //private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);


        /*offset = transform.position - player.transform.position;
        transform.position = player.transform.position;*/

        fixOnPlayer = true;

    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if (fixOnPlayer)
        {
            // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        }
    }
}