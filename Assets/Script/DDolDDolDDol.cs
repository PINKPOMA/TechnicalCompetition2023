using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DDolDDolDDol : MonoBehaviour
{
    [SerializeField] int dir;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * dir * 20 * Time.deltaTime);
    }
}






// �ڲ� ������ �� ���� ������ �� ���� �� ���� �� ����� �� ���� ��鸮�� �ʰ� ���� �� �̰߰� ���� �� �� �Ⱦ���
// �ѷ��ڽ���~ ����~ �ѷ��ڽ���~ �������~
