﻿using System;

namespace Mysterole
{
	public class Deplacement : Evenement
	{
        float _x;
        float _y;

        public Deplacement () : base()
		{


		}

        public Deplacement(float x, float y)
        {
            _x = x;
            _y = y;

        }


        /*private EffectuerDeplacement()
        {

        }*/
	}
}













////////////////////



/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mysterole
{
public class Deplacement : Evenement 
{
    public Vector2 Destination;


    public Deplacement(float x, float y) : base()
    {
        Destination = new Vector2(x, y);
    }

    public override string GetTypeEvent()
    {
        return "Deplacement";
    }


}
}
*/
