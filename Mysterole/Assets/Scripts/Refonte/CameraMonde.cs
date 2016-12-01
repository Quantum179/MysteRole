using UnityEngine;
using System.Collections;
using Tiled2Unity;
using System.Text;

namespace Mysterole
{
	public class CameraMonde : MonoBehaviour
	{
        //Attributs

        private static GameObject _moi;
        public static GameObject Moi
        {
            get { return _moi; }
        }


        public JoueurMonde joueurMonde;
        public float smoothing = 4f;
        private Vector3 offset;
        private Camera cameraCurr;
        private float z = -20;
        private TiledMap carte;

        private static GameObject _acteurPrincipal;
        public static GameObject ActeurPrincipal
        {
            get { return _acteurPrincipal; }
            set { _acteurPrincipal = value; }
        }

        void Start()
        {

            _moi = this.gameObject;
            cameraCurr = GetComponent<Camera>();
            joueurMonde = JoueurMonde.Moi.GetComponent<JoueurMonde>(); //Faire ça automatiquement depuis GestionMonde

            transform.position = new Vector3(joueurMonde.transform.position.x, joueurMonde.transform.position.y, z);
            offset = transform.position - joueurMonde.transform.position;
        }


        
        void Update()
        {


            if(GestionMonde.FaitCinematique)
            {
                //carte = GestionMonde.CarteActive.GetComponent<TiledMap>();
                ////Vector3 targetCamPos = _acteurPrincipal.transform.position + offset;

                //if (_acteurPrincipal.transform.position.x <= carte.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect)
                //{
                //    targetCamPos.x = carte.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect;
                //}
                //if (_acteurPrincipal.transform.position.x >= carte.transform.position.x + carte.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect)
                //{
                //    targetCamPos.x = carte.transform.position.x + carte.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect;
                //}

                //if (_acteurPrincipal.transform.position.y >= carte.transform.position.y - cameraCurr.orthographicSize)
                //{
                //    targetCamPos.y = carte.transform.position.y - cameraCurr.orthographicSize;
                //}

                //if (_acteurPrincipal.transform.position.y <= carte.transform.position.y - carte.NumTilesHigh + cameraCurr.orthographicSize)
                //{
                //    targetCamPos.y = carte.transform.position.y - carte.NumTilesHigh + cameraCurr.orthographicSize;
                //}


                //transform.position = Vector3.Lerp(transform.position, targetCamPos, GetSmoothing() * Time.deltaTime);
            }
            else
            {
                carte = GestionMonde.CarteActive.GetComponent<TiledMap>();
                Vector3 targetCamPos = joueurMonde.transform.position + offset;

                if (joueurMonde.transform.position.x <= carte.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect)
                {
                    targetCamPos.x = carte.transform.position.x + cameraCurr.orthographicSize * cameraCurr.aspect;
                }
                if (joueurMonde.transform.position.x >= carte.transform.position.x + carte.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect)
                {
                    targetCamPos.x = carte.transform.position.x + carte.NumTilesWide - cameraCurr.orthographicSize * cameraCurr.aspect;
                }

                if (joueurMonde.transform.position.y >= carte.transform.position.y - cameraCurr.orthographicSize)
                {
                    targetCamPos.y = carte.transform.position.y - cameraCurr.orthographicSize;
                }

                if (joueurMonde.transform.position.y <= carte.transform.position.y - carte.NumTilesHigh + cameraCurr.orthographicSize)
                {
                    targetCamPos.y = carte.transform.position.y - carte.NumTilesHigh + cameraCurr.orthographicSize;
                }


                transform.position = Vector3.Lerp(transform.position, targetCamPos, GetSmoothing() * Time.deltaTime);
            }
        }

        float GetSmoothing()
        {
            if (GestTransition.EnTransition)
                return 9999.9f;
            else
                return smoothing;
        }


    }
}



//transform.position = Vector3.Lerp(transform.position, targetCamPos, GetSmoothing() * Time.deltaTime);
