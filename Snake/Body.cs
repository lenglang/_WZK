using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : Composite
{

    // Use this for initialization
    public override void UpdatePosition()
    {
        Composite myParent = GetParent();
        Vector2 parentPosition = new Vector2(myParent.transform.localPosition.x, myParent.transform.localPosition.z);
        Vector2 myPosition = new Vector2(transform.localPosition.x, transform.localPosition.z);

        //标记
        //float angle = MathTool.GetAngle2(myPosition, parentPosition);
        transform.LookAt(myParent.transform);

        float distance = Vector3.Distance(myPosition, parentPosition);
        myPosition = Vector2.Lerp(parentPosition, myPosition,0.5f/distance);
        Vector3 mp;
        mp.x = myPosition.x;
        mp.z = myPosition.y;
        mp.y = transform.localPosition.y;
        transform.position = mp;
        base.UpdatePosition();
    }
}
