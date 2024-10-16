using UnityEngine;

[CreateAssetMenu(fileName = "newStageData", menuName = "Card Game/StageData")]
public class StageData : ScriptableObject
{
    [Header("카드배치")]
    public int cardsCount = 12;
    public int rowCount = 3;
    public int colCount = 4;
    
    [Header("게임 시간 설정")]
    public float hurryUpTime = 10f;
    public float gameTime = 60f;
}
