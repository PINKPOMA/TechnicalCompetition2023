using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

[Serializable]
public class Mon
{
    public GameObject[] mob;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private Mon[] monster;
    [SerializeField] private GameObject[] bossMonster;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Sprite[] backgroundImages;
    [SerializeField] private float createDelay;
    [SerializeField] private int createCount;
    
    public bool isStage2;

    public int score;
    [SerializeField] private int storege;
    [SerializeField] private int spawnCount;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nowScoreText;
    [SerializeField] private TextMeshProUGUI nowStageText;
    [SerializeField] private float stagePlayTime;
    [SerializeField] private GameObject clearUI;
    [SerializeField] private TextMeshProUGUI stagePlayTimeText;

   

    private void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(LevelUp());
        nowStageText.text = "Stage1";
    }



    private void Update()
    {
        stagePlayTime += Time.deltaTime;
        Cheat();
        nowScoreText.text = $"Score: {score}";
    }

    IEnumerator Spawn()
    {
        storege++;

        if (storege > spawnCount * 4)
        {
            for (int i = 0; i < Random.Range(1, createCount); i++)
            {
                Instantiate(monster[isStage2 ? 1 : 0].mob[Random.Range(0, monster[isStage2 ? 1 : 0].mob.Length)],
                    new Vector2(Random.Range(0, 3), 6.5f), Quaternion.identity);
            }
            spawnCount++;
        }
        if (storege >= 90)
        {
            Instantiate(bossMonster[isStage2 ? 1 : 0], new Vector2(0, 7), Quaternion.identity);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Spawn());
        }
    }
    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            if (isStage2)
            {
                nowStageText.text = "Stage1";

                isStage2 = false;
                stagePlayTime = 0;
                createCount = 1;
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score = 0;
                background.sprite = backgroundImages[0];
                storege = 0;
                spawnCount = 1;
            }
            else if (!isStage2)
            {
                nowStageText.text = "Stage2";
                isStage2 = true;
                stagePlayTime = 0;
                createCount = 1;
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score = 0;
                background.sprite = backgroundImages[1];
                storege = 0;
                spawnCount = 1;
            }
        }
    }

    private IEnumerator LevelUp()
    {
        createCount++;
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        StartCoroutine(LevelUp());
    }


    public void StageClear()
    {
        clearUI.SetActive(true);
        scoreText.text = $"{score} + " +
            $"{GameObject.FindWithTag("Player").GetComponent<PlayerController>().hp / 10} = " +
            $"{score + GameObject.FindWithTag("Player").GetComponent<PlayerController>().hp / 10}";

        stagePlayTimeText.text = $"{(int)stagePlayTime}s";

        score += GameObject.FindWithTag("Player").GetComponent<PlayerController>().hp / 10;
        Time.timeScale = 0f;
    }

    public void NextButton()
    {
        if(!isStage2)
        {
            nowStageText.text = "Stage2";
            clearUI.SetActive(false);
            isStage2 = true;
            stagePlayTime = 0;
            storege = 0;
            spawnCount = 1;
            createCount = 1;
            Time.timeScale = 1f;
            background.sprite = backgroundImages[1];
            StartCoroutine(Spawn()); 
        }
        else
        {
            Time.timeScale = 1f;
            if (ScoreManager.instance.rankBoard[0].score < score)
            {
                ScoreManager.instance.isRanking = true;
                ScoreManager.instance.nowPlayerScore = score;
                SceneManager.LoadScene("End");
            }
            else
            {
                ScoreManager.instance.isRanking = false;
                SceneManager.LoadScene("End");
            }
        }
    }
}
