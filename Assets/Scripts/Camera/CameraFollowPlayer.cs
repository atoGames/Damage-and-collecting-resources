using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    protected Player m_Player;
    [SerializeField] private Vector3 m_Offset = new Vector3(0f, 10f, -10f);
    [SerializeField] private Quaternion m_Rotition = Quaternion.Euler(45f, 0f, 0f);
    [SerializeField] private float Speed = 1.5f;

    private void Awake()
    {
        m_Player ??= FindObjectOfType<Player>();

        transform.rotation = m_Rotition;

    }
    void LateUpdate() => Follow_Player();

    private void Follow_Player()
    {
        if (!m_Player)
            return;

        var _Position = m_Player.transform.position + m_Offset;

        _Position.x = Mathf.Lerp(transform.position.x, _Position.x, Time.deltaTime * Speed);
        _Position.y = transform.position.y;
        _Position.z = Mathf.Lerp(transform.position.z, _Position.z, Time.deltaTime * Speed);

        transform.position = _Position;

    }

}