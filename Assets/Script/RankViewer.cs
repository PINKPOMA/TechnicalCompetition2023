using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] rankS;

    private void Start()
    {
        SetView();
    }
    public void SetView()
    {
       for(int i = 4; i >= 0; i--)
        {
            rankS[i].text = $"#{ 5 - i}: {ScoreManager.instance.rankBoard[i].name} {ScoreManager.instance.rankBoard[i].score}";
        }
    }
}
