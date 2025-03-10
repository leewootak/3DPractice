using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float dotValue;
    public Image hpBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        hpBar.fillAmount = GetPercentage();
    }

    public IEnumerator DotAdd(float amount)
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.7f);
            // 둘 중의 작은 값 (ex. maxValue보다 커지면 maxValue)
            curValue = Mathf.Min(curValue + amount, maxValue);
        }
        yield break;
    }

    public void Subtract(float amount)
    {
        // 둘 중의 큰 값 (ex. 0보다 작아지면 0)
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}