using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject shot;
    public Transform shotPointTr;
    float speed = 5;
    Vector3 min, max;
    Vector2 colSize;
    Vector2 chrSize;
    void Start()
    {
          
        //ī�޶� ���� �Ʒ� ����
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        //ī�޶� ������ �� ����
        max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        colSize=GetComponent<BoxCollider2D>().size;
        //�ݶ��̴��� ������ /2�� ��
        chrSize = new Vector2(colSize.x/2,colSize.y/2);   
    }


    void Update()
    {
        Move();
        PlayerShot();

    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   //1,-1 ���
        float y = Input.GetAxisRaw("Vertical");     //1,-1 ���

        Vector3 dir = new Vector3(x, y, 0).normalized;
        transform.position += dir * Time.deltaTime * speed;    //����*�ð�*�ӵ�=����

        float newX = transform.position.x;    //������� x��ǥ
        float newY = transform.position.y;  //������� y��ǥ

        //clamp �Լ� ���� �ּ����� �Լ��� ����.
        newX = Mathf.Clamp(newX, min.x + chrSize.x, max.x - chrSize.x);
        newY = Mathf.Clamp(newY, min.y + chrSize.y, max.y - chrSize.y);

        /*if (newX < min.x + chrSize.x)   //������� �߽�x��ǥ�� ī�޶��� ���� �Ʒ� ���� + �ݶ��̴��� �ʺ�/2 �� ������ ���� ��
        {
            newX=min.x + chrSize.x; //������� x��ǥ�� ī�޶� ���� �Ʒ��� �ּڰ��� ����.
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

        /*if (newY + chrSize.y < min.y) ��Ŵ� ȭ�� �Ʒ��� ������ �� ������ Ƣ����� �ڵ�
        {
           newY = max.y;
        }
        */
        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    public float shotMax = 0.2f;
    public float shotDelay = 0;
    void PlayerShot()
    {
        shotDelay += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            if(shotDelay > shotMax)
            {
                //������� ��ġ �������� ��
                Vector3 vec = new Vector3(transform.position.x + 1.12f,
                    transform.position.y - 0.17f, transform.position.z);

                //��ü �����ϴ� �Լ�,3��°�Ŵ� ��ü ȸ�� ���
                Instantiate(shot, vec, Quaternion.identity);
                shotDelay = 0;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            CoinScript coinScript=collision.gameObject.GetComponent<CoinScript>();
            GameManager.instance.coin += coinScript.coinSize;
            print("Coin"+GameManager.instance.coin);
            Destroy(collision.gameObject);
        }
    }
}