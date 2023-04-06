using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int hp;
    [SerializeField] private int oil;

    [SerializeField] private bool isGod;

    [SerializeField] private bool skill1IsActive;
    [SerializeField] private bool skill2IsActive;

    [SerializeField] private float skill1Cool;
    [SerializeField] private float skill2Cool;

    [SerializeField] private int skill1Count;
    [SerializeField] private int skill2Count;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float maxYPos;
    [SerializeField] private float maxXPos;

    [SerializeField] private float shotDelay;
    [SerializeField] private float shootDelays;


    [SerializeField] private Image boom;
    [SerializeField] private Image healPack;

    [SerializeField] GameObject shild;
    [SerializeField] GameObject[] bullets;
    [SerializeField] int bulletLevel;

    [SerializeField] Camera mainCam;

    [SerializeField] TextMeshProUGUI skill1CoolText;
    [SerializeField] TextMeshProUGUI skill2CoolText;

    //Text
    [SerializeField] TextMeshProUGUI skill1CountText;
    [SerializeField] TextMeshProUGUI skill2CountText;

    [SerializeField] Slider hpSlider;
    [SerializeField] Slider oilSlider;

    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI shildText;

    //Audio

    [SerializeField] private AudioClip[] hitS;
    [SerializeField] private AudioClip shootS;
    [SerializeField] private AudioClip uiS;
    [SerializeField] private AudioClip healS;
    [SerializeField] private AudioClip itemS;
    [SerializeField] private AudioClip boomS;
    [SerializeField] private AudioSource playerSource;

    [SerializeField] private GameObject boomParticle;

    [SerializeField] private Animator animator;



    private void Start()
    {
        animator = GetComponent<Animator>();
        oilSlider.value = oil;
        hpSlider.value = hp;
        skill1CountText.text = $"Count: {skill1Count}";
        skill2CountText.text = $"Count: {skill2Count}";
        StartCoroutine(OilDown());
    }

    private void Update()
    {
        Move();
        Shoot();
        Skill();
        Cheat();
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enem in enemys)
            {
                if (enem.name != "EnemyBullet" || enem.name != "EnemyBulletPlus")
                {
                    Destroy(enem);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            bulletLevel = 4;
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            StopCoroutine(Skill1());
            StopCoroutine(Skill2());
            healPack.color = new Color(1, 1, 1, 1f);
            skill1Cool = 5;
            skill1Count = 5;
            skill1IsActive = false;
            skill1CountText.text = $"Count: {skill1Count}";
            skill1CoolText.text = " ";

            boom.color = new Color(1, 1, 1, 1f);
            skill2Cool = 5;
            skill2Count = 5;
            skill2IsActive = false;
            skill2CountText.text = $"Count: {skill1Count}";
            skill2CoolText.text = " ";
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            hp = 100;
            hpSlider.value = hp;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            oil = 100;
            oilSlider.value = oil;
        }
    }

    private IEnumerator OilDown()
    {
        yield return new WaitForSeconds(1f);
        oil--;
        oilSlider.value = oil;
        if (oil <= 0)
            End();
        StartCoroutine(OilDown());
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            animator.SetBool("isLeft", true);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            animator.SetBool("isRight", true);
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            animator.SetBool("isLeft", false);
        if (Input.GetKeyUp(KeyCode.RightArrow))
            animator.SetBool("isRight", false);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector2.up * (Input.GetKey(KeyCode.LeftShift) ? moveSpeed + (moveSpeed / 2) : moveSpeed ) * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(Vector2.down * (Input.GetKey(KeyCode.LeftShift) ? moveSpeed  + (moveSpeed / 2) : moveSpeed ) * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(Vector2.left * (Input.GetKey(KeyCode.LeftShift) ? moveSpeed + (moveSpeed / 2) : moveSpeed ) * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(Vector2.right * (Input.GetKey(KeyCode.LeftShift) ? moveSpeed + (moveSpeed / 2) : moveSpeed) * Time.deltaTime);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -maxXPos, maxXPos), 
            Mathf.Clamp(transform.position.y, -maxYPos, maxYPos));
    }

    private void Shoot()
    {
        shotDelay += Time.deltaTime;
        if(Input.GetKey(KeyCode.Space) && shotDelay > shootDelays)
        {
            playerSource.clip = shootS;
            playerSource.Play();
            Instantiate(bullets[bulletLevel],
            new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
            shotDelay = 0;
        }
    }

    private void Skill()
    {
        if(Input.GetKeyDown(KeyCode.Z) && !skill1IsActive && skill1Count >0)
        {
            StartCoroutine(Skill1());
        }
        else if(Input.GetKeyDown(KeyCode.Z) && skill1Count <= 0)
        {
            StartCoroutine(NoCount());
        }
        else if (Input.GetKeyDown(KeyCode.Z) && skill1IsActive)
        {
            StartCoroutine(WaitCool());
        }

        if (Input.GetKeyDown(KeyCode.X) && !skill2IsActive && skill2Count > 0)
        {
            Instantiate(boomParticle, transform.position, Quaternion.identity);
            StartCoroutine(Skill2());
        }
        else if (Input.GetKeyDown(KeyCode.X) && skill2Count <= 0)
        {
            StartCoroutine(NoCount());
        }
        else if (Input.GetKeyDown(KeyCode.X) && skill2IsActive)
        {
            StartCoroutine(WaitCool());
        }
    }

    private IEnumerator WaitCool()
    {
        infoText.text = "Wait the CoolTime";
        playerSource.clip = uiS;
        playerSource.Play();
        yield return new WaitForSeconds(1.5f);
        infoText.text = " ";
    }
    private IEnumerator NoCount()
    {
        infoText.text = "No Have Count"; 
        playerSource.clip = uiS;
        playerSource.Play();
        yield return new WaitForSeconds(1.5f);
        infoText.text = " ";
    }
    private IEnumerator Skill1()
    {
        healPack.color = new Color(1, 1, 1, 0.5f);
        skill1IsActive = true;
        skill1CoolText.gameObject.SetActive(true);
        skill1Count--;
        HealHp(35);
        skill1CountText.text = $"Count: {skill1Count}";
        for (int i = 0; i < skill1Cool; i++)
        {
            skill1CoolText.text = $"Cool: {skill1Cool - i}";
            yield return new WaitForSeconds(1f);
        }
        healPack.color = new Color(1, 1, 1, 1f);
        skill1IsActive = false;
        skill1CoolText.text = " ";
    }

    private IEnumerator CamAnim()
    {
        for(int i = 0; i < 20; i++)
        {
            mainCam.transform.position = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, -10);
            yield return new WaitForSeconds(0.01f);
        }
        mainCam.transform.position = new Vector3(0,0,-10);
    }

    private IEnumerator Skill2()
    {
        skill2IsActive = true;
        boom.color = new Color(1, 1, 1, 0.5f);
        skill2Count--;
        skill2CountText.text = $"Count: {skill2Count}";
        playerSource.clip = boomS;
        playerSource.Play();

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enem in enemys)
        {
            Destroy(enem);
        }
        for (int i = 0; i < skill2Cool; i++)
        {
            skill2CoolText.text = $"Cool: {skill2Cool - i}";
            yield return new WaitForSeconds(1f);
        }
        boom.color = new Color(1, 1, 1, 1f);
        skill2IsActive = false;
        skill2CoolText.text = " ";
    }

    private void End()
    {
        if (ScoreManager.instance.rankBoard[4].score < GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score)
        {
            ScoreManager.instance.isRanking = true;
            ScoreManager.instance.nowPlayerScore = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().score;
        }
        SceneManager.LoadScene("End");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            switch(collision.gameObject.name)
            {
                case "EnemyBullet":
                    DeleteHp(3);
                    break;
                case "Robot(Clone)":
                    DeleteHp(4);
                    break;
                case "EnemyBulletPlus":
                    DeleteHp(4);
                    break;
                case "UpgradeEnemyBullet":
                    DeleteHp(5);
                    break;
                case "Meteo(Clone)":
                    DeleteHp(5);
                    break;
                case "Can(Clone)":
                    DeleteHp(5);
                    break;
                case "UpgradeCan(Clone)":
                    DeleteHp(7);
                    break;
                case "CameraMania(Clone)":
                    DeleteHp(2);
                    break;
                case "UpgradeCameraMania(Clone)":
                    DeleteHp(3);
                    break;
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            playerSource.clip = itemS;
            playerSource.Play();
            switch (collision.GetComponent<Item>().type)
            {
                case "BulletUpgrade":
                    bulletLevel++;
                    if (bulletLevel > 4)
                        bulletLevel = 4;
                    break;
                case "Shild":
                    StopCoroutine(ShildOn());
                    StartCoroutine(ShildOn());
                    break;
                case "Heal":
                    HealHp(30);
                    hpSlider.value = hp;
                    break;
                case "OilPack":
                    oil = 100;
                    oilSlider.value = oil;
                    break;
                case "ShotSpeed":
                    shootDelays -= 0.05f;
                    if (shootDelays < 0.15f)
                        shootDelays = 0.15f;
                    break;
            }
            Destroy(collision.gameObject);
        }

    }
    IEnumerator ShildOn()
    {
        shild.SetActive(true);
        isGod = true;
        for(int i = 0; i < 5; i++)
        {
            shildText.text = $"Shild: {5 - i}s";
            yield return new WaitForSeconds(1f);
        }
        shildText.text = " ";
        shild.SetActive(false);
        isGod = false;
    }

    //ItemActives

    public void HealHp(int value)
    {
        hp += value;
        playerSource.clip = healS;
        playerSource.Play();
        if (hp > 100)
            hp = 100;
        hpSlider.value = hp;
    }

    public void DeleteHp(int value)
    {
        if (isGod) return;
        StopCoroutine(CamAnim());
        StartCoroutine(CamAnim());
        playerSource.clip = hitS[Random.Range(0, 2)];
        playerSource.Play();
        hp -= value;
        StartCoroutine(God());
        if (hp < 0)
            End();
        hpSlider.value = hp;
    }

    private IEnumerator God()
    {

        isGod = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        isGod = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }
}
