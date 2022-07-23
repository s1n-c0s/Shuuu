using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCtrl : MonoBehaviour
{
    bool turnLeft;
    bool isGround;
    int ts;
    public GameObject pause;
    public GameObject bulletPrefabs; // ?????? ???????????? Prefabs ????????? Folder Prefabs
    public GameObject winpanel;
    public GameObject losspanel;
    public Transform bulletOut; // ?????? ???????????? BulletOut ?? Scene
    GameObject cloneBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GenerateBullet() //?????????????????????????
    {
        cloneBullet = Instantiate(bulletPrefabs); // ??? Copy BulletPrefabs ?????? Clone
        cloneBullet.transform.position = bulletOut.position; // ????? Clone ???????????????????????
        if (turnLeft)
        {
            /*cloneBullet.transform.localScale = new Vector2(cloneBullet.transform.localScale.x, 
                                                            cloneBullet.transform.localScale.y);*/
            cloneBullet.transform.rotation *= Quaternion.Euler(0, 0, 180);// ???????????????????????????????
            cloneBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 1000);// ?????????????????? Clone
        }
        else
        {
            cloneBullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1000);// ?????????????????? Clone
        }
        Destroy(cloneBullet, 0.5f); //?????????????????????? ??????????????? 1 ??????
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pause.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10);
        }

        float LR, SpeedX = 6.0f;
        LR = Input.GetAxis("Horizontal"); // ???????? ?????? ??? x ????????? -1,0,1
        transform.Translate(LR * Time.deltaTime * SpeedX, 0, 0); //????????????? ??? Player ??????????????????? ???????? X

        if (LR < 0 && !turnLeft) // ????????????????? ???????? 0 ??????? 
        {
            turnLeft = true;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (LR > 0 && turnLeft) // ????????????????? ??????? 0 ??????
        {
            turnLeft = false;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GenerateBullet();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGround = true;
        }
        if (collision.gameObject.tag == "End")
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            winpanel.SetActive(true);
            
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            Time.timeScale = 0;
            losspanel.SetActive(true);
            
            
        }
    }
}
