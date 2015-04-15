using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

    private Charty diagram;

    private float innerRad = 150f;
    private int prevIRad = 150;

    private float outerRad = 200f;
    private int prevORad = 200;

    private float sectors = 7f;
    private int prevSec = 7;

    private float outlineWidth = 10f;
    private int  prevOutlineWidth = 10;

    private bool infoBox = false;
    private bool pointers = false;
    private bool outline = false;

    private string[] toolbarStrings = new string[] {"Equal", "Random"};
    private int toolbarInt = 0;
	
	void Start () {
	    diagram = gameObject.AddComponent <Charty>() as Charty;
	    diagram.MakeEqual(prevSec);
	    diagram.MakeDiagram(prevIRad, prevORad,  Screen.width/2, Screen.height/2);
        diagram.SetInfoBoxPos(new Vector2(10, Screen.height - Screen.height/3 - 50));
        diagram.infoMode="hor";
	}
	
	void Update () {
		TheMouseOver();
	}

    void OnGUI () {
        
        innerRad = GUI.HorizontalSlider (new Rect (25, 25, 200, 30), innerRad, 0, outerRad - 1);
        GUI.Label(new Rect(225, 25, 50, 30), prevIRad.ToString());
        GUI.Label(new Rect(255, 25, 100, 30), "Innner Radius");

        if (innerRad > 50) outerRad = GUI.HorizontalSlider (new Rect (25, 50, 200, 30), outerRad, innerRad + 1, 200);
        if (innerRad < 50) outerRad = GUI.HorizontalSlider (new Rect (25, 50, 200, 30), outerRad, 50, 200);
        GUI.Label(new Rect(225, 50, 50, 30), prevORad.ToString());
        GUI.Label(new Rect(255, 50, 100, 30), "Outer Radius");

        sectors = GUI.HorizontalSlider (new Rect (25, 75, 200, 30),  sectors, 1, 20);

        GUI.Label(new Rect(225, 75, 50, 30), prevSec.ToString());
        GUI.Label(new Rect(255, 75, 50, 30), "Sectors");

        GUI.Label(new Rect(25, 100, 50, 30), "Mode:");
        toolbarInt = GUI.Toolbar(new Rect(105, 100, 200, 30), toolbarInt, toolbarStrings);
        
        if (outline){
        	GUI.Label(new Rect(100, 205, 50, 30), prevOutlineWidth.ToString());
            outlineWidth = GUI.HorizontalSlider (new Rect (125, 210, 100, 30),  outlineWidth, 1, 10);	
        }

        infoBox = GUI.Toggle (new Rect (25, 150, 100, 30), infoBox, "InfoBox");
        diagram.infobox = infoBox;
        
        pointers = GUI.Toggle (new Rect (25, 175, 100, 30), pointers, "Pointers");
        diagram.pointers = pointers;

        outline = GUI.Toggle (new Rect (25, 200, 100, 30), outline, "Outline");
        diagram.outline = outline;

        bool rotateCW = false;
        bool rotateCCW = false;

        if (GUI.RepeatButton(new Rect(10, 300, 100, 30), "Rotate CW")) rotateCW = true;
        if (GUI.RepeatButton(new Rect(20 + 100, 300, 100, 30), "Rotate CCW"))  rotateCCW = true;

        if (rotateCW)   diagram.RotateCW();
        if (rotateCCW)  diagram.RotateCCW();
        
        OnChange();
        
    }

    void OnChange(){
    	if (GUI.changed){

    		if ((int)Mathf.Round(innerRad) != prevIRad){
            	diagram.SetInnerRadius((int)Mathf.Round(innerRad));
            
            	prevIRad = (int)Mathf.Round(innerRad);
            }
            
            if ((int)Mathf.Round(outerRad) != prevORad){
            	diagram.SetOuterRadius((int)Mathf.Round(outerRad));
            	
            	prevORad = (int)Mathf.Round(outerRad);
            }  

            if ((int)Mathf.Round(sectors) != prevSec){
            	diagram.ClearSectors();
            	if (toolbarInt == 0)  diagram.MakeEqual((int)Mathf.Round(sectors));
	            if (toolbarInt == 1)  diagram.MakeRandom((int)Mathf.Round(sectors));
	            diagram.Redraw();
	            prevSec = (int)Mathf.Round(sectors);
            }

            if ((int)Mathf.Round(outlineWidth) != prevOutlineWidth){
                diagram.SetOutlineInnerWidth((int)Mathf.Round(outlineWidth));
                diagram.SetOutlineOuterWidth((int)Mathf.Round(outlineWidth));
	            prevOutlineWidth = (int)Mathf.Round(outlineWidth);
            }
        }
    }
    void TheMouseOver(){
        	Vector2 toCheck = new Vector2(Input.mousePosition.x, InvertY(Input.mousePosition.y));
        	for (int i = 0; i < diagram.SectorsAmount(); i++){
        		if (diagram.ArcContains(toCheck, i)){
                    diagram.MoveArc(i , 1);
        		}else{
                    diagram.MoveArc(i ,-1);
        		}
        	}
        	
    }

    private float InvertY(float y){
            return Screen.height - y;
    }
   
}
