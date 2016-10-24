﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mysterole;
using System.Text;

public class Pnj : MonoBehaviour {

    private Rigidbody2D rbody;
    private Animator anim;
    private Vector2 offset;
    private int velocity = 10;
    public string nomPNJ;

    private List<Evenement> dataPnj;
    private int index = 0;

    private bool isMoving = false;

    // Use this for initialization
    void Start () {

        
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        dataPnj = new List<Evenement>();

        dataPnj.Add(new Dialogue("Halte là !"));
        dataPnj.Add(new Deplacement(115, -176));
        dataPnj.Add(new Dialogue("Vous n'avez pas le droit d'entrer ici !"));


        

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.B))
        {
            RunEvent(true);
        }


        if (dataPnj[index].GetTypeEvent().ToString() == "Dialogue")
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                EcranDialogue.closeDialog();

                index++;
            }
        }


        if (dataPnj[index].GetTypeEvent().ToString() == "Deplacement")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isMoving = true;
            }

            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    Debug.Log(rbody.position);
            //    //Debug.Log(((Deplacement)dataPnj[index]).Destination);
            //    //Debug.Log(rbody.position - ((Deplacement)dataPnj[index]).Destination);

            //}


            if (isMoving)
            {

                offset = new Vector2(((Deplacement)dataPnj[index]).Destination.x - rbody.position.x,
                          ((Deplacement)dataPnj[index]).Destination.y - rbody.position.y
                        );


                //Debug.Log(rbody.position.x);
                //Debug.Log(((Deplacement)dataPnj[index]).Destination);
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", offset.x);
                anim.SetFloat("input_y", offset.y);

                //rbody.MovePosition(rbody.position + offset * 10 * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, ((Deplacement)dataPnj[index]).Destination, Time.deltaTime * 5);




            }

            if (rbody.position == ((Deplacement)dataPnj[index]).Destination)
            {
                isMoving = false;
                anim.SetBool("isWalking", false);
                index++;

            }
        }



 
            

    }

    public void RunEvent(bool canEvent)
    {
        //if(dataPnj[index].GetType() == typeof(Dialogue))
        //{

        //}
        //else if(dataPnj[index])


       

        switch(dataPnj[index].GetTypeEvent().ToString())
        {
            case "Dialogue":

                EcranDialogue.NewDialog(this.gameObject.name, ((Dialogue)dataPnj[index]).Message);
                break;
            case "Deplacement":
                isMoving = true;
                break;
        }


    }

}
