using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Transform mSquarePrefab;
    public Text mStartPositionLabel;
    public Text mGoalPositionLabel;
    public Button mDFSearchButton;
    public Button mResetButton;

    Color WHITE = new Color(1, 1, 1, 1);

    Transform[,] mSquareList = new Transform[10, 10];

    Square mStartTile = null;
    Square mGoalTile = null;

    // Use this for initialization
    void Start()
    {
        //move camera
        Camera.main.transform.Translate(new Vector3(5, 4.5f, 0));

        mStartPositionLabel.gameObject.SetActive(false);
        mGoalPositionLabel.gameObject.SetActive(false);

        CreateGrid();

    }

    void CreateGrid()
    {
        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                Transform squareInstance = Instantiate(mSquarePrefab, new Vector3(row, col, 0), Quaternion.identity) as Transform;
                mSquareList[row, col] = squareInstance;
                Square script = squareInstance.GetComponent<Square>();
                //north neighbor
                if (row + 1 < 10)
                {
                    script.neighbors[0] = new Vector2(row + 1, col);
                }
                else
                {
                    script.neighbors[0] = new Vector2(-1, -1);
                }
                //south neighbor
                if (row - 1 >= 0)
                {
                    script.neighbors[1] = new Vector2(row - 1, col);
                }
                else
                {
                    script.neighbors[1] = new Vector2(-1, -1);
                }
                //east neighbor
                if (col + 1 < 10)
                {
                    script.neighbors[2] = new Vector2(row, col + 1);
                }
                else
                {
                    script.neighbors[2] = new Vector2(-1, -1);
                }
                //west neighbor
                if (col - 1 >= 0)
                {
                    script.neighbors[3] = new Vector2(row, col - 1);
                }
                else
                {
                    script.neighbors[3] = new Vector2(-1, -1);
                }
            }
        }


    }

    public void DFSButtonWrapper()
    {
        mResetButton.interactable = false;
        StartCoroutine(DFSearchButtongHandle());
    }

    IEnumerator DFSearchButtongHandle()
    {

        Stack<Square> nodeStack = new Stack<Square>();
        nodeStack.Push(mStartTile);

        while (nodeStack.Count != 0)
        {
            Square current = nodeStack.Pop();
            if (current.mIsVisited)
            {
                continue;
            }
            current.mIsVisited = true;
            if (current != mStartTile && current != mGoalTile)
            {
                current.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
            }


            if (current == mGoalTile)
            {
                mResetButton.interactable = true;
                yield break;
            }
            foreach (Vector2 neighborPos in current.neighbors)
            {
                if (neighborPos.x >= 0 && neighborPos.y >= 0)
                {
                    nodeStack.Push(mSquareList[(int)neighborPos.x, (int)neighborPos.y].GetComponent<Square>());
                }
            }
            yield return new WaitForSeconds(2 * Time.deltaTime);
        }

    }

    public void ResetButtonHandler()
    {
        foreach (Transform tile in mSquareList)
        {
            tile.GetComponent<SpriteRenderer>().color = WHITE;
            tile.GetComponent<Square>().mIsVisited = false;
        }

        mStartTile = null;
        mGoalTile = null;

        mStartPositionLabel.gameObject.SetActive(false);
        mGoalPositionLabel.gameObject.SetActive(false);
        mDFSearchButton.interactable = false;
        mResetButton.interactable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (mStartTile != null)
        {
            mStartPositionLabel.text = "Start Position: (" + mStartTile.transform.localPosition.x + ", " +
                mStartTile.transform.localPosition.y + ")";
            mStartPositionLabel.gameObject.SetActive(true);
        }
        if (mGoalTile != null)
        {
            mGoalPositionLabel.text = "Goal Position: (" + mGoalTile.transform.localPosition.x + ", " +
                mGoalTile.transform.localPosition.y + ")";
            mGoalPositionLabel.gameObject.SetActive(true);
        }
    }

    public void HandleSquareClick(Square clicked)
    {
        if (mStartTile == null)
        {
            mStartTile = clicked;
            mStartTile.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
            mResetButton.interactable = true;
        }
        else if (mGoalTile == null && clicked != mStartTile)
        {
            mGoalTile = clicked;
            mGoalTile.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            mDFSearchButton.interactable = true;
        }
    }
}
