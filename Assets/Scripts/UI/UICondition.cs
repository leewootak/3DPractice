using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    // 플레이어의 체력 정보를 관리하는 Condition 컴포넌트 참조
    public Condition health;

    void Start()
    {
        // CharacterManager의 싱글톤 인스턴스를 통해 플레이어의 UICondition을 현재 UICondition으로 설정
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }
}