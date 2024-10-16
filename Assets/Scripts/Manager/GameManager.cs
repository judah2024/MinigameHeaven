using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance;

    [Header("Card Game")]
    public Board board;
    [SerializeField] private StageData stageData;
    public StageData StageData => stageData;

    [Header("Sound")] 
    public AudioClip matchClip;
    public AudioClip flipClip;

    Dictionary<GameState.Type, GameState> _gameStates;
    GameState.Type _curState;
    public GameState.Type CurState => _curState;

    public event UnityAction<float> OnTimerUpdated;
    public event UnityAction<float> OnReadyUpdated;
    public event UnityAction<bool> OnGameEnded;
    public event UnityAction<int> OnCountUpdated;
    public event UnityAction<bool> OnMatchCard;

    float _curTime;
    int _matchCount;
    int _collectCount;
    List<Card> _openedCards = new List<Card>(2);

     void Awake()
     {
         _instance = this;

         InitializeStates();
     }

     void Start()
     {
         _curTime = stageData.gameTime;
         _matchCount = 0;
         board.gameObject.SetActive(true);

         Time.timeScale = 1;
         ChangeState(GameState.Type.Ready);

     }

     void Update()
     {
         _gameStates[_curState]?.Update();
     }

     void InitializeStates()
     {
         _gameStates = new Dictionary<GameState.Type, GameState>
         {
             { GameState.Type.Ready, new ReadyState(this) },
             { GameState.Type.Playing, new PlayState(this) },
             { GameState.Type.Matching, new MatchingState(this) },
             { GameState.Type.GameEnd, new GameEndState(this) }
         };
     }

     /// <summary>
     /// 상태패턴의 상태전이 함수
     /// </summary>
     /// <param name="newState"> 새 상태 타입 </param>
     public void ChangeState(GameState.Type newState)
     {
         Debug.Log($"{_curState} -> {newState}");
         _gameStates[_curState]?.Exit();
         _gameStates[newState]?.Enter();
         _curState = newState;
         
     }

     public void ReadyUpdated(float seconds)
     {
         OnReadyUpdated?.Invoke(seconds);
     }

     public void UpdateTimer()
     {
         _curTime -= Time.deltaTime;
         float remainTime = _curTime - Time.deltaTime;
         _curTime = Mathf.Clamp(remainTime, 0, stageData.gameTime);
         OnTimerUpdated?.Invoke(_curTime);

         if (_curTime == 0)
         {
             OnGameEnded?.Invoke(false);
             ChangeState(GameState.Type.GameEnd);
         }
     }

     public void EndGame()
     {
         board.gameObject.SetActive(false);

         Time.timeScale = 0;
     }

     public void OpenCard(Card card)
     {
         if (IsPlaying() == false)
             return;
         
         AudioManager.instance.PlaySoundEffect(flipClip);
         _openedCards.Add(card);
         if (_openedCards.Count == _openedCards.Capacity)
         {
             ChangeState(GameState.Type.Matching);
         }
     }

     public void CheckMatch()
     {
         if (_openedCards[0] == _openedCards[1])
         {
             SuccessMatch();
         }
         else
         {
             FailMatch();
         }
         
         _matchCount++;
         OnCountUpdated?.Invoke(_matchCount);
         _openedCards.Clear();
     }

     public void EndMatch()
     {
         GameState.Type nextState = IsCollected() ? GameState.Type.GameEnd : GameState.Type.Playing;

         if (IsCollected() == true)
         {
             ChangeState(GameState.Type.GameEnd);
             OnGameEnded?.Invoke(true);
         }
         else
         {
             ChangeState(GameState.Type.Playing);
         }
     }

     public bool IsHurryUp() => _curTime < stageData.hurryUpTime;
     
     public bool IsCollected() => _collectCount == stageData.cardsCount;
     public bool IsPlaying() => _curState == GameState.Type.Playing;

     void SuccessMatch()
     {
         foreach (Card card in _openedCards)
         {
             card.DestroyCard();
         }

         _collectCount += 2;
         AudioManager.instance.PlaySoundEffect(matchClip);
         OnMatchCard?.Invoke(true);
     }

     void FailMatch()
     {
         foreach (Card card in _openedCards)
         {
             card.CloseCard();
         }
         OnMatchCard?.Invoke(false);
     }

}
