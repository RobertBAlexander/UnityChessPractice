using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public GameObject tileHighlightPrefab;

    private GameObject tileHighlight;
    // Use this for initialization
    void Start () {
        //gets an initial row and column for the highlight tile,
        //turns it into a point and creates a game object from the prefab.
        Vector2Int gridPoint = Geometry.GridPoint(0, 0);
        Vector3 point = Geometry.PointFromGrid(gridPoint);
        tileHighlight = Instantiate(tileHighlightPrefab, point, Quaternion.identity, gameObject.transform);
        // This object is initially deactivated, so it won’t be visible until it’s needed.
        tileHighlight.SetActive(false);
		
	}

	
	// Update is called once per frame
	void Update () {
        //the 'ray' in this raycast, it literally a point of light, or a spotlight that comes from the camera,
        //and highlights the point you clicked on. The gridpoint stuff means it centers on the square that you
        //are at, instead of say... centering over the mouse.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        //if the ray does intersect a collider, then this has the details, including the point at which it happens.
        RaycastHit hit;

        //Physics.Raycast checks to see if the ray intersects any physics collider in the system
        //The board is the only object with a collider. This means that pieces won't hide eachother(whatever that means!)
        if (Physics.Raycast(ray, out hit))
        {
            //the intersection is turned into a grid point
            Vector3 point = hit.point;
            Vector2Int gridPoint = Geometry.GridFromPoint(point);

            //use that gridpoint to find which tile is the one that should be highlighted
            tileHighlight.SetActive(true);
            tileHighlight.transform.position = Geometry.PointFromGrid(gridPoint);
            //if the player left clicks
            if (Input.GetMouseButtonDown(0))
            {
                //a gameobject called 'selectedPiece' calls the 'pieceAtGrid' method in the GameManager, passing in the gridpoint
                //we got from the raycast, and that method passes back the piece at that point.
                GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);
                //calls the 'DoesPieceBelongToCurrentPlayer' method in GameManager to check
                //if the player owns the piece just clicked on
                if (GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece))
                {
                    //selects the piece, and highlights it.
                    GameManager.instance.SelectPiece(selectedPiece);
                    // Reference Point 1: add ExitState call here later
                    //cleans up current state, and calls 'enter state' of next state
                    //TODO learn more about this. Calls ExitState method
                    ExitState(selectedPiece);
                }
            }
        }
        else
        {
            //remove highlight from tile
            tileHighlight.SetActive(false);
        }
    }

    //why would this work, 'enabled' in this case is something we can set with an EnterState method, it seems.
    //this is not a variable that needed to be set elsewhere in code.
    //TODO: Learn more about how functions like this can be used in states.

    public void EnterState()
    {
        enabled = true;
    }
    //passes in the piece that has been selected, to become one that can be moved
    private void ExitState(GameObject movingPiece)
    {
        //disable this state of tileselector
        this.enabled = false;
        //remove highlight on piece
        tileHighlight.SetActive(false);
        //below two lines are calling on the move selector .cs file, getting the component and telling it to enter the 'moving piece' state.
     
        MoveSelector move = GetComponent<MoveSelector>();
        move.EnterState(movingPiece);
    }
}
