using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public Transform mSquarePrefab;
    public Text mStartPositionLabel;
    public Text mGoalPositionLabel;

    Square mStartTile = null;
    Square mGoalTile = null;

    // Use this for initialization
    void Start()
    {
        //move camera
        Camera.main.transform.Translate(new Vector3(5, 4.5f, 0));

        mStartPositionLabel.gameObject.SetActive(false);
        mGoalPositionLabel.gameObject.SetActive(false);

        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 10; col++)
            {
                GameObject squareInstance = Instantiate(mSquarePrefab, new Vector3(row, col, 0), Quaternion.identity) as GameObject;

            }
        }
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

    public void Foo(Square clicked)
    {
       if (mStartTile == null)
       {
           mStartTile = clicked;
           mStartTile.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
       }
       else if(mGoalTile == null)
       {
           mGoalTile = clicked;
           mGoalTile.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
       }
    }
}
