using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_CharacterController;
    protected PlayerAnimation m_PlayerAnimation;
    private Vector3 m_Movement;
    [SerializeField] private float m_Speed = 5;

    private float m_Gravity = -9.81f;
    [SerializeField] private float m_TurnSmoothing = 10;
    protected const string _Horizontal = "Horizontal";
    protected const string _Vertical = "Vertical";

    // Start is called before the first frame update
    void Start()
    {
        m_CharacterController ??= GetComponent<CharacterController>();
        m_PlayerAnimation ??= GetComponentInChildren<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Movement = new Vector3(Input.GetAxis(_Horizontal), 0, Input.GetAxis(_Vertical));
        var _IsMoving = m_Movement.x != 0 || m_Movement.z != 0;

        if (m_PlayerAnimation.IsHit)
        {
            m_Movement = Vector3.zero;
            return;
        }
        Movement();
        RotateToDirection(_IsMoving);

        // Animation
        m_PlayerAnimation.PlayRunAnimation(_IsMoving);

    }

    protected void Movement()
    {
        if (!m_CharacterController.isGrounded)
            m_Movement.y += m_Gravity * Time.deltaTime;

        // Movement 
        m_CharacterController.Move(m_Movement * m_Speed * Time.deltaTime);
    }
    protected void RotateToDirection(bool isMoving)
    {
        if (isMoving)
        {
            var _Rot = Quaternion.LookRotation(new Vector3(m_Movement.x, 0, m_Movement.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, _Rot, m_TurnSmoothing * Time.deltaTime);
        }
    }
}


