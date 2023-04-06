using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : Monster
{
    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        Instantiate(bullet, transform.position, Quaternion.identity);
        Instantiate(bullet, transform.position, Quaternion.Euler(0,0,45));
        Instantiate(bullet, transform.position, Quaternion.Euler(0,0,-45));
        StartCoroutine(Shoot());
    }

}
