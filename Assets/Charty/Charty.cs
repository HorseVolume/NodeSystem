using UnityEngine;
using System.Collections;

public class Charty : MonoBehaviour {
    
  //Sectors info;
	private ArrayList Values = new ArrayList();
	private ArrayList Names = new ArrayList();
	private ArrayList Colors = new ArrayList();
  private ArrayList Percents = new ArrayList();

  //Drawing Arrays;
  private Vector2[] Radiuses;
	private Vector2[] Centers;
  private Vector2[] StartEnd;
	private Vector2[][] ArcsInner;
	private Vector2[][] ArcsOuter;
  
  //Drawing Parametrs;
	private const int segments = 360;
	private const int degrees = 360;
	private int OuterRadius = 0;
	private int InnerRadius = 0;
  private int rotation = 0;
	private Vector2 Center;

	//Outline;
  private Vector2[]   OutlineRadiuses;
  private Vector2[][] OutlineInner;
	private Vector2[][] OutlineOuter;
  private Vector2[][] OutlineInner2;
  private Vector2[][] OutlineOuter2;
  private int OutlineInnerWidth = 0;
  private int OutlineOuterWidth = 0;
  private Color OutLineColor = Color.black;
	
  //Infobox;
  public  Font infoFont;
  public  int infoFontSize = 12;
  public  Color infoFontColor = Color.white;
  public  string infoMode = "hor";
  public  string infoTitle = "Stats";
  public  string infoToShow = "names"; 
  public  int per_rows = 10;
  public  int per_columns = 10;
  private Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);
  private Rect[] InfoBoxRects;
  private GUIStyle infoStyle =    new GUIStyle();
  private int infowidth = 35;
  private int infoheight = 30;
  private int infoSquareSize = 10;
  
  //Pointers;
  public  Font pointerFont;
  public  int pointerFontSize = 12;
  public  Color pointerFontColor = Color.white;
  public  int pointerLength = 20;
  public  string pointermode = "percents";
  private Rect[] InfoRects;
  private GUIStyle pointerStyle = new GUIStyle();
  
  //Bools;
  public  bool pointers = false;
  public  bool outline = false;
  public  bool infobox = false;
  private bool draw = false;
  private bool showSquares = false;
  
  //Other;
  private Material mat; 
  private int widthlimit = 100;
  private string avoiderror;
  private int avoiderrorInt;
  private int limit = 60;
  private Color emptyCol = new Color(0f, 0f, 0f, 0f);
  
  //Main functions;
  void Awake(){
    makeMaterial();  
  }

	void OnGUI(){
    AllDrawing();
	}

  //Getters;
  public int GetIndex(string name){
    for (int i = 0; i < SectorsLength(); i++){
        string st = (string)Names[i];
        if (st == name) return i;
    }
    return -1;
  }

  public int GetIndex(float value){
    for (int i = 0; i < SectorsLength(); i++){
        float st = (float)Values[i];
        if (st == value) return i;
    }
    return -1;
  }

  public int GetIndex(int value){
    for (int i = 0; i < SectorsLength(); i++){
        int st = (int)Values[i];
        if (st == value) return i;
    }
    return -1;
  }

  public int GetIndex(Color myColor){
    for (int i = 0; i < SectorsLength(); i++){
        Color st = (Color)Colors[i];
        if (st == myColor) return i;
    }
    return -1;
  }

  public Color GetColor(int index){
     //
     return (Color)Colors[index];
  }

  public float GetPercent(int index){
    //
    return (float)Percents[index];
  }

  public float GetValue(int index){
    //
    return (float)Values[index];
  }

  public string GetName(int index){
    //
    return (string)Names[index];
  }

  private int getWindowX(){
    //
    return (int)Mathf.Round(windowRect.x);
  }

  private int getWindowY(){
    //
    return (int)Mathf.Round(windowRect.y);
  }

  private int getWindowW(){
    //
    return (int)Mathf.Round(windowRect.width);
  }

  private int getWindowH(){
    //
    return (int)Mathf.Round(windowRect.height);
  }

  public int GetRotation(){
    //
    return rotation;
  }

  public int GetOutlineInnerWidth(){
    //
    return OutlineInnerWidth;
  } 

  public int GetOutlineOuterWidth(){
    //
    return OutlineOuterWidth;
  } 

  public Color GetOutlineColor(){
    //
    return OutLineColor;
  } 

  public void SetColor(Color newCol, int index){
    //
    Colors[index] = newCol;
  }

  public void SetValue(int newVal, int index){
    if (newVal < 0){
        ThrowError("Value cannot be negative");
        return;
    }
    Values[index] = newVal;
  }

  public void SetValue(float newVal, int index){
    if (newVal < 0){
        ThrowError("Value cannot be negative");
        return;
    }
    Values[index] = newVal;
  }

  public void SetName(string newNam, int index){
    //
    Names[index] = newNam;
  }

  public void SetInfoBoxPos(Vector2 myVec){
    int x = FloatToInt(myVec.x);
    int y = FloatToInt(myVec.y);
    windowRect.x = x;
    windowRect.y = y;
  }

  public Vector2 GetInfoBoxPos(){
    int x = FloatToInt(windowRect.x);
    int y = FloatToInt(windowRect.y);
    Vector2 myVec = new Vector2(x, y);
    return myVec;
  }

  //Setters;
  public void SetPos(Vector2 newPos){
    Center = new Vector2(FloatToInt(newPos.x), FloatToInt(newPos.y));
    if (draw) Redraw();
  }

  public void SetPos(int x, int y){
    //
    SetPos(new Vector2(x, y));
  }

  public void SetPos(float x, float y){
    //
    SetPos(new Vector2(x, y));
  }

  public Vector2 GetCenter(){
    //
    return Center;
  }

  public int GetInnerRadius(){
    //
    return InnerRadius;
  }

  public int GetOuterRadius(){
    //
    return OuterRadius;
  }

  private void SetX(int x){
    Center.x = x;
    if (draw) Redraw();
  }

  private void SetY(int y){
    Center.y = y;
    if (draw) Redraw();
  }

  public void SetInnerRadius(int rad){
    if (rad < 0){
        ThrowError("Inner radius cannot be less than zero.");
        return;
    }
    if (OuterRadius != 0){
        if (rad > OuterRadius){
            ThrowError("Inner radius cannot be bigger than outer radius.");
            return;
        }
    }
    InnerRadius = rad;
    if (draw) Redraw();
  }

  public void SetOuterRadius(int rad){
    if (rad < 1){
        ThrowError("Outer radius cannot be less than one.");
        return;
    }
    //if (InnerRadius != null){
        if (rad < InnerRadius){
            ThrowError("Outer radius cannot be less than inner radius.");
            return;
        }
    //}
    OuterRadius = rad;
    if (draw) Redraw();
  }
  
  //Outline;
  public void SetOutlineInnerWidth(int width){
    if ( InnerRadius - width < 0) return;
    OutlineInnerWidth = width;
    for (int i = 0; i < SectorsLength(); i++){
         OutlineRadiuses[i].x = Radiuses[i].x - width;
    }
    FillOutline();
  } 

  public void SetOutlineOuterWidth(int width){
    if (width > widthlimit) return;
    OutlineOuterWidth = width;
    for (int i = 0; i < SectorsLength(); i++){
         OutlineRadiuses[i].y = Radiuses[i].y + width;
    }
    FillOutline();
  } 

  public void SetOutlineColor(Color c){
    //
    OutLineColor = c;
  } 

  //Drawing;
  private void AllDrawing(){
     if (draw){
        GUI.backgroundColor = EmptyColor();
        InfoStyle();
        if (infobox) windowRect = GUI.Window(0, windowRect, DoMyWindow, infoTitle);
        DrawArcs();
        if (pointers)  ShowPercents(); 
     }
  }

  private void InfoStyle(){
    infoStyle.fontSize = infoFontSize;
    infoStyle.normal.textColor = infoFontColor;
    if (infoFont != null) infoStyle.font = infoFont;
    if (infoFont == null) infoStyle.font = GUI.skin.font;
  }

  public void MakeEqual(int count){
    //
    MakeWhatever(false, count);
  }

  public void MakeRandom(int count){
    //
    MakeWhatever(true, count);
  }

  private void MakeWhatever(bool random, int count){
    if (count <= 0){
        ThrowError("Number of sectors to create is less or equals to zero.");
        return;
    } 
    if (count > limit){
        ThrowError("Number of sectors to create is more than maximum limit.");
        return;
    } 
    float value = 1f;
    for (int i = 0; i < count; i++){
         if (random) value = Random.Range(0f, 1000f);
         AddSector(i.ToString(), value, RandomColor());
    }
  }

  public void MakeDiagram(int innerRad, int outerRad, int x, int y){
    showSquares = false;
    draw = false;
    if (outerRad <= innerRad){
        ThrowError("Outer radius is less or equal than inner. Diagram is undefined.");
        return;
    }
    SetInnerRadius(innerRad);
    SetOuterRadius(outerRad);
    SetX(x);
    SetY(y);
    if (ZeroSumError()){
        ThrowError("All values appear to be zero. Diagram is undefined.");
        return;
    }
    if (ZeroMembersError()){
        ThrowError("No sectors to draw. Diagram is undefined.");
        return;
    }
    MakeRadiusesAndCenters();
    DefineStartEnd();
    FillDiagram();
    FillOutline();
    draw = true;
  }

  public void MakeDiagram(int innerRad, int outerRad, float x, float y){
    //
    MakeDiagram(innerRad, outerRad, FloatToInt(x), FloatToInt(y));
  }

  public void Redraw(){
    //
    MakeDiagram(InnerRadius, OuterRadius, Center.x, Center.y);
  }

  public void AddSector(string name, float value, Color theColor){
    Names.Add(name);
    Values.Add(value);
    Colors.Add(theColor);
    if (draw) Redraw();
  }

  public void AddSector(string name, float value){
    //
    AddSector(name, value, RandomColor());
  }

  public void AddSector(string name, int value){
    float v = (float)value;
    AddSector(name, v, RandomColor());
  }

  public void AddSector(string name, int value, Color theColor){
    float v = (float)value;
    AddSector(name, v, theColor);
  }

  public void RemoveSectorByIndex(int index){
     if (index >= -1){
         Names.RemoveAt(index);
         Values.RemoveAt(index);
         Colors.RemoveAt(index);
         Redraw();
     }
  }

  public void RemoveSector(string name){
    for (int i = 0; i < SectorsLength(); i++){
         string theName = (string)Names[i];
         if (theName == name){
             RemoveSectorByIndex(i);
             break;
         }
    }
  }

  public void RemoveSector(float value){
    for (int i = 0; i < SectorsLength(); i++){
         float theValue = (float)Values[i];
         if (theValue == value){
             RemoveSectorByIndex(i);
             break;
         }
    }
  }

  public void RemoveSector(int value){
    for (int i = 0; i < SectorsLength(); i++){
         int theValue = (int)Values[i];
         if (theValue == value){
             RemoveSectorByIndex(i);
             break;
         }
    }
  }

  public void RemoveSector(Color c){
    for (int i = 0; i < SectorsLength(); i++){
         Color theColor = (Color)Colors[i];
         if (theColor == c){
             RemoveSectorByIndex(i);
             break;
         }
    }
  }

  public void ClearSectors(){
    Names.Clear();
    Values.Clear();
    Colors.Clear();
    Percents.Clear();
    draw = false;
  }

  private void MakeRadiusesAndCenters(){
    Vector2 rad = new Vector2(InnerRadius, OuterRadius);
    Radiuses    = new Vector2[SectorsLength()];
    Centers     = new Vector2[SectorsLength()];
    OutlineRadiuses = new Vector2[SectorsLength()];
    for (int i = 0; i < SectorsLength(); i++){
         Radiuses[i] = rad;
         OutlineRadiuses[i] = rad;
         Centers[i] = Center;
    } 
  }

  private void DefineStartEnd(){
    int[] columns = GetColumns();
    int s = 0;
    int e = 0;
    for (int i = 0; i < SectorsLength(); i++){
         s = e;
         if (columns[i] != 0) e +=columns[i];
         if (e == degrees - 1) e = 0;
         if (columns[i] == 0){
             s = e - 1;
             if (s == -1) s = degrees - 1;
         } 
         if (e == degrees - 1) e = 0;
         StartEnd[i] = new Vector2(s, e);
    }
  }

  private int[] GetColumns(){
    Percents.Clear();
    StartEnd = new Vector2[SectorsLength()];
    int[] columns = new int[SectorsLength()];
    int colamount = 0;
    float sum = GetSum();
    for (int i = 0; i < SectorsLength(); i++){
         float v = (float)Values[i];
         float percentage = DisplayPercentage((double)v/sum);
         colamount += ColumnAmount(percentage);
         columns[i] = ColumnAmount(percentage);
         Percents.Add(percentage);
    }
    return ColumnFiltration(columns, colamount);
  }

  private int ColumnAmount(float myfloat){
    float segPercent = RoundFloat(segments/100f);
    if (myfloat < 1f/segPercent) return 0;
    return (int)Mathf.Floor(myfloat * segPercent);
  }

  private int[] ColumnFiltration(int [] columns, int colamount){
    int diff = segments - colamount;
    if (diff != 0){
        for (int i = 0; i < SectorsLength(); i++){
             if (columns[i] != 0) {
                 columns[i]++;
                 diff--;
             }
             if (diff == 0) break;
        }
    }
    return columns;
  }
  
  private void FillDiagram(){
    ArcsInner = new Vector2[SectorsLength()][];
    ArcsOuter = new Vector2[SectorsLength()][];
    for (int i = 0; i < SectorsLength(); i++){
         RedrawArc(i);
    }
  }

  private void FillOutline(){
    OutlineInner = new Vector2[SectorsLength()][];
    OutlineOuter = new Vector2[SectorsLength()][];
    OutlineInner2 = new Vector2[SectorsLength()][];
    OutlineOuter2 = new Vector2[SectorsLength()][];
    for (int i = 0; i < SectorsLength(); i++){
         RedrawOutline(i);
    }
  }

  private void MakeArc(int index, int from, int to, int Inrad, int Ourad, int x, int y){
    ArcsInner[index] = ReceiveCoordinates(from, to, Inrad, x, y);
    ArcsOuter[index] = ReceiveCoordinates(from, to, Ourad, x, y);
  }

  private void MakeOutline(int index, int from, int to, int Inrad, int Ourad, int x, int y){
    OutlineInner[index] = ReceiveCoordinates(from, to, Inrad, x, y);
    OutlineOuter[index] = ReceiveCoordinates(from, to, Ourad, x, y);
  }

  private void MakeOutline2(int index, int from, int to, int Inrad, int Ourad, int x, int y){
    OutlineInner2[index] = ReceiveCoordinates(from, to, Inrad, x, y);
    OutlineOuter2[index] = ReceiveCoordinates(from, to, Ourad, x, y);
  }

  private Vector2[] ReceiveCoordinates(int from, int to, int rad, int x, int y){
    int indexOfArray = -1;
    if (to <= from) to += degrees;
    Vector2[] coordinates = new Vector2[(to - from) + 1];
    for (int i = from; i < to + 1; i++){
         indexOfArray++;
         coordinates[indexOfArray] = GetPoint(WrapAngle(i), rad, x, y);
    }
    return coordinates;
  }
  
  private void RedrawArc(int index){
    int s = FloatToInt(StartEnd[index].x);
    int e = FloatToInt(StartEnd[index].y);
    int x = FloatToInt(Centers[index].x);
    int y = FloatToInt(Centers[index].y);
    int n = FloatToInt(Radiuses[index].x);
    int o = FloatToInt(Radiuses[index].y);
    MakeArc(index, s, e, n, o, x, y);
  }

  private void RedrawOutline(int index){
    int s = FloatToInt(StartEnd[index].x);
    int e = FloatToInt(StartEnd[index].y);
    int x = FloatToInt(Centers[index].x);
    int y = FloatToInt(Centers[index].y);
    int n = FloatToInt(OutlineRadiuses[index].x - OutlineInnerWidth);
    int o = FloatToInt(OutlineRadiuses[index].y + OutlineOuterWidth);
    MakeOutline(index, s, e, n, InnerRadius, x, y);
    MakeOutline2(index, s, e, OuterRadius, o, x, y);  
  }

  private void DrawArcs(){
    StartMatrix();
    GL.Begin(GL.TRIANGLES);
    OutLineColor.a = 0f;
    if (outline) StartOutline();
    StartArc();
    if (pointers) DrawPointers();
    GL.End();
    if (infobox)  StartInfoBox();
    EndMatrix();
  }

  private void StartMatrix(){
    GL.PushMatrix();
    mat.SetPass(0);
    GL.LoadPixelMatrix(0, Screen.width, 0, Screen.height);
  }

  private void EndMatrix(){
    //
    GL.PopMatrix();
  }

  private void StartArc(){
    for (int i = 0; i < ArcsInner.Length; i++){
          for (int i2 = 0; i2 < ArcsInner[i].Length; i2++){
               if (i2 != ArcsInner[i].Length-1){
                   Color c = (Color)Colors[i];
                   PaintArc(c, i2, i2 + 1, i);
               } 
          }
    }     
  }

  private void StartOutline(){
    OutLineColor.a = 1f;
    for (int i = 0; i < OutlineInner.Length; i++){
         for (int i2 = 0; i2 < OutlineInner[i].Length; i2++){
              if (i2 != OutlineInner[i].Length-1){
                  PaintOutline(OutLineColor, i2, i2 + 1, i);
                  PaintOutline2(OutLineColor, i2, i2 + 1, i);
              } 
         }
    }
  }

  private void StartInfoBox(){
    GL.Begin(GL.QUADS);
    if (showSquares) PaintSquares();
    GL.End();
  }
  
  private void PaintArc(Color c, int index1, int index2, int index3){
    Vector2 p1 = ArcsInner[index3][index1];
    Vector2 p2 = ArcsOuter[index3][index1];
    Vector2 p3 = ArcsInner[index3][index2];
    Vector2 p4 = ArcsOuter[index3][index2];
    GL.Color(c);
    ArcSet(p1, p2, p3, p4);
  }

  private void PaintOutline(Color c, int index1, int index2, int index3){
    Vector2 p1 = OutlineInner[index3][index1];
    Vector2 p2 = OutlineOuter[index3][index1];
    Vector2 p3 = OutlineInner[index3][index2];
    Vector2 p4 = OutlineOuter[index3][index2];
    GL.Color(c);
    ArcSet(p1, p2, p3, p4);
  }

  private void PaintOutline2(Color c, int index1, int index2, int index3){
    Vector2 p1 = OutlineInner2[index3][index1];
    Vector2 p2 = OutlineOuter2[index3][index1];
    Vector2 p3 = OutlineInner2[index3][index2];
    Vector2 p4 = OutlineOuter2[index3][index2];
    GL.Color(c);
    ArcSet(p1, p2, p3, p4);
  }

  private void ArcSet(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4){
    Vertex(p1, p2, p3);
    Vertex(p2, p3, p4);
  }

  private void Vertex(Vector2 p1, Vector2 p2, Vector2 p3){
    GL.Vertex3(p1.x, InvertY(p1.y), 0);
    GL.Vertex3(p2.x, InvertY(p2.y), 0);
    GL.Vertex3(p3.x, InvertY(p3.y), 0);
  }

  //Rotation
  public void SetRotation(int angle){

    angle = WrapAngle(angle);

    int diff = rotation - angle;
    if (diff == 0) return;
    if (diff >  0) RotateCCW(diff);
    if (diff <  0) RotateCW(diff);
  }

  public void RotateCW(){
    //
    RotateDiagram(1);
  }

  public void RotateCW(int value){
    //if (value <=0 ) return;
    RotateDiagram(value);
  }

  public void RotateCCW(){
    //
    RotateDiagram(-1);
  }

  public void RotateCCW(int value){
    if (value <=0 ) return; 
    RotateDiagram(value * -1);
  }

  private void RotateDiagram(int add){
    for (int i = 0; i < StartEnd.Length; i++){
         Vector2 ownCenter = Centers[i];
         Vector2 forMagnitude;
         int magnitude = 0;
         if (Center != ownCenter){ 
             forMagnitude = Center - ownCenter;
             magnitude = FloatToInt(forMagnitude.magnitude);
             Centers[i] = Center;
         }
         int s = FloatToInt(StartEnd[i].x + add);
         int e = FloatToInt(StartEnd[i].y + add);
         StartEnd[i] = new Vector2(WrapAngle(s), WrapAngle(e));
         if (Center != ownCenter){ 
             Vector2 id = ArcsInner[i][0];
             Vector2 od = ArcsOuter[i][0];
             Vector2 mi = Center - id;
             Vector2 mo = Center - od;
             int magn1 = FloatToInt(mi.magnitude);
             int magn2 = FloatToInt(mo.magnitude);
             if (magn1 < magn2) MoveArc(i, magnitude);
             if (magn1 > magn2) MoveArc(i, -magnitude);
         }
    }
    rotation = WrapAngle(rotation + add);
    FillDiagram();
    FillOutline();
  }

  //Movement
  public void MoveWithRadius(int index, int value){
    int i1 = FloatToInt(Radiuses[index].x);
    int o1 = FloatToInt(Radiuses[index].y);
    int i2 = FloatToInt(OutlineRadiuses[index].x);
    int o2 = FloatToInt(OutlineRadiuses[index].y);
    if (i1 + value < 0) return;
    Vector2 r1 = new Vector2(i1 + value, o1 + value);
    Vector2 r2 = new Vector2(i2 + value, o2 + value);
    Radiuses[index] = r1;
    OutlineRadiuses[index] = r2;
    RedrawOutline(index);
    RedrawArc(index);
  }

  public void MoveArc(int index, int value){
    if (value == 0 ) return;
    bool allow = true;
    int s = FloatToInt(StartEnd[index].x);
    int e = FloatToInt(StartEnd[index].y);
    //int i = FloatToInt(Radiuses[index].x);
    int o = FloatToInt(Radiuses[index].y);
    float x = Centers[index].x;
    float y = Centers[index].y;
    float a = AngleBetween(s, e);
    Vector2 ooo = GetPointF(a, o, x, y);
    Vector2 c = Centers[index];
    Vector2 v = ooo - c;
    v.Normalize();
    Vector2 toAdd = new Vector2(v.x * value, v.y * value);
    Vector2 newCenter = c + toAdd;
    Vector2 toCheck = newCenter - Center;
    toCheck.Normalize();

    if (toCheck == v) allow = true;
    Vector2 distance = newCenter - Center;
    if (distance.magnitude < 1.5 && value < 0) newCenter = Center;
    
    if (allow){
        Centers[index] = newCenter;
        RedrawOutline(index);
        RedrawArc(index);
    }
  }

  //InfoBox
  private void DoMyWindow(int windowID){
    showSquares = false;
    InfoBoxRects = new Rect[SectorsLength()];
    int x = 0;
    int y = 0;
    int offsetX = 0;
    int offsetY = infoFontSize + 10;
    int spaceBetween = 30;
    int icopy = -1;
    int maxwidth = 0;
    int newwidth = 0;
    int maxOffset = 0;
    Rect rect;
    for (int i = 0; i < Names.Count; i++){
         
         string text = "undefined";
         if (infoToShow == "percents")  text = (string)(Percents[i].ToString() + "%");
         if (infoToShow == "values")  text = (string)(Values[i].ToString());
         if (infoToShow == "names") text = (string)(Names[i].ToString());

         int count = 0;
         foreach (char ccc in text) { 
            if (System.Char.IsUpper(ccc)) count++;
         }

         infowidth = ((text.Length - count) * (infoFontSize/2)) + (count * ((infoFontSize/2) + (infoFontSize/4)))  + (infoFontSize/2);
         infoheight = (infoFontSize/2) + 10;
         
         icopy++;
         if (infoMode == "ver" || per_rows == 1){
             if (maxwidth < infowidth) maxwidth = infowidth;
             if (icopy + 1 == per_columns){
                 icopy = 0;
                 offsetY = infoFontSize + 10;
                 offsetX += maxwidth + (infoSquareSize);
             }
             x = offsetX + (infoSquareSize);
             y = offsetY;
             offsetY = y + infoheight + infoSquareSize;
         }

         if (infoMode == "hor" || per_columns == 1){
             if (icopy + 1 == per_rows){
                 icopy = 0;
                 offsetX = 0;
                 offsetY += infoheight + infoSquareSize;
             }
             x = offsetX + (infoSquareSize);
             y = offsetY;
             offsetX = x + infowidth + infoSquareSize;
             if (maxOffset < offsetX) maxOffset = offsetX;
         }
         if (i == SectorsAmount() - 1 && infoMode == "ver") newwidth = x + infowidth + spaceBetween;
         if (i == SectorsAmount() - 1 && infoMode == "hor") newwidth = maxOffset;
         rect = new Rect(x, y, infowidth, infoheight);
         GUI.Label (rect, text, infoStyle);
         InfoBoxRects[i] = rect;
    }
    if (newwidth < 100) newwidth = 100;
    windowRect.width = newwidth;
    showSquares  = true;
  }

  private void PaintSquares(){
    infoSquareSize = infoFontSize - 2;
    int offsetX = infoSquareSize + 2;
    int offsetY = infoSquareSize + 2;
    for (int i = 0; i < SectorsLength(); i++){
         Color c = (Color)Colors[i];
         Rect r = InfoBoxRects[i];
         int x = FloatToInt(r.x + getWindowX() - offsetX);
         int y = FloatToInt(r.y + getWindowY() + offsetY);
         InfoBoxQuad(c, x, FloatToInt(InvertY(y)), infoSquareSize, infoSquareSize);
    }
  }

  private void InfoBoxQuad(Color c, int x, int y, int width, int height){
    GL.Color(c);

    GL.Vertex3(x, y, 0);
    GL.Vertex3(x + width, y, 0);
    GL.Vertex3(x + width, y + height, 0);
    GL.Vertex3(x, y + height, 0);
  }

  //Pointers

  private void ShowPercents(){
    pointerStyle.fontSize = pointerFontSize;
    pointerStyle.normal.textColor = pointerFontColor;
    if (pointerFont != null) pointerStyle.font = pointerFont;
    if (pointerFont == null) pointerStyle.font = GUI.skin.font;
    for (int i = 0; i < Names.Count; i++){
         Rect r = InfoRects[i];
         string st = "undefined";
         if (pointermode == "percents")  st = (string)(Percents[i].ToString() + "%");
         if (pointermode == "values")  st = (string)(Values[i].ToString());
         if (pointermode == "names") st = (string)(Names[i].ToString());
         GUI.Label (r, st, pointerStyle);
    }
  }

  private void DrawPointers(){
    InfoRects = new Rect[Names.Count];
    for (int i = 0; i < Names.Count; i++){
         int s = FloatToInt(StartEnd[i].x);
         int e = FloatToInt(StartEnd[i].y);
         float x = Centers[i].x;
         float y = Centers[i].y;
         int o = FloatToInt(Radiuses[i].y);
         float a = AngleBetween(s, e);
         if (e == 0 && s == 359) a = s;
         if (s + 1 == e ) a = s;
         
         Vector2 original = GetPointF(a, o, x, y);
         Vector2 center = Centers[i];
         Vector2 vec = original - center;
         Vector2 per = Perpendicular(vec);
         per.Normalize();
         vec.Normalize();
         Vector2 p1 = original + per;
         Vector2 p2 = original - per;
         Vector2 p3 = p1 + (vec  * pointerLength);
         Vector2 p4 = p2 + (vec  * pointerLength);

         Color c = (Color)Colors[i];
         Pointer(c, p1, p2, p3, p4);
         Point2(p1, p2, p3, p4, c, i, a, vec);
    }
  }

  private void Point2(Vector2 p1, Vector2 p2, Vector2 p3, Vector3 p4, Color c, int i, float angle, Vector2 myvec){
    string name = "undefined";
    if (pointermode == "percents")  name = (string)(Percents[i].ToString() + "%");
    if (pointermode == "values")  name = (string)(Values[i].ToString());
    if (pointermode == "names") name = (string)(Names[i].ToString());
    int count = 0;
    foreach (char ccc in name) { 
      if (System.Char.IsUpper(ccc)) count++;
    }
    int width = ((name.Length - count) * (pointerStyle.fontSize/2)) + (count * ((pointerStyle.fontSize/2) + (pointerStyle.fontSize/4)))  + (pointerStyle.fontSize/2);
    int height = (pointerStyle.fontSize/2) + 10;
    int quarter = DefineQuarter(FloatToInt(angle));
    GetRect(p1, p2, p3, p4, c, width, height, quarter, i, myvec);
  }

  private void GetRect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, Color c, int width, int height, int quarter, int i, Vector2 myvec){
    Rect r = new Rect ();
    if (quarter == 1){
       p4 = new Vector2(p3.x, p3.y - 2);
       p1 = new Vector2(p3.x + pointerLength, p3.y);
       p2 = new Vector2(p4.x + pointerLength, p4.y);
       r = new Rect (FloatToInt(p1.x + 1), FloatToInt(p2.y), width, height);
    }
    if (quarter == 2){
       p3 = new Vector2(p4.x, p4.y - 2);
       p1 = new Vector2(p3.x - pointerLength, p3.y);
       p2 = new Vector2(p4.x - pointerLength, p4.y);
       r = new Rect (FloatToInt(p1.x - width), FloatToInt(p1.y - height/4), width, height);
    }
    if (quarter == 3){
       p4 = new Vector2(p3.x, p3.y + 2);
       p1 = new Vector2(p3.x - pointerLength, p3.y);
       p2 = new Vector2(p4.x - pointerLength, p4.y);
       r = new Rect (FloatToInt(p1.x - width), FloatToInt(p1.y - height/2), width, height);
    }
    if (quarter == 4){ 
       p3 = new Vector2(p4.x, p4.y + 2);
       p1 = new Vector2(p3.x + pointerLength, p3.y);
       p2 = new Vector2(p4.x + pointerLength, p4.y);
       r = new Rect (FloatToInt(p1.x + 1), FloatToInt(p2.y - height/2), width, height);
    }
    InfoRects[i] = r;
    Pointer(c, p1, p2, p3, p4);
  }

  private void Pointer(Color c, Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4){
    GL.Color(c);
    ArcSet(p1, p2, p3, p4);
  }

  //Utils
  private Color RandomColor(){
    Color r = new Color (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
    return r;
  }

  private Color EmptyColor(){
    //
    return emptyCol;
  }

  private int WrapAngle(int angle){
    while(angle >= degrees){
      angle-=degrees;
    }
    while(angle < 0){
      angle+=degrees;
    }
    //if (angle >= degrees) angle = angle - degrees;
    //if (angle < 0) angle = degrees + angle;
    return angle;
  }

  private float RoundFloat(float myfloat){
    float f = Mathf.Round(myfloat * 100f) / 100f;;
    return f;
  }

  private float GetSum(){
     float myFloat = 0f;
     for (int i = 0; i < Values.Count; i++){
          float f = (float)Values[i];
          myFloat += f;
     }
     return myFloat;
  }

  private float DisplayPercentage(double ratio) {
    string percentage = string.Format("Percentage is {0:0.0%}", ratio);
    avoiderror = percentage;
    avoiderrorInt = avoiderror.Length;
    avoiderrorInt++;
    float number = (float)ratio * 100.0f;
    return (Mathf.Floor(number * 100) / 100.0f);
  }

  private int GetAngleCenterLess(Vector2 myVec, int index){
    int x = FloatToInt(myVec.x);
    int y = FloatToInt(myVec.y);
    int a = (x - FloatToInt(Centers[index].x));
    int b = (y - FloatToInt(Centers[index].y));
    float angle = (Mathf.Atan2(b, a)*degrees/2 / Mathf.PI) * -1;
    if (angle < 0) angle = degrees/2 + (degrees/2 - (angle * -1));
    return FloatToInt(degrees - 1 - angle);
  }

  private float InvertY(float y){
    //
    return Screen.height - y;
  }

  private float AngleBetween(int a, int b){
    if (b == a) return a;
    if (b > a)  return(((b - a)/2) + a);
    if (b < a){
        int c = degrees - a;
        int d = (c + b)/2;
        int e = a + d;
        if (e >=degrees) e = e - degrees;
        return e;
    }
    return -1;
  }

  private Vector2 GetPoint(int a, int rad, int x, int y){
    float angle = (float)(a * Mathf.PI / 180.0);
    float pointX = Mathf.Cos(angle) * rad + x;
    float pointY = Mathf.Sin(angle) * rad + y;
    Vector2 p = new Vector2(pointX, pointY);
    return p;
  }

  private Vector2 GetPointF(float a, int rad, float x, float y){
    float angle = (float)(a * Mathf.PI / 180.0);
    float pointX = Mathf.Cos(angle) * rad + x;
    float pointY = Mathf.Sin(angle) * rad + y;
    Vector2 p = new Vector2(pointX, pointY);
    return p;
  }

  private bool InnerContains(Vector2 myVec,  int index){
    bool answer = ContainsWhatever(myVec, index, true);
    return answer;
  }

  private bool OuterContains(Vector2 myVec, int index){
    bool answer = ContainsWhatever(myVec, index, false);
    return answer;
  }

  private bool ContainsWhatever(Vector2 myVec, int index, bool inner){
    float x = myVec.x;
    float y = myVec.y;
    float a = (x - Centers[index].x) * (x - Centers[index].x);
    float b = (y - Centers[index].y) * (y - Centers[index].y);
    int c = FloatToInt(Radiuses[index].x) * FloatToInt(Radiuses[index].x);
    if (!inner) c = FloatToInt(Radiuses[index].y) * FloatToInt(Radiuses[index].y);
    if ((a + b) <= c )  return true;
    return false;
  }

  public bool ArcContains(Vector2 myVec, int index){
    if (OuterContains(myVec, index) && !InnerContains(myVec, index)){
        int ang =  GetAngleCenterLess(myVec, index);
        int min =  FloatToInt(StartEnd[index].x);
        int max =  FloatToInt(StartEnd[index].y);
        if (min < max){
            if (ang >=min && ang<= max) return true;
        }
        if (max < min){
            if ((ang <=max && ang>= 0) || (ang >=min && ang<= 360)) return true; 
        }      
    }
    return false;
  }

  public int Contains(Vector2 myVec){
    for (int i = 0; i < SectorsLength(); i++){
         if (ArcContains(myVec, i)) return i;
    }
    return -1;
  }

  private int DefineQuarter(int angle){
    if (angle >= 0   && angle <90)  return 1;
    if (angle >= 90  && angle <180) return 2;
    if (angle >= 180 && angle <270) return 3;
    if (angle >= 270 && angle <360) return 4;
    return 0;
  }

  private Vector2 Perpendicular(Vector2 vec){
  	Vector2 perpen = new Vector2(-(vec.y), vec.x);
  	return  perpen;
  }

  private int SectorsLength(){
    //
  	return Names.Count;
  }

  public int SectorsAmount(){
    //
    return SectorsLength();
  }

  private Vector2[] ArrayRemoveAt(Vector2[] arr, int index) {
    Vector2[] toReturn = new Vector2[arr.Length - 1];
    int l = -1;
    for (int i = 0; i < arr.Length - 1; i++){
         l++;
         if (i == index) l++;
         toReturn[i] = arr[l];
    }
    return toReturn;
  }

  private int FloatToInt(float myFloat){
    //
    return (int)Mathf.Round(myFloat);
  }

  private void makeMaterial(){
    mat = new Material( "Shader \"Lines/Colored Blended\" {" +
        "SubShader { Pass { " +
        " Blend SrcAlpha OneMinusSrcAlpha " +
        " ZWrite Off Cull Off Fog { Mode Off } " +
        " BindChannels {" +
        " Bind \"vertex\", vertex Bind \"color\", color }" +
        "} } }" );
        mat.hideFlags = HideFlags.HideAndDontSave;
        mat.shader.hideFlags = HideFlags.HideAndDontSave; 
  }

  //Errors Handling;
  private bool ZeroSumError(){
    if (GetSum() <= 0.000001f || GetSum() == 0f || GetSum() == 0) return true;
    return false;
  }

  private bool ZeroMembersError(){
    if (SectorsLength() == 0) return true;
    return false;
  }

  private void ThrowError(string errorText){
    //
    Debug.LogError(errorText);
  }

}
