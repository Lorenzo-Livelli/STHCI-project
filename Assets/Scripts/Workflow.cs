using System.Collections;
using UnityEngine;

public class Workflow : MonoBehaviour
{
    private GameObject[] objectPrefabs;
    private Vector3 spawnPosition = new Vector3(1, 8, 0);
    private bool isGenerating = false;

    void Awake()
    {
        // Load all prefabs in Assets/Resources/Object_prefabs
        objectPrefabs = Resources.LoadAll<GameObject>("Object_prefabs");

        if (objectPrefabs == null || objectPrefabs.Length == 0)
            Debug.LogError("No prefabs found in Resources/Object_prefabs!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isGenerating)
        {
            isGenerating = true;
            StartCoroutine(GeneratePrefabs());
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGenerating = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator GeneratePrefabs()
    {
        while (isGenerating)
        {
            if (objectPrefabs.Length == 0)
            {
                Debug.LogWarning("objectPrefabs is empty—stopping coroutine.");
                yield break;
            }

            int randomIndex = Random.Range(0, objectPrefabs.Length);
            Instantiate(objectPrefabs[randomIndex], spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(3f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawnPosition, 0.5f);
    }
}
