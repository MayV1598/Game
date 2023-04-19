using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    private GameObject otherDot;
    private Board board;
    private FindMatches findMatches;
    private EndGameManager endGameManager;
    public float swipeAngle = 0;
    public float swipeResist = 1f;
    public bool isMatched = false;
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    
    void Start()
    {
        endGameManager = FindObjectOfType<EndGameManager>();
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
    }
    
    void Update()
    {
        if(isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            Color currentColor = mySprite.color;
            mySprite.color = new Color(1f,1f,1f,.2f);
            board.currentState = GameState.wait;
        }

        targetX = column;
        targetY = row;

        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = Vector2.Lerp(transform.position,tempPosition,.6f);

            if(board.allDots[column,row] != this.gameObject)
                board.allDots[column,row] = this.gameObject;
            findMatches.FindAllMatches();
        }
        else
        {
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = tempPosition;
        }

        if(Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = Vector2.Lerp(transform.position,tempPosition,.6f);

            if(board.allDots[column,row] != this.gameObject)
                board.allDots[column,row] = this.gameObject;
            findMatches.FindAllMatches();
        }
        else
        {
            tempPosition = new Vector2(transform.position.x,targetY);
            transform.position = tempPosition;
        }
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentState = GameState.move;
            }
            else
            {

                board.DestroyMatches();
            }
            otherDot = null;
        }
    }

    private void OnMouseDown()
    {
        if(board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if(board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    // Вычисление угла направления свайпа
    void CalculateAngle()
    {
        if(Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y,finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePiecesActual(Vector2 direction)
    {
        otherDot = board.allDots[column + (int)direction.x,row + (int)direction.y];
        previousRow = row;
        previousColumn = column;

        if(otherDot != null)
        {
            otherDot.GetComponent<Dot>().column += -1 * (int)direction.x;
            otherDot.GetComponent<Dot>().row += -1 * (int)direction.y;
            column += (int)direction.x;
            row += (int)direction.y;
            StartCoroutine(CheckMoveCo());
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    // Движение фигуры
    void MovePieces()
    {
        if(swipeAngle <= 45 && swipeAngle > -45 && column < board.width - 1)
        {
            // Свайп в право
            MovePiecesActual(Vector2.right);
        }
        else if(swipeAngle <= 135 && swipeAngle > 45 && row < board.height - 1)
        {
            // Свайп в верх
            MovePiecesActual(Vector2.up);
        }
        else if((swipeAngle <= -135 || swipeAngle > 135) && column > 0)
        {
            // Свайп в лево
            MovePiecesActual(Vector2.left);
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Свайп в низ
            MovePiecesActual(Vector2.down);
        }
    }

    void FindMatches()
    {
        if(column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1,row];
            GameObject rightDot1 = board.allDots[column + 1,row];

            if(leftDot1 != null && rightDot1 != null)
            {
                if(leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }

        if(row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column,row + 1];
            GameObject downDot1 = board.allDots[column,row - 1];

            if(upDot1 != null && downDot1 != null)
            {
                if(upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}
