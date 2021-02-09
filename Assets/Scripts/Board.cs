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

using UnityEngine;

public class Board : MonoBehaviour
{
    public Material defaultMaterial;
    public Material selectedMaterial;

    //adds a piece with values (piece type, column, row)
    //player is not passed through here(from piece creation in gameManager), as the piece gets attached to the player, once this method has been called
    public GameObject AddPiece(GameObject piece, int col, int row)
    {
        //create a vector2Int gridPoint who's geometry matches the gridpoint of col to x, and row to y.
        Vector2Int gridPoint = Geometry.GridPoint(col, row);
        //instantiate a new 'piece' gameobject. The type of piece to instantiate is the type passed in, and gridpoint is also passed in.
        GameObject newPiece = Instantiate(piece, Geometry.PointFromGrid(gridPoint),
            // Quaternion.identity refers to the rotation of the object. Quaternion means no rotation https://docs.unity3d.com/ScriptReference/Quaternion-identity.html
            //
            Quaternion.identity, gameObject.transform);
        return newPiece;
    }
    //removes the passed in piece gameobject
    public void RemovePiece(GameObject piece)
    {
        Destroy(piece);
    }

    //moves the passed in piece to the passed in gridpoint.
    public void MovePiece(GameObject piece, Vector2Int gridPoint)
    {
        //this crashed when I tried to click on a piece to move, but didn't have the 'EnterState(GameObject piece)' method working fully in 'MoveSelector'
        //transform the position of the pieces to the geometry position of the passed in gridPoint.
        piece.transform.position = Geometry.PointFromGrid(gridPoint);
    }

    //select a piece
    public void SelectPiece(GameObject piece)
    {
        //set the mesh renderer to the meshrenderer component in the selected piece
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        //set the material of the render to 'selectedMaterial', which is the material to use when a selection occurs(in this case a highlight colour material
        renderers.material = selectedMaterial;
    }
    //deselect a piece
    public void DeselectPiece(GameObject piece)
    {
        //set the mesh renderer to the meshrenderer component in the selected piece
        MeshRenderer renderers = piece.GetComponentInChildren<MeshRenderer>();
        //set the material of the render back to 'default material', which is the material to use when the object is not selected.
        renderers.material = defaultMaterial;
    }
}
