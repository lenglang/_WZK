using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : MonoBehaviour
{
    protected Composite _parentNode = null;
    virtual public void Add(Component c)
    { }
    virtual public void Remove(Component c)
    { }
    virtual public Component GetChild(int n)
    {
        return null;
    }
    virtual public void UpdatePosition()
    { }
    virtual public void SetParent(Composite parent)
    {
        _parentNode = parent;
    }
    virtual public Composite GetParent()
    {
        return _parentNode;
    }
}
