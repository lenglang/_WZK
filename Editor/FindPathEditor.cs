using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

/// <summary>
/// </summary>
public class FindPathEditor
{


    /// <summary>
    /// 1、  Ctrl+ 鼠标 点选  父对象 和子对象（子对象的子对象*n的子对象都可以）。 
    /// 2、选中父子两个对象后快捷键 Ctrl+Shift+C 就可以复制了。或者右键点击按钮 Create Other/Copy Find Child Path。
    /// 3、复制成功失败日志都会打印。
    /// 4、如果复制成功 直接 切换到 代码处 Ctrl+V 粘贴 一下就OK 啦~。
    /// </summary>
    [MenuItem("GameObject/Create Other/Copy Find Child Path _%#_ C")]
    static void CopyFindChildPath()
    {

        Object[] objAry = Selection.objects;
        //Debug.Log(objAry.Length);

        if (objAry.Length == 2)
        {
            GameObject gmObj0 = (GameObject)objAry[0];
            GameObject gmObj1 = (GameObject)objAry[1];
            List<Transform> listGameParent0 = new List<Transform>(gmObj0.transform.GetComponentsInParent<Transform>(true));
            List<Transform> listGameParent1 = new List<Transform>(gmObj1.transform.GetComponentsInParent<Transform>(true));
            System.Text.StringBuilder strBd = new System.Text.StringBuilder("");
            //gmObj0.transform.FindChild("");
            //string findCode = "gmObj0"
            if (listGameParent0.Contains(gmObj1.transform))
            {
                int startIndex = listGameParent0.IndexOf(gmObj1.transform);
                Debug.Log(startIndex);
                for (int i = startIndex; i >= 0; i--)
                {
                    if (i != startIndex)
                    {
                        strBd.Append(listGameParent0[i].gameObject.name).Append(i != 0 ? "/" : "");
                    }
                    
                }
            }
            
            if (listGameParent1.Contains(gmObj0.transform))
            {
                int startIndex = listGameParent1.IndexOf(gmObj0.transform);
                for (int i = startIndex; i >= 0; i--)
                {
                    if (i != startIndex)
                    {
                        strBd.Append(listGameParent1[i].gameObject.name).Append(i != 0 ? "/" : "");
                    }
                    
                }
            }

            TextEditor textEditor = new TextEditor();
            textEditor.text = "\"" + strBd.ToString() + "\"";// "hello world";
            textEditor.OnFocus();
            textEditor.Copy();
            string colorStr = strBd.Length > 0 ? "<color=green>" : "<color=red>";
            Debug.Log(colorStr + "复制：【\"" + strBd.ToString() + "\"】" + "</color>");
        }


    }
}
