using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Monster
{
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        Instantiate(bullet, transform.position, Quaternion.identity);
        StartCoroutine(Shoot());
    }
}
