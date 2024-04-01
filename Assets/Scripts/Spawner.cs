using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab,theMan;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 2f;
    public float HowMuchMade;

    private void Start()
    {
        HowMuchMade = 0;
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
       
        if (HowMuchMade<25)
        {
            GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
            pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
            HowMuchMade += 1;
        }
    }

}
