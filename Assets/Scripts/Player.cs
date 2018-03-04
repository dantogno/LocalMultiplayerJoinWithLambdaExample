using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int PlayerNumber { get; private set; }
    public bool IsJoined { get; set; }

    public Player(int playerNumber)
    {
        PlayerNumber = playerNumber;
    }
}
