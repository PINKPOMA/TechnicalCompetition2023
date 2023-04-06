using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpUi : MonoBehaviour
{

    [SerializeField] private GameObject helpUI;
    [SerializeField] private GameObject lbutton;
    [SerializeField] private GameObject rbutton;
    [SerializeField] private Sprite[] helpImages;
    [SerializeField] private Image helpBG;
    [SerializeField] private int page;
    
    public void HelpOpen()
    {
        helpUI.SetActive(true);
    }

    public void PageUp()
    {   
        helpBG.sprite = helpImages[1];
        rbutton.SetActive(false);
       lbutton.SetActive(true);
    }
    public void PageDown()
    {
        helpBG.sprite = helpImages[0];
        lbutton.SetActive(false);
        rbutton.SetActive(true);
    }
    public void HelpClose()
    {
        helpUI.SetActive(false);
    }

}
