using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCharacter : MonoBehaviour
{
    #region serialized fields
    [SerializeField]
    private float moveSpeed = 10;
    #endregion

    #region private fields
    private Rigidbody2D rigidbody;
    private Text playerIndexLabel;
    private Player controllingPlayer_UseProperty;
    #endregion

    #region public properties
    // Which player controls the character?
    // We will use the Player.PlayerNumber to
    // decide which GamePad to look at.
    public Player ControllingPlayer
    {
        get { return controllingPlayer_UseProperty; }
        set
        {
            controllingPlayer_UseProperty = value;
            UpdatePlayerIndexLabelText();
        }
    }
    #endregion

    #region private properties
    private float HorizontalInput
    {
        get { return Input.GetAxis(HorizontalInputName); }
    }

    private float VerticalInput
    {
        get { return Input.GetAxis(VerticalInputName); }
    }

    // You must configure the Unity Input Manager
    // to have an axis for each control for each supported player.
    // Begin numbering at 1, as Unity numbers "joysticks" beginning at 1.
    // "Joystick" means gamepad in real life...
    private string HorizontalInputName
    {
        get
        {
            return "Horizontal" + ControllingPlayer.PlayerNumber;
        }
    }

    private string VerticalInputName
    {
        get
        {
            return "Vertical" + ControllingPlayer.PlayerNumber;
        }
    }

    private string FireInputName
    {
        get
        {
            return "Fire" + ControllingPlayer.PlayerNumber;
        }
    }
    #endregion

    #region MonoBehaviour functions
    private void Awake ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerIndexLabel = GetComponentInChildren<Text>();
	}

    // Update is called once per frame
    private void FixedUpdate ()
    {
        Move();
	}
    #endregion

    private void UpdatePlayerIndexLabelText()
    {
        playerIndexLabel.text = ControllingPlayer.PlayerNumber.ToString();
    }

    private void Move()
    {
        var moveDirection = new Vector2(HorizontalInput, VerticalInput);
        rigidbody.velocity = moveDirection * moveSpeed;
    }
}
