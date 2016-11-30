using UnityEngine;
using System.Collections;
using MathCore;

public class GridOverlay : MonoBehaviour {
	

	public bool showMain = true;

	public uint gridSizeX;
	public uint gridSizeY;

	public uint tickSizeX;
	public uint tickSizeY;

	public float largeStep = 1f;

	public uint lineWidth = 1;

	public float posX;
	public float posY;

	public Color xColor = new Color(0f,1f,0f,1f);
	public Color yColor = new Color(0f,0.5f,0f,1f);

	public FasorHandler[] Fasores;
	public static GridOverlay instance;

	public Material lineMaterial;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	void Update () {
		if (largeStep == 0)
			largeStep = 1;
		else
			largeStep = largeStep > 0 ? largeStep : -largeStep;
	}

	void OnDrawGizmos() {
		OnRenderObject ();
	}
	 
	void OnRenderObject() {
		// set the current material
		lineMaterial.SetPass( 0 );

		GL.Begin( GL.QUADS );
		if(showMain)
		{
			float width = lineWidth / 100f;
			float tickX = tickSizeX / 100f;
			float tickY = tickSizeY / 100f;
			float startX = - gridSizeX/2f;
			float startY = - gridSizeY/2f;

			// X axis
			GL.Color(xColor);
			GL.Vertex3( startX-width/2f, posY, 0);
			GL.Vertex3( startX-width/2f, posY+width, 0);
			GL.Vertex3( startX+gridSizeX+width/2f, posY+width, 0);
			GL.Vertex3( startX+gridSizeX+width/2f, posY, 0);

			// Y Axis
			GL.Color(yColor);
			GL.Vertex3( posX, startY-width/2f, 0);
			GL.Vertex3( posX+width, startY-width/2f, 0);
			GL.Vertex3( posX+width, startY+gridSizeY+width/2f, 0);
			GL.Vertex3( posX, startY+gridSizeY+width/2f, 0);

			// X axis sublines
			GL.Color(xColor);
			float dist = ((posX+gridSizeX/2f) % largeStep);
			if (dist < 0)
				dist+=largeStep;
			for(float k = startX + dist; k <= startX+gridSizeX; k += largeStep) {
				if (Mathf.Abs(k - posX) > 0.1f) {
					GL.Vertex3( k-width/2f, posY-tickX, 0);
					GL.Vertex3( k+width/2f, posY-tickX, 0);
					GL.Vertex3( k+width/2f, posY+tickX+width, 0);
					GL.Vertex3( k-width/2f, posY+tickX+width, 0);
				}
			}

			// Y axis sublines
			GL.Color(yColor);
			dist = ((posY+gridSizeY/2f) % largeStep);
			if (dist < 0)
				dist+=largeStep;
			for(float j = startY + dist; j <= gridSizeY+startY; j += largeStep) {
				if (Mathf.Abs(j - posY) > 0.1f) {
					GL.Vertex3( posX-tickY, j-width/2f, 0);
					GL.Vertex3( posX-tickY, j+width/2f, 0);
					GL.Vertex3( posX+tickY+width, j+width/2f, 0);
					GL.Vertex3( posX+tickY+width, j-width/2f, 0);
				}
			}

		}

		GL.End();

		if (Fasores != null) {
			GL.Begin (GL.LINES);

			foreach (FasorHandler f in Fasores) {
				GL.Color (f.cor);
				Complexo c = f.fasor.Retangular (0);
				GL.Vertex3 (0, 0, 0);
				GL.Vertex3 ((float)c.Real, (float)c.Imaginario, 0f);
			}

			GL.End ();
		}
	}

}