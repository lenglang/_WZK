using UnityEngine;
using System.Collections;
using WZK;
public class ChickTest : MonoBehaviour
{
    [Header("脏图")]
    public Texture _dirtyTexture;
    private PainterManager _painterManager;
    private float _countTime = 0;//计时
    private float _limitTime = 1f / 24f;//时间限制
	// Use this for initialization
	void Start ()
    {
        _painterManager = _painterManager = GameObject.FindObjectOfType<PainterManager>();
        _painterManager._currentMaterial.mainTexture = _dirtyTexture;
        _painterManager.SetBrushColor(Color.white);
        _painterManager._uvIndex = 2;
        GameObject obj = GameObject.Find("小鸡/xiaoji").gameObject;
        obj.GetComponent<SkinnedMeshRenderer>().material.SetTexture("_DirtyTex", _painterManager._currentRenderTexture);
	}
	
	// Update is called once per frame
	void Update ()
    {
        _countTime += Time.deltaTime;
        if (_countTime > _limitTime)
        {
            _countTime = 0;
            _painterManager.DoAction();
        }
	}
    /// <summary>
    /// 判断完成程度
    /// </summary>
    public void JudeCompleteProgress()
    {
        //每50个绘制完判断一次
        //_dirtyTex = (Texture2D)PainterManager.Instance._dirtyMaterial.mainTexture;
        //Color[] colors = _dirtyTex.GetPixels();
        //Debug.Log(colors.Length);
        //ThreadHelper.Instance.QueueOnThreadPool((state) =>
        //{
        //    int count = 0;
        //    for (int i = 0; i < colors.Length; i++)
        //    {
        //        if (colors[i] == Color.white) count++;
        //    }
        //    Debug.Log("清理面积:" + (count * 100 / colors.Length) + "%");
        //    ThreadHelper.Instance.QueueOnMainThread(delegate ()
        //    {
        //        if ((int)(count * 100 / colors.Length) >= 50)
        //        {

        //        }
        //    });
        //});
    }
}
