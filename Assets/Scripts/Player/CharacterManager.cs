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
                // 인스턴스가 아직 생성되지 않은 경우 새 GameObject를 생성하고 CharacterManager 컴포넌트를 추가하여 할당
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
        // 인스턴스가 아직 할당되지 않은 경우 현재 오브젝트를 싱글톤 인스턴스로 할당
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 존재하고 현재 오브젝트가 그 인스턴스와 다를 경우 중복 생성된 오브젝트 파괴
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
