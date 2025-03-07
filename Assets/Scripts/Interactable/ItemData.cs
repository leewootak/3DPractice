using UnityEngine;

// 아이템의 종류를 나타내는 열거형
public enum ItemType
{
    Resource,   // 자원 타입
    Consumable  // 소비 가능한 아이템 타입
}

// 소비 아이템의 종류를 나타내는 열거형
public enum ConsumableType
{
    Health, // 체력 회복
    Speed   // 속도 증가
}

// 소비 아이템의 데이터를 저장하는 클래스
[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type; // 소비 아이템의 종류
    public float value;         // 소비 아이템의 효과 값
}

// ScriptableObject를 상속받아 아이템 데이터를 저장하는 클래스
[CreateAssetMenu(fileName = "Item", menuName = "New Item")] // 생성 메뉴에 "New Item" 추가
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;     // 아이템의 이름
    public string description;     // 아이템의 설명
    public ItemType type;          // 아이템의 타입
    public GameObject dropPrefab;  // 아이템 드랍 시 사용할 프리팹

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; // 소비 아이템 관련 데이터 배열
}
