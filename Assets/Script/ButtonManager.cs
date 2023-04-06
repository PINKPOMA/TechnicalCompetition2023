using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private GameObject Tropes;

    private void Start()
    {
        if(ScoreManager.instance.isRanking)
        {
            inputfield.gameObject.SetActive(true);
            Tropes.gameObject.SetActive(true);
        }
    }
    public void InputName()
    {
       ScoreManager.instance.nowPlayerName= inputfield.text;
       ScoreManager.instance.InputRank();
        inputfield.gameObject.SetActive(false);
    }
}
