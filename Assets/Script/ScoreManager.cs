using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public class Rank
{
    public string name;
    public int score;
}
public class ScoreManager : MonoBehaviour
{
    public Rank[] rankBoard = new Rank[5];

    public static ScoreManager instance;

    public string nowPlayerName;
    public int nowPlayerScore;

    public bool isRanking;

    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void SetRank()
    {
        for(int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 4 - i; j++)
            {
                if (rankBoard[j].score > rankBoard[j+1].score)
                {
                    var temp = rankBoard[j].score;
                    rankBoard[j].score = rankBoard[j + 1].score;
                    rankBoard[j + 1].score = temp;

                    var stemp = rankBoard[j].name;
                    rankBoard[j].name = rankBoard[j + 1].name;
                    rankBoard[j + 1].name = stemp;
                }
            }
        }

        GameObject.FindWithTag("View").GetComponent<RankViewer>().SetView();
    }
    public void InputRank()
    {
        rankBoard[0].score = nowPlayerScore;
        rankBoard[0].name = nowPlayerName;
        SetRank();
    }
}
