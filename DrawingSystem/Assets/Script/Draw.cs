using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Draw : MonoBehaviour {


	// Circle function by Bournellis Ippokratis, for Vectrosity. Just for fun, no optimisations.

	Material lineMaterial = null;
	int nrOfCircles = 10;


	Vector2[] Circle ( int xcenter , int ycenter, int radius ) {
		// Create a javascript array to hold the points of the circle
        
 
		List<Vector2> pointsArray = new List<Vector2>();
		// Calculate each point of the circle.
		for (float theta = 0; theta < ( 2 * Mathf.PI ); theta += 0.01f )
		{
           float x = xcenter + ( radius * Mathf.Sin ( theta ) );
		   float y = ycenter + ( radius * Mathf.Cos ( theta ) );
            Vector2 xy = new Vector2(x, y);

            pointsArray.Add ( xy );
		}
        // copy the data from the javascript array to a Vector2 array
        Vector2[] circlePoints = pointsArray.ToArray(); //.ToBuiltin ( Vector2 );

        return circlePoints;
	}  

	void Start () {
		// Set up the vector object camera (using the camera tagged "Main Camera" by default)
		Vector.SetCamera();
		// Make a couple of circles
		for ( int i = 0; i < nrOfCircles; ++i){  
			Vector2[] linePoints = Circle ( Random.Range ( 100, 200 ), Random.Range( 100, 200 ),
				Random.Range ( 10, 100) );
			VectorLine myCircle = new VectorLine ( "myCircle", linePoints,  Color.white,
				lineMaterial, 3, 0, 1, LineType.Continuous, Joins.Fill);
			// Draw the circle
			Vector.DrawLine(myCircle);
		}
	}


}
