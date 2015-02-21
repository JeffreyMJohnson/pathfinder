using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public ArrayList neighbors;
    Grid gridScript;

	// Use this for initialization
	void Start () {
        gridScript = FindObjectOfType<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        gridScript.Foo(this);
    }
}
