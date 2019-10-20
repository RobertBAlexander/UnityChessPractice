using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelector : MonoBehaviour {
    
    public GameObject moveLocationPrefab;
    public GameObject tileHighlightPrefab;
    public GameObject attackLocationPrefab;

    //the highlighted tile?
    private GameObject tileHighlight;
    //piece that was selected
    private GameObject movingPiece;

    private List<Vector2Int> moveLocations;
    private List<GameObject> locationHighlights;

	// Use this for initialization
	void Start () {
        //The move selector begins disabled because nothing should be selected to move.
        this.enabled = false;
        //creates a tile highlighter It begins at the point 0, 0
        tileHighlight = Instantiate(tileHighlightPrefab, Geometry.PointFromGrid(new Vector2Int(0, 0)),
            Quaternion.identity, gameObject.transform);
        //makes the tile highlight not appear or be active yet.
        tileHighlight.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        //think of this like a light ray that extends out from the point of the camera.
        //The great thing about this is that whatever you see through that camera, is the same as these rays extending from it.
        //when you find the ray that intersects with the mouse position, you are essentially clicking on the exact thing that
        //it looks like the mouse is hovering over, from your perspective.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //if the ray hits an object
        if(Physics.Raycast(ray, out hit))
        {
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            if(Input.GetMouseButtonDown(0))
            {
                if (!moveLocations.Contains(gridPoint))
                {
                    return;
                }

                if (GameManager.instance.PieceAtGrid(gridPoint) == null)
                {
                    //if moving to empty point
                    GameManager.instance.Move(movingPiece, gridPoint);
                }
                else
                {
                    //if moving to point with enemy piece, capture it
                    GameManager.instance.CapturePieceAt(gridPoint);
                    GameManager.instance.Move(movingPiece, gridPoint);
                }

                ExitState();
            }
        }
        else
        {
            tileHighlight.SetActive(false);
        }
	}

    public void EnterState(GameObject piece)
    {
        movingPiece = piece;
        this.enabled = true;

        //gets a list of valid locations
        moveLocations = GameManager.instance.MovesForPiece(movingPiece);
        //the empty list to store the tile oerlay objects(what does that mean?)
        locationHighlights = new List<GameObject>();
        //loop over each loaction in the list(the list of valid possible move locations)
        foreach (Vector2Int loc in moveLocations)
        {
            GameObject highlight;
            //if there is a piece there, it is an enemy piece, and an attack move is made. We didn't store the type of move location
            if (GameManager.instance.PieceAtGrid(loc))
            {
                highlight = Instantiate(attackLocationPrefab, Geometry.PointFromGrid(loc),
                    Quaternion.identity, gameObject.transform);
            }
            else
            {
                highlight = Instantiate(moveLocationPrefab, Geometry.PointFromGrid(loc),
                    Quaternion.identity, gameObject.transform);
            }
            locationHighlights.Add(highlight);
        }

    }

    private void ExitState()
    {
        this.enabled = false;
        tileHighlight.SetActive(false);
        GameManager.instance.DeselectPiece(movingPiece);
        movingPiece = null;
        //below two lines are calling on the tile selector .cs file, getting the component and telling it to enter the 'select tile' state.
        TileSelector selector = GetComponent<TileSelector>();
        GameManager.instance.NextPlayer();
        selector.EnterState();
        foreach (GameObject highlight in locationHighlights)
        {
            Destroy(highlight);
        }

    }
}
