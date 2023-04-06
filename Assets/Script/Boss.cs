using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Sprite = UnityEngine.Sprite;

public class Boss : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] private float patonCool;
    [SerializeField] private TextMeshProUGUI bossHpText;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioSource audio;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private SpriteRenderer _sprite;
    public void BossBooting()
    {
        _sprite = GetComponent<SpriteRenderer>();
        bossHpText = GameObject.FindWithTag("BossHp").GetComponent<TextMeshProUGUI>();
        bossHpText.text = $"bossHp: {hp}";
        StartCoroutine(Paton());
    }

    private IEnumerator Paton()
    {
        switch(Random.Range(0,gameObject.name=="Boss1(Clone)"? 5: 7))
        {
            case 0:
                StartCoroutine(Fun1()); break;
            case 1:
                StartCoroutine(Fun2()); break;
            case 2:
                StartCoroutine(Fun3()); break;
            case 3:
                StartCoroutine(Fun4()); break;
            case 4:
                StartCoroutine(Fun5()); break;
            case 5:
                StartCoroutine(Fun6()); break;
            case 6:
                StartCoroutine(Fun7()); break;
        }

        yield return new WaitForSeconds(patonCool);
        StartCoroutine(Paton());
    }

    private IEnumerator Fun1()
    {
        patonCool = 3f;
        for (int i = 0; i < 120;i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 6));
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 36; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 10));
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 90; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 8));
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 60; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 5));
        }
    }
    private IEnumerator Fun2()
    {
        patonCool = 0.6f;
        for (int i = 0; i < 60; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 6));
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator Fun3()
    {
        patonCool = 1f;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 36));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Fun4()
    {
        patonCool =0.8f;
        for (int i = 0; i < 80; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i *4));
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator Fun5()
    {
        patonCool = 0.6f;
        for (int i = 0; i < 6; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 60));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Fun6()
    {
        patonCool = 1.8f;
        for (int i = 0; i < 9; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * -8));
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 8));
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 16));
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * -16));
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Fun7()
    {
        patonCool = 1.5f;
        for (int i = 0; i < 360; i++)
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * -2));
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i * 2));
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
        
            audio.Play();
            hp -= 1;
            bossHpText.text = $"bossHp: {hp}";
            if(hp <= 0)
            {
                bossHpText.text = " ";
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score += 1000;
                GameObject.FindWithTag("GameManager").GetComponent<GameManager>().StageClear();
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator ChangeSprite()
    {
        _sprite.sprite = sprites[1];
        yield return new WaitForSeconds(0.05f);
        _sprite.sprite = sprites[0];
    }
}
