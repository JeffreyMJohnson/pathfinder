using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

    public Vector2[] neighbors = new Vector2[4];
    Grid gridScript;
    public bool mIsVisited = false;

	// Use this for initialization
	void Start () {
        gridScript = FindObjectOfType<Grid>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        gridScript.HandleSquareClick(this);
    }
}
