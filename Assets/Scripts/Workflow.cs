using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workflow : MonoBehaviour
{
    [Header("Drag your exp_objects GameObject here")]
    [SerializeField] private Transform expObjectsParent;

    // Keep track of the currently displayed piece:
    private Transform currentPiece;
    private Vector3 currentOriginalPos;

    // Hold a reference so we can cancel if the user mashes N:
    private Coroutine showCoroutine;

    private void Update()
    {
        // Press N to show a new piece after 3 seconds:
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (expObjectsParent == null)
            {
                Debug.LogWarning("expObjectsParent is not assigned!");
                return;
            }

            // If a coroutine is already waiting, cancel it:
            if (showCoroutine != null)
                StopCoroutine(showCoroutine);

            showCoroutine = StartCoroutine(ShowNewPieceAfterDelay(3f));
        }
    }

    private IEnumerator ShowNewPieceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 1) Restore the previous piece, if any
        if (currentPiece != null)
        {
            currentPiece.position = currentOriginalPos;
            Debug.Log($"Restored {currentPiece.name} to {currentOriginalPos}");
        }

        // 2) Pick a new random child
        List<Transform> children = new List<Transform>();
        foreach (Transform child in expObjectsParent)
            children.Add(child);

        if (children.Count == 0)
        {
            Debug.LogWarning("No child objects under " + expObjectsParent.name);
            yield break;
        }

        int idx = Random.Range(0, children.Count);
        currentPiece = children[idx];
        currentOriginalPos = currentPiece.position;

        // 3) Move it up
        currentPiece.position = new Vector3(0f, 10f, 0f);
        Debug.Log($"Moved {currentPiece.name} to (0,10,0)");

        // Clear coroutine reference since it’s done:
        showCoroutine = null;
    }
}




