using UnityEngine;
using System.Collections;

public class ArrowDirection : MonoBehaviour {

    // Use this for initialization
    public Transform _target;
    public float _angle;
    public Vector3 _rotation;
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //标记 MathTool
        //Vector2 from = new Vector2(transform.localPosition.x, transform.localPosition.y);
        //Vector2 to = new Vector2(_target.localPosition.x, _target.localPosition.y);
        //_angle = MathTool.GetAngle2(from, to);
        //_rotation = transform.localEulerAngles;
        //_rotation.z = _angle;
        //transform.localEulerAngles = _rotation;
    }
}
