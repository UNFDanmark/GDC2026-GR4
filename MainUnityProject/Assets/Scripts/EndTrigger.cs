using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SceneTransition.instance.unloadScene("Credits");
    }
}
