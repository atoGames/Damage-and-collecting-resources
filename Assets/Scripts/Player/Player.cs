using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected LayerMask m_ItemLayer;
    protected Transform m_Hand;
    public GameObject m_AxePrefab;
    protected TakeItem m_TakeItem;
    private float m_MaxRadius = 5, m_MinRadius = 3;
    private void Start()
    {
        // Try Find the layer
        m_ItemLayer = 1 << LayerMask.NameToLayer("Item");
        // Find the hand
        m_Hand ??= GetComponentsInChildren<Transform>().ToList().Find(hand => hand.name == "Hand");
        // Create weapon
        CreateWeapon(m_AxePrefab);
    }
    private void Update()
    {
        var _Colliders = Physics.OverlapSphere(transform.position, m_MaxRadius, m_ItemLayer);

        foreach (var col in _Colliders)
        {
            // Only if col have TakeItem script
            if (col.TryGetComponent(out m_TakeItem))
            {
                var _IsItemClose = Vector3.Distance(transform.position, col.transform.position) < m_MinRadius;

                if (Input.GetKeyDown(KeyCode.E) && _IsItemClose)
                {
                    // Add item to inventory
                    // ...
                    Destroy(m_TakeItem.gameObject);
                }
                // Show ["TO TAKE PRESS E"] if player close to item , if he not close  hide it
                m_TakeItem?.ShowTakeItemButton(_IsItemClose);
            }
        }
    }
    public void CreateWeapon(GameObject wp) => Instantiate(wp, m_Hand);
}
