using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    //
    public GameObject message;
    public GameObject maribMan;
    public GameObject maribManUpHand;

    //متغير من خلاله نعرف هل الرسالة تم استلامها لكي يتم عرض كتابة الفوز
    public bool hasPassedMan = false;
    private void Awake()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {

        if (FindObjectOfType<GameManager>().score < 25)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                direction = Vector3.up * strength;
            }

            // Apply gravity and update the position
            direction.y += gravity * Time.deltaTime;
            transform.position += direction * Time.deltaTime;

            // Tilt the bird based on the direction
            Vector3 rotation = transform.eulerAngles;
            rotation.z = direction.y * tilt;
            transform.eulerAngles = rotation;

        }
        else
        {
            transform.Rotate(0, 0, 0);
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;

            //تمكنه من التقدم الى الامام x في حال تجاوز العدد المطلوب من الاعمدة يتم اعطاء الطائر قيمة معينة على المحور 
            transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
            
            if (maribMan.transform.position.x - 4f < message.transform.position.x)// في هذا الشرط نقارن موقع الطائر مع موقع الرجل  على المحور  العمودي اذا اقترب نقوم بتنفيذ اوامر القاء الرسالة 
            {                                                                     // تم تنقيص موقع محور الرجل العمودي بمقدار 4 وذلك لكي يتم تنفيذ القاء الرسالة قبل وصول الهدهد فوق الرجل يمكن زيادة العدد او تنقيصه لتغير المكان المناسب للهدهد لكي يلقي الرسالة
                
                //في البداية تكون الرسالة ابن للهدهد او تابع لهذا قمنا هنا بجعل الرسالة بدون اب لكي لا تتاثر في الحركة
                message.transform.SetParent(null);
                //هنا تم اخفاء صورة الرجل العادي
                maribMan.SetActive(false);
                //هنا اضهرنا صورة الرجل الذي يرفع يديه لاستلام الرسالة
                maribManUpHand.SetActive(true);
                //تنفيذ دالة القاء الرسالة 
                StartCoroutine(MoveMessage());//IEnumerator  وذلك لان دالة القاء الرسالة من نوع   StratCoroutine تم استخدام   

            }




        }




    }

    //  
    private IEnumerator MoveMessage()
    {
        //تحديد الموقع الابتدائي للرسالة 
        Vector3 startPosition = message.transform.position;
        //تحديد الموقع المراد ارسال الرسالة اليه وهو عند يدي الرجل
        Vector3 targetPosition = new Vector3(6.36000013f, -2.86999989f, 0);

        //متغير لحفظ الوقت المنقضي لتمرير الرسالة 
        float elapsedTime = 0f;
        //هنا قمت بقياس المسافة بين الموقعين ثم قمت بالقسمة على السرعة لحساب المدة
        float duration = Vector3.Distance(startPosition, targetPosition) / 5f;

        // نتحقق اذا كان الوقت المنقضي اقل من المدة المطلوبة لوصول الرسالة الى الهدف نقوم بتنفيذ القاء 
        while(elapsedTime < duration)
        {
            //في كل مرة يتم زيادة المتغير بوقت حقيقي
            elapsedTime += Time.deltaTime;
          
            //clamp01  هنا هذا المتغير راح يخزن قيمة تتراوح قيمتها بين 1 و 0 او اي عدد نوع فلووت بين 0 و 1 وهذا لانه استخدمنا دالة 
            
            float t = Mathf.Clamp01(elapsedTime / duration);//راح نوضح فائدة هذا المتغير في الاسفل

            //Vector3.Lerp  هنا لنقل الرسالة بشكل خطي من موقع الى اخر يتم استخدام    
            //ذلك لنقل الرسالة من الهدهد الى الرجل 
            // تقبل ثلاثة متغيرات  الاول نقطة البداية و الثاني نقطة النهاية و الثالث هو رقم نوع فلووت تتراوح قيمته بين 1 و 0
            //اذا كانت قيمة المتغير 0 اي ان الرسالة في موقع البداية و اذا كان 1 اي ان الرسالة وصلت لموقع النهاية 
            // لانه يحسب القسمة بين المدة والوقت المنقضي وعندما يتساوان راح يكون ناتج القسمة 1 اي ان الرسالة وصلت للرجل t  لهذا السبب عملنا المتغير  
            message.transform.position = Vector3.Lerp(startPosition,targetPosition, t);
            
            //null  لا نحتاج لتأخير تنفيذ الدالة لهذا قمنا بوضع   
            yield return null;
        }
        //بعد وصول الرسالة ليدي الرجل يتم نقوم هنا بجعل كائن الرسالة ابن لكائن الرجل
        message.transform.SetParent(maribManUpHand.transform);

        hasPassedMan = true;


    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
            GameManager.Instance.IncreaseScore();
        }
    }

}
