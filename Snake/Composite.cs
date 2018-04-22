using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Composite : Component
{
    protected List<Component> _childrenList = new List<Component>();
    public int _index = 1;
    public override void Add(Component c)
    {
        _childrenList.Add(c);
        c.SetParent(this);
    }
    public override Component GetChild(int n)
    {
        if (n > 0 && n <= _childrenList.Count)
        { return _childrenList[n - 1]; }
        return null;
    }
    public override void UpdatePosition()
    {
        for (int i = 0; i < _childrenList.Count; i++)
        {
            _childrenList[i].UpdatePosition();
        }
    }

}
