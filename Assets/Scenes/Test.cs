using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private CellularAutomaton cellular;
    public Vector2Int size;
    public float fill;
    private Color[,] pixel_colors;
    public Texture2D result;
	// Use this for initialization
	void Start () {
        result = new Texture2D(size.x,size.y);
        cellular = new CellularAutomaton(size.x,size.y,fill);
        pixel_colors = new Color[size.x,size.y];
        SyncColorsWithCells();
	}

	private void Update(){
        cellular.Step();
        SyncColorsWithCells();
        
	}
	void SyncColorsWithCells() {
        for (int x = 0; x < size.x; x++)
            for (int y = 0; y < size.y; y++){
                Color deactive = Color.Lerp(Color.white,Color.grey, cellular.GetHeatValue(x,y)/1.5f);
                Color active = Color.Lerp(Color.black, Color.green, cellular.GetHeatValue(x, y)/1.5f);
                Color newcolor = cellular.GetCellValue(x, y) == 1 ? active : deactive;
                pixel_colors[x, y] = newcolor;
                result.SetPixel(x, y, pixel_colors[x, y]); 
            }
        result.Apply();
    }


	private void OnGUI(){
        GUI.DrawTexture(new Rect(Screen.width/2-256,Screen.height/2-256,512,512),result);
	}

}
