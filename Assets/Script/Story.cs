using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField] string story1;
    [SerializeField] string story2;

    [SerializeField] private Text storyText1;
    [SerializeField] private Text storyText2;
    [SerializeField] private GameObject StartButton;

    private bool isSkip;


    private void Start()
    {
        story1.Split();
        story2.Split();
        StartCoroutine(StoryView1());
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isSkip = true;
            storyText1.text = story1;
            storyText2.text = story2;
            StartButton.SetActive(true);
        }
    }
    private IEnumerator StoryView1()
    {
        foreach (var story in story1)
        {
            if (!isSkip)
            {
                storyText1.text += story;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (!isSkip)
        {
            StartCoroutine(StoryView2());
        }
    }
    private IEnumerator StoryView2()
    {
        foreach (var story in story2)
        {

            storyText2.text += story;
            yield return new WaitForSeconds(0.1f);

        }
        StartButton.SetActive(true);
    }
}
