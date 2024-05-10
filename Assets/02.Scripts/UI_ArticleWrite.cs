using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public UI_ArticleList UI_ArticleList;
    public Toggle NoticeToggleUI;
    public TMP_InputField ContentInputFieldUI;

    private void Start()
    {
        UI_ArticleList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickExitButton()
    {
        UI_ArticleList.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickCompleteButton()
    {
        ArticleType articleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Normal;
        string content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        ArticleManager.Instance.Write(articleType, content);
        UI_ArticleList.Show();
        ContentInputFieldUI.text = string.Empty;
        NoticeToggleUI.isOn = false;
        gameObject.SetActive(false);
        /*ArticleManager.Instance.Write(NoticeToggleUI.isOn? ArticleType.Notice:ArticleType.Normal, ContentInputFieldUI.text.ToString());
        UI_ArticleList.GetComponent<UI_ArticleList>().Refresh();
        ContentInputFieldUI.text = string.Empty;
        NoticeToggleUI.isOn = false;
        UI_ArticleList.gameObject.SetActive(true);
        gameObject.SetActive(false);*/
    }
}