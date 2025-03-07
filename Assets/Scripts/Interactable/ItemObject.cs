using UnityEngine;

// 인터랙션 가능한 객체를 위한 인터페이스
public interface IInteractable
{
    public string GetInteractPrompt();  // 인터랙션 시 표시할 텍스트
    public void OnInteract();           // 인터랙션 실행 함수
}

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;  // 아이템 데이터

    public string GetInteractPrompt()
    {
        // 아이템 이름과 설명을 조합하여 반환
        return $"{data.displayName}\n{data.description}";
    }

    public void OnInteract()
    {
        // 플레이어에게 아이템 데이터를 전달 후 아이템 오브젝트 제거
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
