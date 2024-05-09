using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public GameObject UI_ArticleList;
    public Toggle Toggle;
    public TMP_InputField InputField;

    private void Start()
    {
        UI_ArticleList.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }

    public void OnClickFinishButton()
    {
        ArticleManager.Instance.Write(InputField.text.ToString(),Toggle);
        UI_ArticleList.GetComponent<UI_ArticleList>().Refresh();
        InputField.text = string.Empty;
        Toggle.isOn = false;
        gameObject.SetActive(false);
    }
}