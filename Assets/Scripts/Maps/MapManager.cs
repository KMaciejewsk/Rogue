using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 10f;

    private bool canSpwan = true;

    public static MapManager instance;

    [SerializeField] private int width = 80, height = 45;

    [SerializeField] private TileBase floorTile, wallTile;

    [SerializeField] private Tilemap floorMap, obstacleMap;

    public Tilemap FloorMap { get => floorMap; }

    public Tilemap ObstacleMap { get => obstacleMap; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Vector3Int centerTile = new Vector3Int(width / 2, height / 2, 0);

        CreateWallsAtEdges();

        Instantiate(Resources.Load<GameObject>("Amogus"), new Vector3(40 + 0.5f, 25 + 0.5f, 0), Quaternion.identity).name = "Amogus";
        Instantiate(Resources.Load<GameObject>("Gun"), new Vector3(40 + 0.5f, 25 + 0.5f, 0), Quaternion.identity).name = "Gun";
    }

        void Update()
    {
        if (canSpwan)
        {
            SpawnEnemies();

            StartCoroutine(ReloadCoroutine());
        }
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    private void CreateWallsAtEdges()
    {
        for (int x = 0; x <= width; x++)
        {
            Vector3Int topWallPosition = new Vector3Int(x, height, 0);
            Vector3Int bottomWallPosition = new Vector3Int(x, 0, 0);
            obstacleMap.SetTile(topWallPosition, wallTile);
            obstacleMap.SetTile(bottomWallPosition, wallTile);
        }

        for (int y = 0; y <= height; y++)
        {
            Vector3Int leftWallPosition = new Vector3Int(0, y, 0);
            Vector3Int rightWallPosition = new Vector3Int(width, y, 0);
            obstacleMap.SetTile(leftWallPosition, wallTile);
            obstacleMap.SetTile(rightWallPosition, wallTile);
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject EnemyGameObject = Instantiate(Resources.Load<GameObject>("Skull"), new Vector3(Random.Range(1, width-1), Random.Range(1, height-1), 0), Quaternion.identity);
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        canSpwan = false;

        yield return new WaitForSeconds(spawnInterval);

        canSpwan = true;
    }
}
