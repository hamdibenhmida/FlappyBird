using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver,win;

    public int score;
    public int Score => score;

    private void Awake()
    {
        Time.timeScale = 1f;

        win.SetActive(false);
        gameOver.SetActive(false);
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);
            Pause();
        }
    }
    private void Update()
    {
        if (FindObjectOfType<Player>().hasPassedMan && score >= 2)
        {
            win.SetActive(true);
           
        }
    }
    public void Play()
    {
        //⁄‰„« Ì»œ« «·‘Œ’ «··⁄»… ﬁ„  ›Ì Â–Â «·«”ÿ— »«—Ã«⁄ ﬂ· «·ﬁÌ„ «·«› —«÷ÌÂ ··„ €Ì—«  «· «·Ì… 
        FindObjectOfType<Player>().hasPassedMan = false;
        FindObjectOfType<Player>().maribMan.SetActive(false);
        FindObjectOfType<Player>().maribManUpHand.SetActive(false);
        FindObjectOfType<Player>().message.transform.SetParent(player.transform);
        FindObjectOfType<Player>().message.transform.localPosition = new Vector3(-0.123999998f, -0.314999998f, 2);
        //ﬁ„   »«—Ã«⁄ «·ÂœÂœ ·„ﬂ«‰Â «·«› —«÷Ì ⁄‰œ »œ¡ «··⁄»… 
        player.transform.position = new Vector3(0, 0, -2);

        score = 0;
        scoreText.text = score.ToString()+"/25";

        playButton.SetActive(false);
        gameOver.SetActive(false);
        win.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        FindObjectOfType<Spawner>().HowMuchMade = 0;
        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        
        gameOver.SetActive(true);

        Pause();
    }

   

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString()+"/25";
    }
  
}
