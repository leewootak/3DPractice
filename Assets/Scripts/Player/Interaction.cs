using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    // 상호작용 오브젝트를 체크하는 주기
    public float checkRate = 0.05f;
    // 마지막으로 체크한 시간을 저장하는 변수
    private float lastCheckTime;
    // 레이캐스트를 통해 체크할 최대 거리
    public float maxCheckDistance;
    // 레이캐스트 대상이 되는 레이어를 지정하는 마스크
    public LayerMask layerMask;

    // 현재 상호작용 가능한 게임 오브젝트
    public GameObject curInteractGameObject;
    // 현재 상호작용 가능한 오브젝트의 인터페이스
    private IInteractable curInteractable;

    // 상호작용 프롬프트 객체
    public TextMeshProUGUI promptText;
    // 메인 카메라 참조
    private Camera camera;

    // 게임 시작 시 한 번 호출되는 초기화 함수
    void Start()
    {
        // 현재 씬의 메인 카메라를 찾아 할당
        camera = Camera.main;
    }

    // 매 프레임마다 호출되는 업데이트 함수
    void Update()
    {
        // 마지막 체크 이후 경과 시간이 checkRate보다 크면 상호작용 체크를 진행
        if (Time.time - lastCheckTime > checkRate)
        {
            // 현재 시간을 lastCheckTime에 저장
            lastCheckTime = Time.time;

            // 화면 중앙에서 카메라를 통해 레이 생성
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            // 지정한 최대 거리와 레이어 마스크를 사용하여 레이캐스트 수행
            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // 레이캐스트에 탐지된 오브젝트가 현재 저장된 오브젝트와 다를 경우
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    // 탐지된 오브젝트로 현재 상호작용 대상을 업데이트
                    curInteractGameObject = hit.collider.gameObject;
                    // 해당 오브젝트에서 IInteractable 컴포넌트를 가져옴 (없으면 null)
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    // 상호작용 프롬프트 텍스트를 갱신
                    SetPromptText();
                }
            }
            else // 레이캐스트에 탐지된 오브젝트가 없을 경우
            {
                // 상호작용 대상 초기화 및 프롬프트 텍스트 비활성화
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    // 상호작용 가능한 오브젝트의 프롬프트 텍스트를 설정하는 함수
    private void SetPromptText()
    {
        // 프롬프트 텍스트 UI 활성화
        promptText.gameObject.SetActive(true);
        // 현재 상호작용 대상의 인터페이스에서 상호작용 프롬프트 메시지를 받아와 텍스트로 표시
        promptText.text = curInteractable.GetInteractPrompt();
    }

    // 입력 시스템을 통해 상호작용 입력 이벤트가 발생하면 호출되는 함수
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // 입력 이벤트가 시작되었고, 현재 상호작용 대상이 존재할 경우
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            // 상호작용 대상의 OnInteract() 함수를 호출하여 상호작용 수행
            curInteractable.OnInteract();
            // 상호작용 후 대상 및 인터페이스 초기화, 프롬프트 텍스트 숨김
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
