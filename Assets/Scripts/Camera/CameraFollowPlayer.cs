using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    protected Player m_Player;
    [SerializeField] protected Vector3 m_Offset = new Vector3(0f, 10f, -10f);
    [SerializeField] protected Quaternion m_Rotition = Quaternion.Euler(45f, 0f, 0f);
    [SerializeField] protected float m_Speed = 5f;

    protected void Awake()
    {
        // Find player
        m_Player ??= FindObjectOfType<Player>();
        // Set rotation
        transform.rotation = m_Rotition;
    }
    protected void LateUpdate() => Follow_Player();
    protected void Follow_Player()
    {
        if (!m_Player)
            return;

        var _Pos = m_Player.transform.position + m_Offset;

        _Pos = new Vector3(Mathf.Lerp(transform.position.x, _Pos.x, Time.deltaTime * m_Speed)
                           , _Pos.y = transform.position.y,
                           Mathf.Lerp(transform.position.z, _Pos.z, Time.deltaTime * m_Speed)
                        );

        transform.position = _Pos;

    }

}