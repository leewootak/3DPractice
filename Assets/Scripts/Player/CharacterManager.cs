using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                // �ν��Ͻ��� ���� �������� ���� ��� �� GameObject�� �����ϰ� CharacterManager ������Ʈ�� �߰��Ͽ� �Ҵ�
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    private Player player;
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private void Awake()
    {
        // �ν��Ͻ��� ���� �Ҵ���� ���� ��� ���� ������Ʈ�� �̱��� �ν��Ͻ��� �Ҵ�
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // �̹� �ν��Ͻ��� �����ϰ� ���� ������Ʈ�� �� �ν��Ͻ��� �ٸ� ��� �ߺ� ������ ������Ʈ �ı�
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
