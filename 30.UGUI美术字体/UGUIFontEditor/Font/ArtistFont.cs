using UnityEngine;
using System.Collections;
using UnityEditor;
using LitJson;

public class ArtistFont : MonoBehaviour
{
    public class JsonString
    {
        public Frames[] frames;
        public string file;
    }
    public class Frames
    {
        //public int x,y,w,h,offX,offY,sourceW,sourceH;
    }
    public static void BatchCreateArtistFont()
    {
        string dirName = "";
        string fntname = EditorUtils.SelectObjectPathInfo(ref dirName).Split('.')[0];
        Debug.Log(fntname);
        Debug.Log(dirName);

        string fntFileName = dirName + fntname + ".fnt";

        Font CustomFont = new Font();
        {
            AssetDatabase.CreateAsset(CustomFont, dirName + fntname + ".fontsettings");
            AssetDatabase.SaveAssets();
        }

        TextAsset BMFontText = null;
        {
            BMFontText = AssetDatabase.LoadAssetAtPath(fntFileName, typeof(TextAsset)) as TextAsset;
        }

        //Debug.Log(BMFontText.ToString().Replace("\"", "\'"));
        Debug.Log(BMFontText.text);
        JsonString js = JsonMapper.ToObject<JsonString>(BMFontText.text);
        //JsonString js = JsonUtility.FromJson<JsonString>(BMFontText.text);
        Debug.Log(js.file);


        BMFont mbFont = new BMFont();
        BMFontReader.Load(mbFont, BMFontText.name, BMFontText.bytes);  // 借用NGUI封装的读取类
        CharacterInfo[] characterInfo = new CharacterInfo[mbFont.glyphs.Count];
        for (int i = 0; i < mbFont.glyphs.Count; i++)
        {
            BMGlyph bmInfo = mbFont.glyphs[i];
            CharacterInfo info = new CharacterInfo();
            info.index = bmInfo.index;

            Rect r = new Rect();
            r.x = (float)bmInfo.x / (float)mbFont.texWidth;
            r.y = 1 - (float)bmInfo.y / (float)mbFont.texHeight;
            r.width = (float)bmInfo.width / (float)mbFont.texWidth;
            r.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;

            info.uvBottomLeft = new Vector2(r.xMin, r.yMin);
            info.uvBottomRight = new Vector2(r.xMax, r.yMin);
            info.uvTopLeft = new Vector2(r.xMin, r.yMax);
            info.uvTopRight = new Vector2(r.xMax, r.yMax);

            //info.uv.x = (float)bmInfo.x / (float)mbFont.texWidth;
            //info.uv.y = 1 - (float)bmInfo.y / (float)mbFont.texHeight;
            //info.uv.width = (float)bmInfo.width / (float)mbFont.texWidth;
            //info.uv.height = -1f * (float)bmInfo.height / (float)mbFont.texHeight;
            r.x = (float)bmInfo.offsetX;
            r.y = (float)bmInfo.offsetY;
            r.width = (float)bmInfo.width;
            r.height = (float)bmInfo.height;
            //info.vert.x = (float)bmInfo.offsetX;
            //info.vert.y = (float)bmInfo.offsetY;
            //info.vert.width = (float)bmInfo.width;
            //info.vert.height = (float)bmInfo.height;
            info.minX = (int)r.xMin;
            info.maxX = (int)r.xMax;
            info.minY = (int)r.yMax;
            info.maxY = (int)r.yMin;
            info.advance = (int)bmInfo.advance;
            characterInfo[i] = info;
        }
        CustomFont.characterInfo = characterInfo;


        string textureFilename = dirName + mbFont.spriteName + ".png";
        Material mat = null;
        {
            Shader shader = Shader.Find("Transparent/Diffuse");
            mat = new Material(shader);
            Texture tex = AssetDatabase.LoadAssetAtPath(textureFilename, typeof(Texture)) as Texture;
            mat.SetTexture("_MainTex", tex);
            AssetDatabase.CreateAsset(mat, dirName + fntname + ".mat");
            AssetDatabase.SaveAssets();
        }
        CustomFont.material = mat;
    }
}
