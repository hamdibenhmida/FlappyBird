using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxOfGround : MonoBehaviour
{
    public float StartPos, EndPOs,speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //åäÇ ŞãÊ ÈæÖÚ ÔÑØ áãÚÑİÉ Çä ÇááÇÚÈ ÊÌÇæÒ ÇáÚÏÏ ÇáãØáæÈ Çæ áÇ áßí  íÊã ÇíŞÇİ ÍÑßÉ ÇáÛíæã æÇáÇÑÖ
        if (FindObjectOfType<GameManager>().score < 25)
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
        else
            transform.Translate(Vector2.zero);
        


        if (transform.position.x <= EndPOs)
        {
            transform.position = new Vector2(StartPos, transform.position.y);
        }
    }
}
