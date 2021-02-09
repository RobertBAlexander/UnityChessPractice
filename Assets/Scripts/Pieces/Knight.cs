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

public class Knight : Piece
{
    public override List<Vector2Int> MoveLocations(Vector2Int gridPoint)
    {
        List<Vector2Int> locations = new List<Vector2Int>();
        int forwardDirection = GameManager.instance.currentPlayer.forward;
        Vector2Int forwardTwoRight = new Vector2Int(gridPoint.x + 1, gridPoint.y + (forwardDirection * 2));
        locations.Add(forwardTwoRight);
        Vector2Int rightTwoForward = new Vector2Int(gridPoint.x + 2, gridPoint.y + forwardDirection);
        locations.Add(rightTwoForward);
        Vector2Int rightTwoBack = new Vector2Int(gridPoint.x + 2, gridPoint.y - forwardDirection);
        locations.Add(rightTwoBack);
        Vector2Int backTwoRight = new Vector2Int(gridPoint.x + 1, gridPoint.y - (forwardDirection * 2));
        locations.Add(backTwoRight);
        Vector2Int backTwoLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y - (forwardDirection * 2));
        locations.Add(backTwoLeft);
        Vector2Int leftTwoBack = new Vector2Int(gridPoint.x - 2, gridPoint.y - forwardDirection);
        locations.Add(leftTwoBack);
        Vector2Int leftTwoForward = new Vector2Int(gridPoint.x - 2, gridPoint.y + forwardDirection);
        locations.Add(leftTwoForward);
        Vector2Int forwardTwoLeft = new Vector2Int(gridPoint.x - 1, gridPoint.y + (forwardDirection * 2));
        locations.Add(forwardTwoLeft);
        return locations;
    }
}
