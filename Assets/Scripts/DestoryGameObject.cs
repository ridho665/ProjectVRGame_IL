using UnityEngine;

public class DestoryGameObject : MonoBehaviour
{

    public void OnDestroy() 
    {
        Destroy(gameObject);
    }

}
