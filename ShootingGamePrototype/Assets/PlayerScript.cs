using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject shot;
    public GameObject explosion;
    public Transform shotPointTr;
    float speed = 5;
    Vector3 min, max;
    Vector2 colSize;
    Vector2 chrSize;
    void Start()
    {
          
        //카메라 왼쪽 아래 구석
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //카메라 오른쪽 위 구석
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        colSize=GetComponent<BoxCollider2D>().size;
        //콜라이더의 사이즈 /2의 값
        chrSize = new Vector2(colSize.x/2,colSize.y/2);   
    }


    void Update()
    {
        Move();
        PlayerShot();

    }
    //플레이어 이동 함수
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   //1,-1 출력
        float y = Input.GetAxisRaw("Vertical");     //1,-1 출력

        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position += dir * Time.deltaTime * speed;    //방향*시간*속도=벡터

        float newX = transform.position.x;    //비행기의 x좌표
        float newY = transform.position.y;    //비행기의 y좌표

        //clamp 함수 밑의 주석문을 함수로 구현.
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);

        /*if (newX < min.x + chrSize.x)   //비행기의 중심x좌표가 카메라의 왼쪽 아래 구석 + 콜라이더의 너비/2 의 값보다 작을 때
        {
            newX=min.x + chrSize.x; //비행기의 x좌표를 카메라 왼쪽 아래의 최솟값에 고정.
        }
        if (newX > max.x - chrSize.x)
        {
            newX = max.x - chrSize.x;
        }
        if (newY > max.y - chrSize.y)
        {
            newY = max.y - chrSize.y;
        }
        if (newY < min.y + chrSize.y)
        {
            newY = min.y + chrSize.y;
        }*/

        /*if (newY + chrSize.y < min.y) 요거는 화면 아래로 나갔을 때 위에서 튀어나오는 코드
        {
           newY = max.y;
        }
        */
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public float shotMax = 0.2f;
    public float shotDelay = 0;
    //발사체 생성 함수
    void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(shotDelay > shotMax)
            {
                //비행기의 위치 가져오는 중
                //발사체의 위치 지정.
                Vector3 vec = new Vector3(transform.position.x + 1.12f,
                    transform.position.y - 0.17f, transform.position.z);

                //발사체 생성,3번째거는 물체 회전 담당
                Instantiate(shot, vec, Quaternion.identity);
                shotDelay = 0;
            }
        }        
    }

    //충돌 검사 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") //부딪힌 물체가 "item"인 경우
        {
            // GetComponent<CoinScript>()는 충돌한 게임 객체에서 CoinScript 컴포넌트를 가져옵니다.
            CoinScript coinScript=collision.gameObject.GetComponent<CoinScript>();
            GameManager.instance.coin += coinScript.coinSize;
            print("Coin"+GameManager.instance.coin);

            GameManager.instance.coinText.text=GameManager.instance.coin.ToString();        //얻은 코인 텍스트로 
            Destroy(collision.gameObject);
        }else if (collision.gameObject.tag =="Asteroid"||
            collision.gameObject.tag =="Enemy"||
            collision.gameObject.tag == "EnemyShot")        //부딪힌 물체의 tag가 ~일 경우 두 물체 다 파괴. 
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(explosion,transform.position,Quaternion.identity);
        }
    }

    
}
