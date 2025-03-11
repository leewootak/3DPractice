using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foothold : MonoBehaviour
{
    private const float BoundSize = 7f;
    private const float BlockMovingSpeed = 3.5f;

    float blockTransition = 0f; // 블럭 이동 애니메이션에 사용되는 시간 변수
    public Transform block;

    void Update()
    {
        // 시간에 따라 블럭 이동 진행률 증가
        blockTransition += Time.deltaTime * BlockMovingSpeed;

        // PingPong 함수를 사용하여 블럭이 정해진 범위 내에서 왕복 이동하도록 함
        float movePosition = Mathf.PingPong(blockTransition, BoundSize);

        block.localPosition = new Vector3(movePosition, 10, 10);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
