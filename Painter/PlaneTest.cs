using UnityEngine;
using System.Collections;
using WZK;
public class PlaneTest : MonoBehaviour {

    private PainterManager _painterManager;
    private float _countTime = 0;//计时
    private float _limitTime = 1f / 24f;//时间限制
    [Header("原图")]
    public Texture _texture;
    [Header("脏图")]
    public Texture _dirtyTexture;
    [Header("白图")]
    public Texture _whiteTexture;
    void Start ()
    {
        _painterManager = GameObject.FindObjectOfType<PainterManager>();
        _painterManager.SetBrushColor(Color.green);
        GameObject obj = GameObject.Find("直升机").gameObject;
        MeshRenderer[] mrs = obj.GetComponentsInChildren<MeshRenderer>();
        _painterManager._materialList[0].mainTexture = _dirtyTexture;
        _painterManager._materialList[2].SetTexture("_MainTex1", _texture);
        for (int i = 0; i < mrs.Length; i++)
        {
            mrs[i].gameObject.AddComponent<MeshCollider>();
            mrs[i].material.SetTexture("_MainTex2", _painterManager._renderTextureList[0]);
            mrs[i].material.SetTexture("_MaskTex3", _painterManager._renderTextureList[1]);
        }
        //obj.GetComponent<SkinnedMeshRenderer>().material.SetTexture("_DirtyTex", _painterManager._currentRenderTexture);
    }
	
	// Update is called once per frame
	void Update () {
        _countTime += Time.deltaTime;
        if (_countTime > _limitTime)
        {
            _countTime = 0;
            _painterManager.DoAction();
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("单色"))
        {
            RenderTexture renderTexture = _painterManager._renderTextureList[2];
            RenderTexture.active = renderTexture;
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex.Apply();
            RenderTexture.active = null;
            _painterManager._materialList[0].mainTexture = tex;

            _painterManager._materialList[1].mainTexture = _whiteTexture;

            _painterManager.CleanBrush();
            _painterManager.SetData(0);
            _painterManager.SetBrushColor(Color.green);
        }
        if (GUILayout.Button("彩色"))
        {
            _painterManager.SaveTexture();
            _painterManager.CleanBrush();
            _painterManager.SetData(1);
            _painterManager.SetBrushColor(Color.black);
        }
    }
}
