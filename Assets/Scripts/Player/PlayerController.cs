using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public LayerMask groundLayerMask;  // ���̾� ����
    private Vector2 curMovementInput;  // ���� �Է� ��

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;  // �ּ� �þ߰�
    public float maxXLook;  // �ִ� �þ߰�
    public float lookSensitivity; // ī�޶� �ΰ���
    private float camCurXRot;

    private Vector2 mouseDelta;  // ���콺 ��ȭ��

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ����� �߾ӿ� ����
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    private void Move()
    {
        // ���� �Է��� y ���� z ��(forward, �յ�)�� ���Ѵ�.
        // ���� �Է��� x ���� x ��(right, �¿�)�� ���Ѵ�.
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;  // ���⿡ �ӷ��� �����ش�.
        dir.y = rb.velocity.y;  // y���� velocity(��ȭ��)�� y ���� �־��ش�.

        rb.velocity = dir;  // ����� �ӵ��� velocity(��ȭ��)�� �־��ش�.
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void CameraLook()
    {
        // ���콺 �������� ��ȭ��(mouseDelta)�� y(�� �Ʒ�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �� �Ʒ��� ȸ���Ϸ��� rotation�� x ���� �־��ش�. ->
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // ���콺 �������� ��ȭ��(mouseDelta)�� x(�¿�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �¿�� ȸ���Ϸ��� rotation�� y ���� �־��ش�.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            // �÷��̾��� ��ġ���� �ణ ���� �ø� ��, ��, ��, ������, ���� �������� 0.2��ŭ �������Ͽ� Raycast �߻�
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }
}