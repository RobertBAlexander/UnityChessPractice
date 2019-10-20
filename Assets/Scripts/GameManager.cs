/*
 * Copyright (c) 2018 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System.Collections.Generic;
using UnityEngine;

//monoBehaviour is the base class from which all unity scripts are derrived.
//https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board board;

    public GameObject whiteKing;
    public GameObject whiteQueen;
    public GameObject whiteBishop;
    public GameObject whiteKnight;
    public GameObject whiteRook;
    public GameObject whitePawn;

    public GameObject blackKing;
    public GameObject blackQueen;
    public GameObject blackBishop;
    public GameObject blackKnight;
    public GameObject blackRook;
    public GameObject blackPawn;

    //set up for a multidimensional array gameObject. The comma means there is a grid of objects in the array, instead of a linear list
    private GameObject[,] pieces;
    private List<GameObject> movedPawns;

    private Player white;
    private Player black;
    public Player currentPlayer;
    public Player otherPlayer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //instantiates the multidimensional array of 'pieces'
        pieces = new GameObject[8, 8];
        movedPawns = new List<GameObject>();
        //white is set as the first player to play, and they are also set as the player with a 'true' bool.
        //This bool is passed into 'Player' upon creation and determins whether the player's pieces can move up or down(forward or back) through the z axis.
        white = new Player("white", true);
        black = new Player("black", false);
        //Games start with White in control, so set current and other player accordingly(this must alternate elsewhere in code upon completion of each move).
        currentPlayer = white;
        otherPlayer = black;
        //Initial setup takes place only after players are created, as each piece will be assigned to a player
        InitialSetup();
    }

    //instantiates all pieces, signifies piece type for prefab, gives each a player(player black, or player white), grid position uses two int values
    //prefab contains 3d shape, and type details that inform movement
    private void InitialSetup()
    {
        AddPiece(whiteRook, white, 0, 0);
        AddPiece(whiteKnight, white, 1, 0);
        AddPiece(whiteBishop, white, 2, 0);
        AddPiece(whiteQueen, white, 3, 0);
        AddPiece(whiteKing, white, 4, 0);
        AddPiece(whiteBishop, white, 5, 0);
        AddPiece(whiteKnight, white, 6, 0);
        AddPiece(whiteRook, white, 7, 0);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(whitePawn, white, i, 1);
        }

        AddPiece(blackRook, black, 0, 7);
        AddPiece(blackKnight, black, 1, 7);
        AddPiece(blackBishop, black, 2, 7);
        AddPiece(blackQueen, black, 3, 7);
        AddPiece(blackKing, black, 4, 7);
        AddPiece(blackBishop, black, 5, 7);
        AddPiece(blackKnight, black, 6, 7);
        AddPiece(blackRook, black, 7, 7);

        for (int i = 0; i < 8; i++)
        {
            AddPiece(blackPawn, black, i, 6);
        }
    }

    public void AddPiece(GameObject prefab, Player player, int col, int row)
    {
        //First instantiate the piece, and add the piece to the board. Type and board location are passed in,
        //as that is information that board needs.
        GameObject pieceObject = board.AddPiece(prefab, col, row);
        //the instantiated object is added to the list of 'pieces' objects owned by the player.
        //player is chosen by passed in 'player' object, and can be 'black' or 'white'
        player.pieces.Add(pieceObject);
        //The board position is passed in again, this time to place the piece we have created in the
        //'pieces gameobject array, which is of size '8 X 8'(the board size).
        //this piece gameobject array is altered again any time a move takes place.
        pieces[col, row] = pieceObject;
    }

    //For when you click on a gridpoint in order to select a chess piece.
    public void SelectPieceAtGrid(Vector2Int gridPoint)
    {
        //Previous code for selecting a piece, but the tutorial has an alt version for some reason.
        //sets the 'selected piece' game object to the piece on the grid point that has been passed in.
        //so a click occurs on a point on the board, which gives an x and y position, which is passed in here
        //and that gridposition is used to select the piece in that position in the grid of 'pieces'
        GameObject selectedPiece = pieces[gridPoint.x, gridPoint.y];
        //there might not be a piece in said position, so we check if the piece exists
        if (selectedPiece)
        {
            //calls the selected piece method in the board class, passing in the piece we found in that position through 'pieces'
            board.SelectPiece(selectedPiece);
        }
        //maybe not supposed ot be here?
        //if(Input.GetMouseButtonDown(0))
        //{
        //    GameObject selectedPiece = GameManager.instance.PieceAtGrid(gridPoint);
        //    if(GameManager.instance.DoesPieceBelongToCurrentPlayer(selectedPiece))
        //    {
        //        GameManager.instance.SelectPiece(selectedPiece);
        //    }
        //}
    }


    //moves a piece from one place to another, passing in the piece, and the grod point to move to.
    public void Move(GameObject piece, Vector2Int gridPoint)
    {
        //gets the 'piece' component(component that uses data from the piece method) of the piece object that we passed in
        Piece pieceComponent = piece.GetComponent<Piece>();
        //checks to see if the piece is of type 'Pawn', and checks to see if it has not moved before.
        if (pieceComponent.type == PieceType.Pawn && !HasPawnMoved(piece))
        {
            //adds this piece to the list of pawns that have moved once or more
            movedPawns.Add(piece);
        }
        //calls the gridforpiece method and
        //sets the starting grid point vector2, to the grid position of the current piece
        Vector2Int startGridPoint = GridForPiece(piece);
        //removes the piece at the starting grid position in pieces gameobject.
        pieces[startGridPoint.x, startGridPoint.y] = null;
        //adds the piece to the finishing grid position in pieces gameobject
        pieces[gridPoint.x, gridPoint.y] = piece;
        //calls the MovePiece method in the board class, passing in the currently selected piece, and the final grid position
        board.MovePiece(piece, gridPoint);
    }

    //add this pawn(piece) to the list of pawns that have moved one or more times.
    public void PawnMoved(GameObject pawn)
    {
        movedPawns.Add(pawn);
    }

    //bool check to see if this pawn is in the list of pawns that have moved one or more times.
    public bool HasPawnMoved(GameObject pawn)
    {
        return movedPawns.Contains(pawn);
    }

    //passes in the current piece in order to see what moves are available(not certain on this).
    //TODO: Need to find what calls this method, what exactly is passed in(assume a piece), and where that object has its component linked in
    public List<Vector2Int> MovesForPiece(GameObject pieceObject)
    {
        //set piece as the piece component attached to the 'pieceObject' that you passed in.
        Piece piece = pieceObject.GetComponent<Piece>();
        //calls the gridforpiece method which tells you which point on the 'pieces[x, y] grid-like gameobject array a piece is on.
        Vector2Int gridPoint = GridForPiece(pieceObject);
        List<Vector2Int> locations = piece.MoveLocations(gridPoint);

        // filter out offboard locations
        locations.RemoveAll(gp => gp.x < 0 || gp.x > 7 || gp.y < 0 || gp.y > 7);

        //using 'FriendlyPieceAt' method, filter out locations with friendly piece
        locations.RemoveAll(gp => FriendlyPieceAt(gp));

        return locations;
    }

    //capture the piece at a specific gridpoint
    public void CapturePieceAt(Vector2Int gridPoint)
    {
        //find which piece is at 'gridPoint'
        GameObject pieceToCapture = PieceAtGrid(gridPoint);
        //if the piece is the king
        if (pieceToCapture.GetComponent<Piece>().type == PieceType.King)
        {
            //player who took the king wins, destroy this instance of the chess game
            Debug.Log(currentPlayer.name + " wins!");
            Destroy(board.GetComponent<TileSelector>());
            Destroy(board.GetComponent<MoveSelector>());
        }
        //else add the piece at that gridPoint to the (gameobject)List of pieces captured by the player.
        currentPlayer.capturedPieces.Add(pieceToCapture);
        //set the item in the 'pieces' multiarray to null(thus removing the piece from the array).
        pieces[gridPoint.x, gridPoint.y] = null;
        //destroy the piece gameObject
        Destroy(pieceToCapture);
    }

    //select the piece passed in
    public void SelectPiece(GameObject piece)
    {
        board.SelectPiece(piece);
    }

    //deslect the piece passed in
    public void DeselectPiece(GameObject piece)
    {
        board.DeselectPiece(piece);
    }

    // bool to check to see if the current piece belongs to the active player
    public bool DoesPieceBelongToCurrentPlayer(GameObject piece)
    {
        //true/false for if the player has the piece in their list of pieces.
        return currentPlayer.pieces.Contains(piece);
    }

    //return the piece gamoObject within the multi-array 'pieces' found at 'gridPoint'.
    public GameObject PieceAtGrid(Vector2Int gridPoint)
    {
        //if the gridpoint passed in is outside the bounds of the chessboard grid, return null.
        if (gridPoint.x > 7 || gridPoint.y > 7 || gridPoint.x < 0 || gridPoint.y < 0)
        {
            return null;
        }
        //return the piece found at the gridpoints passed in
        return pieces[gridPoint.x, gridPoint.y];
    }

    //passes in a piece, to return the grid point it is at.
    public Vector2Int GridForPiece(GameObject piece)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                //checks to see what position in the pieces gameobject(grid) that piece is in
                if (pieces[i, j] == piece)
                {
                    //returns the grid position of that piece as a Vector2Int.
                    
                    //The reason things are done this way, is that the grid positions all need to be stored in the same place
                    //they can not be stored in the 'piece' data, as then you could store multiples in the one place.
                    //pieces is a mutlidimensional array of gameobjects.
                    return new Vector2Int(i, j);
                }
            }
        }
        //if no piece is found, return -1, -1,which shows us that it is an out of place piece
        return new Vector2Int(-1, -1);
    }
    //check if the piece at a location is friendly
    public bool FriendlyPieceAt(Vector2Int gridPoint)
    {
        //passes in the gridpoint to find which piece is at that gridpoint
        GameObject piece = PieceAtGrid(gridPoint);
        //if there is no piece
        if (piece == null)
        {
            return false;
        }
        //check within the list of pieces under the other player for this piece
        if (otherPlayer.pieces.Contains(piece))
        {
            return false;
        }

        return true;
    }

    public void NextPlayer()
    {
        //code to swap the 'current' player with the 'other' player.
        //creates a swaping position.
        Player swapingPlayer = currentPlayer;
        //makes the current player become 'other'
        currentPlayer = otherPlayer;
        //makes the other player become 'swaping', or 'current'
        otherPlayer = swapingPlayer;
    }

    


}




////Get the piece component from the game piece(so pawn or whatever?), and get location
//Piece piece = pieceObject.GetComponent<>();
////setting gridPoint as the gridpoint that piece is on
//Vector2Int gridPoint = GridForPiece(pieceObject);

////filter out offboard locations
//locations.RemoveAll(TileSelector => TileSelector.x< 0 || TileSelector.x> 7 || TileSelector.y< 0 || TileSelector.y> 7);

//        //filter out locatiosn with friendly piece
//        locations.RemoveAll(TileSelector => FriendlyPieceAt(TileSelector));
//        return locations;