using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        float positionY = transform.position.y;
        if (positionY > 8.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
