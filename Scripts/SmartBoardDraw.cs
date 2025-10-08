using UnityEngine;

public class SmartBoardDraw
{
    public Camera drawCamera;//هتتعدل عشان الvr
    public RenderTexture renderTexture;
    public Texture2D texture;
    public RaycastHit hit;
    public Color drawColor = Color.black;

    void Start(){
        texture = new Texture2D(renderTexture.width,renderTexture.height);
    }
    void Update(){
        if(Input.GetMouseButton(0)){//هيتعدل للvr
            Ray ray = drawCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit)){
                if(hit.transform.name == "Smartboard"){
                    Vector2 uv = hit.textureCoord;
                    int x = (int) (uv.x * texture.width);
                    int y = (int) (uv.y * texture.height);
                    
                    texture.SetPixel(x,y,drawColor);
                    texture.Apply();
                }
            }
        }
    }
}
