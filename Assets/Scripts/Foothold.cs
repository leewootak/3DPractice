using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foothold : MonoBehaviour
{
    // 이동 범위와 속도를 상수로 정의
    private const float BoundSize = 7f;
    private const float BlockMovingSpeed = 3.5f;

    // 블럭 이동 애니메이션에 사용되는 진행 시간 변수
    float blockTransition = 0f;
    // 이동할 블럭의 Transform 컴포넌트 참조
    public Transform block;

    void Update()
    {
        // 매 프레임마다 진행 시간을 업데이트
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        // PingPong 함수를 사용해 0부터 BoundSize까지 왕복하는 위치 계산
        float movePosition = Mathf.PingPong(blockTransition, BoundSize);

        // 계산된 위치로 블럭의 로컬 위치 갱신 (y와 z축은 고정)
        block.localPosition = new Vector3(movePosition, 10, 10);
    }

    // 플레이어와 충돌이 시작되면 플레이어를 블럭의 자식으로 설정
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    // 플레이어와의 충돌이 종료되면 플레이어의 부모 참조를 해제
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
