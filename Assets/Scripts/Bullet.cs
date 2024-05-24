using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (!m_IsMove)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, m_CurrPos);

        if (m_QueuePos.Count < 1 && distance < 0.05f)
        {
            GameObject.Destroy(gameObject);
            return;
        }

        if (m_QueuePos.Count > 0 && distance < 0.05f)
        {
            m_CurrPos = m_QueuePos.Dequeue();
        }

        transform.up = (m_CurrPos - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, m_CurrPos, 15f * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        for (int i = 1; i < m_ListPos.Count; i++)
        {
            Vector3 lastPos = m_ListPos[i - 1];
            Vector3 currPos = m_ListPos[i];

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(lastPos, currPos);
        }
    }

    public void Move(Vector3 start, Vector3 mid, Vector3 end)
    {
        m_QueuePos.Clear();
        m_ListPos.Clear();
        for (float i = 0; i <= 1.0f; i += Time.fixedDeltaTime)
        {
            Vector3 p1 = Vector3.Lerp(start, mid, i);
            Vector3 p2 = Vector3.Lerp(mid, end, i);
            Vector3 p = Vector3.Lerp(p1, p2, i);

            m_ListPos.Add(p);
            m_QueuePos.Enqueue(p);
        }

        m_CurrPos = m_QueuePos.Dequeue();
        transform.up = (m_CurrPos - transform.position).normalized;
        transform.position = start;
        m_IsMove = true;
    }

    private bool m_IsMove = false;
    private Vector3 m_CurrPos = Vector3.zero;
    private List<Vector3> m_ListPos = new List<Vector3>();
    private Queue<Vector3> m_QueuePos = new Queue<Vector3>();
}
