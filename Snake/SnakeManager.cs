using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeManager : MonoBehaviour {
    [Header("头")]
    public Composite _head;
    [Header("身体")]
    public Composite _body;

    private List<Transform> _snakeList = new List<Transform>();

    public List<Transform> _lineList = new List<Transform>();
	// Use this for initialization
    //private Composite _
	void Start ()
    {
        _snakeList.Add(_head.transform);
        Composite parentNode = _head.GetComponent<Composite>();
        for (int i = 0; i < _lineList.Count; i++)
        {
            GameObject body = Instantiate(_body.gameObject);
            body.transform.localPosition = new Vector3(0, 0.3f, -(i+1) * 0.5f);
            Composite c = body.GetComponent<Composite>();
            parentNode.Add(c);
            parentNode = c;
            _snakeList.Add(body.transform);
            c._index = i;
            body.SetActive(false);
        }
        _head.GetComponent<Composite>().UpdatePosition();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float x = Input.GetAxis("Horizontal");
        _head.transform.Rotate(0, x * 30 * Time.deltaTime, 0);
        _head.transform.position+=5*Time.deltaTime* _head.transform.forward;
        _head.GetComponent<Composite>().UpdatePosition();

        Vector3 p;
        for (int i = 0; i < _lineList.Count; i++)
        {
            p = _snakeList[i].transform.position;
            p.y = 0;
            //p.y -= i*0.03f;
            _lineList[i].transform.position = p;
            //_lineList[i].transform.localScale = _snakeList[i].transform.localScale;
        }
        _lineList[0].transform.localEulerAngles = _snakeList[0].transform.localEulerAngles;
        for (int i = 1; i < _lineList.Count; i++)
        {
            _lineList[i].LookAt(_lineList[i - 1]);
        }
    }
}
