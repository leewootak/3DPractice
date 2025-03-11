using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    // 현재 체력, 최대 체력, 시작 체력, 회복량, HP바 UI 이미지
    public float curValue;
    public float maxValue;
    public float startValue;
    public float dotValue;
    public Image hpBar;

    private void Start()
    {
        curValue = startValue;
    }

    // 매 프레임마다 HP바의 채워진 정도를 현재 체력 비율로 업데이트
    private void Update()
    {
        hpBar.fillAmount = GetPercentage();
    }

    // 일정 간격으로 체력을 회복하는 DOT(DoT) 효과를 주는 코루틴
    public IEnumerator DotAdd(float amount)
    {
        for (int i = 0; i < 4; i++)
        {
            // 0.7초 대기 후 체력 회복, 최대 체력을 넘지 않도록 설정
            yield return new WaitForSeconds(0.7f);
            curValue = Mathf.Min(curValue + amount, maxValue);
        }
    }

    // 체력을 감소시키는 함수 (체력이 0 미만으로 내려가지 않도록 보정)
    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    // 현재 체력 비율(0~1)을 반환하는 함수
    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}
