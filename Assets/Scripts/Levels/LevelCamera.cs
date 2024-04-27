using System.Collections;
using UnityEngine;

public class LevelCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;

    void Start()
    {
        if (cam != null)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, transform.position.y, -10f);
        }
    }
}
