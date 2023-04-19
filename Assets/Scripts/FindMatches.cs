using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private void AddToListAndMatch(GameObject dot)
    {
        if(!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }

        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1,GameObject dot2,GameObject dot3)
    {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);

        for(int i = 0;i < board.width;i++)
        {
            for(int j = 0;j < board.height;j++)
            {
                GameObject currentDot = board.allDots[i,j];

                if(currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();

                    if(i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1,j];
                        GameObject rightDot = board.allDots[i + 1,j];
                        
                        if(leftDot != null && rightDot != null)
                        {
                            if(leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                                GetNearbyPieces(leftDot,currentDot,rightDot);
                        }
                    }

                    if(j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i,j + 1];
                        GameObject downDot = board.allDots[i,j - 1];

                        if(upDot != null && downDot != null)
                        {
                            if(upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                                GetNearbyPieces(upDot,currentDot,downDot);
                        }
                    }
                }
            }
        }
    }
}
