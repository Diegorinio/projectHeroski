using UnityEngine;

public class testTileSpawner : MonoBehaviour
{
    public GameObject elementPrefab;
    public RectTransform spawnArea;

    private void Start()
    {
        SpawnElementsInArea(10);
    }

    private void SpawnElementsInArea(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Losowe współrzędne punktu w obszarze RectTransform
            float randomX = Random.Range(spawnArea.rect.xMin, spawnArea.rect.xMax);
            float randomY = Random.Range(spawnArea.rect.yMin, spawnArea.rect.yMax);

            // Przelicz współrzędne ekranowe na lokalne
            Vector2 spawnPoint = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(spawnArea, new Vector2(randomX, randomY), null, out spawnPoint);

            // Zespawnuj element w obszarze
            GameObject spawnedElement = Instantiate(elementPrefab);
            spawnedElement.transform.SetParent(spawnArea.transform);

            // Ustaw lokalne współrzędne elementu w obrębie RectTransform obszaru
            RectTransform spawnedRectTransform = spawnedElement.GetComponent<RectTransform>();
            spawnedRectTransform.anchoredPosition = spawnPoint;

            // Opcjonalnie: Skalowanie elementu, aby dopasować do obszaru
            float randomScale = Random.Range(0.5f, 1.5f);
            spawnedRectTransform.localScale = new Vector3(randomScale, randomScale, 1f);
        }
    }
}