using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image hpBar;

    private void Start()
    {
        curValue = startValue;
    }

    private void Update()
    {
        hpBar.fillAmount = GetPercentage();
    }

    public void Add(float amount)
    {
        // �� ���� ���� �� (ex. maxValue���� Ŀ���� maxValue)
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        // �� ���� ū �� (ex. 0���� �۾����� 0)
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}