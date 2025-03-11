using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // 싱글톤 인스턴스를 저장할 변수
    private static CharacterManager instance;

    // 외부에서 싱글톤 인스턴스에 접근하기 위한 프로퍼티
    public static CharacterManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 인스턴스가 아직 생성되지 않은 경우,
                // 새 GameObject를 생성하고 CharacterManager 컴포넌트를 추가하여 인스턴스로 할당
                instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return instance;
        }
    }

    // 관리할 플레이어 객체를 저장하는 변수
    private Player player;

    // 플레이어 객체에 접근하기 위한 프로퍼티
    public Player Player
    {
        get { return player; }
        set { player = value; }
    }

    private void Awake()
    {
        // 아직 인스턴스가 할당되지 않았다면 현재 오브젝트를 인스턴스로 설정
        if (instance == null)
        {
            instance = this;
            // 씬 전환 시에도 해당 오브젝트가 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 인스턴스가 존재하고 현재 오브젝트가 기존 인스턴스와 다를 경우 중복 생성된 오브젝트 파괴
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
