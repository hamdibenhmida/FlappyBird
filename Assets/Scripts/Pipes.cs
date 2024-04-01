using UnityEngine;
using System.Collections;

public class Pipes : MonoBehaviour
{

    public Transform top;
    public Transform bottom;
    public float speed = 5f;

    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * Vector3.left;

        //فقط قمت بزيادة شرط للتاكد من ان اللاعب تجاوز العدد المطلوب من الاعمدة او لا
        if(FindObjectOfType<GameManager>().score >= 25)
        {
            StartCoroutine(destroyPipes());
        }

        if (transform.position.x < leftEdge) {

            Destroy(gameObject);
        }
    }
    IEnumerator destroyPipes()
    {
        
        yield return new WaitForSeconds(1);//قمت بوضع مدة تأخير تنفيذ الاوامر التي تلي هذا السطر مدة ثانية واحدة فقط

        Destroy(gameObject);

        FindObjectOfType<Player>().maribMan.SetActive(true);//اضهار صورة الرجل العادي
    }
}
