using UnityEngine;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
    Text text;
    Animator animator;

    void Awake()
    {
        text = GetComponent<Text>();    
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void SetText(int matchFlag)
    {
        text.text = matchFlag == 0 ? "실패..." : "성공!";
    }

    public void PlayMatchAnimation(bool isCollect)
    {
        string animationName = isCollect ? "SuccessMatch" : "FailMatch";
        animator.Play(animationName,0, 0);
    }

    public void EndMatch()
    {
        GameManager.Instance.EndMatch();
        gameObject.SetActive(false);
    }
}
