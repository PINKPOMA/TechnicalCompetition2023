using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using Random = UnityEngine.Random;
using Sprite = UnityEngine.Sprite;

public class Monster : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] int moveSpeed;
    public int myScore;
    public float shootDelay;
    public GameObject bullet;
    public GameObject deadP;
    public GameObject[] items;
    [SerializeField] private AudioSource audio;

    public Sprite[] sprites;
    public SpriteRenderer _sprite;

  
    private void Update()
    {
        _sprite = GetComponent<SpriteRenderer>();
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void Destroyed(Collider2D collision, int value)
    {
        audio.Play();
        Instantiate(deadP, transform.position, quaternion.identity);
        switch(Random.Range(0,10))
        {
            case 0:
                Instantiate(items[0], transform.position, quaternion.identity);
                break;
            case 1:
                Instantiate(items[1], transform.position, quaternion.identity);
                break;
            case 2:
                Instantiate(items[2], transform.position, quaternion.identity);
                break;
            case 3:
                Instantiate(items[3], transform.position, quaternion.identity);
                break;
            case 4:
                Instantiate(items[3], transform.position, quaternion.identity);
                break;
            case 5:
                Instantiate(items[3], transform.position, quaternion.identity);
                break;
            case 6:
                Instantiate(items[0], transform.position, quaternion.identity);
                break;
            case 7:
                Instantiate(items[4], transform.position, quaternion.identity);
                break;

        }
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score += value;
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            if(gameObject.name != "Meteo(Clone)" && gameObject.name != "EnemyBulletPlus" && gameObject.name != "EnemyBullet")
            {
                StopCoroutine(ChangeSprite());
                StartCoroutine(ChangeSprite());
            }
            audio.Play();
            Destroy(collision.gameObject);
            hp--;
            if (hp <= 0)
                Destroyed(collision, myScore);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private IEnumerator ChangeSprite()
    {
        _sprite.sprite = sprites[1];
        yield return new WaitForSeconds(0.05f);
        _sprite.sprite = sprites[0];
    }
}
