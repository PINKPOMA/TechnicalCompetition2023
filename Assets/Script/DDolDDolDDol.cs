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






// 자꾸 아찔한 이 느낌 위험한 이 느낌 내 손을 꼭 잡아줘 아 원츄 흔들리지 않게 조금 더 뜨겁게 나를 꼭 껴 안아줘
// 롤러코스터~ 우우우~ 롤러코스터~ 우우우우우우~
