﻿using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class PlayerMovement : MonoBehaviour {


    Rigidbody2D rbody;
    Animator anim;
    private bool _canMove = true;
    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    bool canEvent = false;
	private TiledMap map;
	private CameraFollow cf;

	// Use this for initialization
	void Start () {

        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
		cf = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
	}
	
	// Update is called once per frame
	void Update () {


        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //GetComponent<SpriteRenderer>().sortingOrder = -(int)transform.position.y;

        if (movement_vector != Vector2.zero && _canMove)
        {
            anim.SetBool("isWalking", true);
            GestionWorld.UpdateCoef(true);
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
            GestionWorld.UpdateCoef(false);
        }

        if (rbody.position.x + movement_vector.x > cf.map.transform.position.x &&
            rbody.position.x + movement_vector.x < cf.map.transform.position.x + cf.map.NumTilesWide &&
            rbody.position.y + movement_vector.y < cf.map.transform.position.y &&
            rbody.position.y + movement_vector.y > cf.map.transform.position.y - cf.map.NumTilesHigh && _canMove)
        {
            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * 25);
        }


    }




    void OnCollisionEnter2D()
    {
        canEvent = true;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        //evenement = other.transform.GetComponent<Evenement>();
    }

    void OnCollisionExit2D()
    {
        canEvent = false;
    }
}
