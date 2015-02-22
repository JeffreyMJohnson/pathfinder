using UnityEngine;
using System.Collections;
using System;

public class Square : MonoBehaviour, IComparable<Square> {

    public Vector2[] neighbors = new Vector2[4];
    Grid gridScript;
    public bool mIsVisited = false;
    public int mWeight = 1;
    public int mGScore = int.MaxValue;
    public Square mPathParentNode;


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

    public int CompareTo(Square other)
    {
        if (other == null) return 1;
        return mGScore.CompareTo(other.mGScore);
    }
}
