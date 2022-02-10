using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
 public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        player.movement();
        player.shoot();
    }
}
