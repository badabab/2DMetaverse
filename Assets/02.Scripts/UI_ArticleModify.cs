using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleModify : MonoBehaviour
{
    public static UI_ArticleModify instance {  get; private set; }
    public UI_ArticleList UI_ArticleList;
    public Toggle NoticeToggleUI;
    public TMP_InputField ContentInputFieldUI;

    private Article _article;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Show(Article article)
    {       
        _article = article;
        NoticeToggleUI.isOn = _article.ArticleType == ArticleType.Notice;
        ContentInputFieldUI.text = _article.Content;
        gameObject.SetActive(true);
    }

    public void OnClickExitButton()
    {
        UI_ArticleList.Instance.Show();
        gameObject.SetActive(false);
    }

    public void OnClickCompleteButton()
    {
        _article.ArticleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Normal;
        _article.Content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(_article.Content))
        {
            return;
        }

        ArticleManager.Instance.Replace(_article);
        ArticleManager.Instance.FindAll();
        UI_ArticleList.Instance.Show();
        gameObject.SetActive(false);
    }
}
