using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject heartPrefab;

    public Transform heartsParent;

    public int maxHearts = 20;

    private List<Image> heartImages = new List<Image>();

    void Start()
    {
        CreateHearts();
    }

    void Update()
    {
        int currentHealth = Player.GetCurrentHealth();  
        for (int i = 0; i < maxHearts; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true;
            }
            else
            {
                heartImages[i].enabled = false;
            }
        }
    }

    void CreateHearts()
    {
        float heartWidth = heartPrefab.GetComponent<RectTransform>().rect.width;
        float totalWidth = maxHearts * heartWidth;

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            RectTransform heartTransform = heart.GetComponent<RectTransform>();

            float xPos = -totalWidth + i * heartWidth;
            heartTransform.anchoredPosition = new Vector2(xPos - 100, 230);

            heartImages.Add(heart.GetComponent<Image>());
        }
    }
}
