using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class GenerateurGrid : MonoBehaviour {

	public Camera battleCam;
	public float espaceEntreCase;
	public int LargeurGrid;
	public int HauteurGrid;
	public GameObject cases;
	public GameObject[,] listeCases;
	private GameObject caseClone;
	private GameObject carteCombat;
	//private Node[] grid;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3 (0, 0, 0);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("Combat"));
		int carteChoisi =Random.Range(0, 2) ;
		if(carteChoisi==1){
			carteCombat = Instantiate (GameObject.Find ("CartePrefab").GetComponent<Prefab> ().carteCombat1prefab,GameObject.Find("Carte").transform) as GameObject;
			LargeurGrid=carteCombat.GetComponent<infoCarte>().gridLargeur;
			HauteurGrid=carteCombat.GetComponent<infoCarte>().gridHauteur;
			battleCam.transform.position = new Vector3 (carteCombat.GetComponent<Tiled2Unity.TiledMap>().NumTilesWide/2.0f,-carteCombat.GetComponent<Tiled2Unity.TiledMap>().NumTilesHigh/2.0f,-1);
			battleCam.orthographicSize = -carteCombat.GetComponent<infoCarte>().gridPosy;
			transform.position = new Vector2 (carteCombat.GetComponent<infoCarte>().gridPosx,carteCombat.GetComponent<infoCarte>().gridPosy);
        }
        else
        {
			carteCombat = Instantiate(GameObject.Find("CartePrefab").GetComponent<Prefab>().carteCombat2prefab,GameObject.Find("Carte").transform) as GameObject;
			LargeurGrid=carteCombat.GetComponent<infoCarte>().gridLargeur;
			HauteurGrid=carteCombat.GetComponent<infoCarte>().gridHauteur;
			battleCam.transform.position = new Vector3 (carteCombat.GetComponent<Tiled2Unity.TiledMap>().NumTilesWide/2.0f,-carteCombat.GetComponent<Tiled2Unity.TiledMap>().NumTilesHigh/2.0f,-1);
			battleCam.orthographicSize = -carteCombat.GetComponent<infoCarte>().gridPosy;
			transform.position = new Vector2 (carteCombat.GetComponent<infoCarte>().gridPosx,carteCombat.GetComponent<infoCarte>().gridPosy);
        }
		listeCases = new GameObject[LargeurGrid+1, HauteurGrid+1];

		for (var i = 1; i < LargeurGrid+1; i++) {
			var go = new GameObject ();
			go.name = "Colonne" + i;
			go.transform.parent = gameObject.transform;
			for (var j =1; j < HauteurGrid+1; j++) {
				caseClone = Instantiate (cases,new Vector3(transform.position.x+(espaceEntreCase*i),transform.position.y-(espaceEntreCase*j),0), Quaternion.identity) as GameObject;
				caseClone.transform.parent = go.transform;
				caseClone.name = "case"+j;
				caseClone.GetComponent<GestionCases> ().PosX = i;
				caseClone.GetComponent<GestionCases> ().PosY = j;
				listeCases [i, j] = caseClone;
			}
		}
		for(var i= 1;i < LargeurGrid+1; i++){
			for(var j=1;j<HauteurGrid+1;j++){
				if (j != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineHaut = listeCases[i,j-1];
				}
				if (j != HauteurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineBas = listeCases [i, j + 1];
				}
				if (i != 1) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineGauche = listeCases [i - 1, j];
				}
				if (i != LargeurGrid) {
					listeCases [i, j].GetComponent<GestionCases> ().CaseVoisineDroite = listeCases [i + 1, j];
				}
			}
		}
	}

		

	// Update is called once per frame
	public List<GameObject> genererRoute(GameObject debut, GameObject fin){
		/*grid = new Node[LargeurGrid + 1, HauteurGrid + 1];
		for (int x = 1; x < LargeurGrid + 1; x++) {
			for (int y = 1; y < HauteurGrid + 1; y++) {
				if (listeCases [x, y].GetComponent<GestionCases> ().EstOccupee) {
					grid [x, y] = new Node (false, x, y);
				} else {
					grid [x, y] = new Node (true, x, y);
				}
			}
		}

		Node startNode = grid [startx, starty];
		Node targetNode = grid [endx, endy];
		*/
		 
		List<GameObject> openSet = new List<GameObject> ();
		HashSet<GameObject> closedSet = new HashSet<GameObject> ();
		openSet.Add (debut);

		while (openSet.Count > 0) {
			GameObject node = openSet [0];
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet [i].GetComponent<GestionCases>().fCost < node.GetComponent<GestionCases>().fCost || openSet [i].GetComponent<GestionCases>().fCost == node.GetComponent<GestionCases>().fCost && openSet [i].GetComponent<GestionCases>().hCost < node.GetComponent<GestionCases>().hCost) {
					if (openSet [i].GetComponent<GestionCases>().hCost < node.GetComponent<GestionCases>().hCost)
						node = openSet [i];
				}
			}

			openSet.Remove (node);
			closedSet.Add (node);

			if (node == fin) {
				List<GameObject> path = new List<GameObject> ();
				GameObject currentNode = fin;
				while (currentNode != debut) {
					path.Add (currentNode);
					currentNode = currentNode.GetComponent<GestionCases>().parent;
				}
				path.Reverse ();
				return path;
			}

			foreach (GameObject neighbour in node.GetComponent<GestionCases>().voisins) {
				if (neighbour == null || neighbour.GetComponent<GestionCases>().EstOccupee && neighbour != fin || closedSet.Contains (neighbour)) {
					continue;
				}

					int newCostToNeighbour = node.GetComponent<GestionCases> ().gCost + GetDistance (node, neighbour);
					if (newCostToNeighbour < neighbour.GetComponent<GestionCases> ().gCost || !openSet.Contains (neighbour)) {
						neighbour.GetComponent<GestionCases> ().gCost = newCostToNeighbour;
						neighbour.GetComponent<GestionCases> ().hCost = GetDistance (neighbour, fin);
						neighbour.GetComponent<GestionCases> ().parent = node;
						
						if (!openSet.Contains (neighbour))
							openSet.Add (neighbour);
					}
				
				//}
			}
		}
		return null;
	}

	int GetDistance(GameObject nodeA,GameObject nodeB){
		int dstX = Mathf.Abs (nodeA.GetComponent<GestionCases>().PosX - nodeB.GetComponent<GestionCases>().PosX);
		int dstY = Mathf.Abs (nodeA.GetComponent<GestionCases>().PosY - nodeB.GetComponent<GestionCases>().PosY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		
		return 14 * dstX + 10 * (dstY - dstX);
	}

	public List<GameObject> TrouverGameObjectCase(List<Vector2> listVector){
		List<GameObject> listCaseCible = new List<GameObject> ();
		foreach (Vector2 vect in listVector) {
			if (vect.x > 0 && vect.x < LargeurGrid+1 && vect.y >0 && vect.y < HauteurGrid+1) {
				listCaseCible.Add(listeCases [(int)vect.x, (int)vect.y]);
			}
		}
		return listCaseCible;
	}

	public List<GameObject> TrouverCaseAttaque(){
		List<GameObject> listCaseCible = new List<GameObject>();
		foreach(GameObject goCase in listeCases){
			if(goCase && goCase.GetComponent<GestionCases>().EstAttack){
				listCaseCible.Add(goCase);
			}
		}
		return listCaseCible;
	}
}