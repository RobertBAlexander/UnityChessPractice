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

public class Geometry
{
    //turns a GridPoint into a Vector3 actual point in the scene.
    static public Vector3 PointFromGrid(Vector2Int gridPoint)
    {
        //x position is found by moving position back by 3.5, then turning the grid number into a float
        float x = -3.5f + 1.0f * gridPoint.x;
        //y position is found by moving position back by 3.5, then turning the grid number into a float
        float z = -3.5f + 1.0f * gridPoint.y;
        //z position is 0
        return new Vector3(x, 0, z);
    }

    //gives you a GridPoint for a given column and row.
    static public Vector2Int GridPoint(int col, int row)
    {
        //simply outputs the two numbers entered
        return new Vector2Int(col, row);
    }

    //turns a Vector 3 position on in the scene into a gridpoint on the board
    static public Vector2Int GridFromPoint(Vector3 point)
    {
        //which column on the board the gridpoint is on
        int col = Mathf.FloorToInt(4.0f + point.x);
        //which row on the board the gridpoint is on. Unclear why this is z, not y.
        int row = Mathf.FloorToInt(4.0f + point.z);
        return new Vector2Int(col, row);
    }
}
