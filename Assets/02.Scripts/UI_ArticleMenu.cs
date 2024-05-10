using UnityEngine;

public class UI_ArticleMenu : MonoBehaviour
{
    private Article _article;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(Article article)
    {
        _article = article;
        gameObject.SetActive(true);
    }

    public void OnClickModifyButton()
    {
        UI_ArticleModify.instance.Show(_article);
        gameObject.SetActive(false);
    }
    public void OnClickDeleteButton()
    {
        ArticleManager.Instance.Delete(_article.Id);
        ArticleManager.Instance.FindAll();
        gameObject.SetActive(false);
        UI_ArticleList.Instance.Refresh();
    }

    public void OnClickBackground()
    {
        gameObject.SetActive(false);
    }
}