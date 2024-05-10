using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public GameObject UI_ArticleList;
    public Toggle NoticeToggleUI;
    public TMP_InputField ContentInputFieldUI;

    private void Start()
    {
        UI_ArticleList.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }

    public void OnClickCompleteButton()
    {
        ArticleManager.Instance.Write(ContentInputFieldUI.text.ToString(),NoticeToggleUI);
        UI_ArticleList.GetComponent<UI_ArticleList>().Refresh();
        ContentInputFieldUI.text = string.Empty;
        NoticeToggleUI.isOn = false;
        gameObject.SetActive(false);
    }
}