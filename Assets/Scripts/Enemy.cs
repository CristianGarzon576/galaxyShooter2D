using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float positionY = transform.position.y;
        if(positionY < -4.2f)
        {
            if(_player != null)
            {
                _player.Damage(1);
                _player.resetScore();
            }
            transform.position = new Vector3((Random.Range(-8.0f, 8.0f)), 4.0f, 0);
        } else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string objectName = other.tag;
        if (objectName == "Player")
        {
            Player player = other.transform.GetComponent("Player") as Player;
            if( player != null)
            {
                player.Damage(1);
                player.resetScore();
            }
            Destroy(this.gameObject);
        }

        if(objectName == "Bullet")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.IncreseScore();
            }
        }
    }
}
