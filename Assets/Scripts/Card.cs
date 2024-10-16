using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [HideInInspector] public int idx;

    public GameObject front;
    public GameObject back;

    public UnityAction<Card> OnCardOpen;

    bool isOpening;
    Animator _animator;
    public Image frontImage;

    Coroutine _fileCoroutine;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        CloseCard();

    }

    public void Setting(int newIdx, Sprite newSprite)
    {
        idx = newIdx;
        frontImage.sprite = newSprite;
        isOpening = false;
    }

    public void OpenCard()
    {
        if (GameManager.Instance.IsPlaying() == false)
            return;

        if (isOpening == true)
            return;
        
        _animator.SetBool("IsOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        isOpening = true;
        OnCardOpen?.Invoke(this);

    }

    public void CloseCard()
    {

        Invoke("CloseCard_Internal", 1.0f);
        //back.GetComponent<Image>().color = Color.gray;
    }

    private void CloseCard_Internal()
    {

        isOpening = false;
        _animator.SetBool("IsOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCard_Internal", 1.0f);
    }

    private void DestroyCard_Internal()
    {
        front.SetActive(false);
        back.SetActive(false);
    }

    public static bool operator ==(Card lhs, Card rhs) => lhs.idx == rhs.idx;
    public static bool operator !=(Card lhs, Card rhs) => !(lhs == rhs);
}
