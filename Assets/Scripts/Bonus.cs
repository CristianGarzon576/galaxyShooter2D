using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

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
        if (positionY < -4.2f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
    }

    void activeTripleShotBonus(Player player, Collider2D bullet)
    {
        player.activeTripleShoot();
        player.resetScore();
        Destroy(this.gameObject);
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    void activeShieldBonus(Player player, Collider2D bullet)
    {
        player.activeShiled();
        player.resetScore();
        Destroy(this.gameObject);
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    void activeExtraSpeedBonus(Player player, Collider2D bullet)
    {
        player.activeSpeed();
        player.resetScore();
        Destroy(this.gameObject);
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    void addExtraLifeBonus(Player player, Collider2D bullet)
    {
        player.gainLife();
        player.resetScore();
        Destroy(this.gameObject);
        if (bullet != null)
        {
            Destroy(bullet.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string objectName = other.tag;
        Collider2D objectToDestroy = objectName == "Bullet" ? other : null;
        {
            switch (transform.tag)
            {
                case "Triple_Shoot_Bonus":
                    activeTripleShotBonus(_player, objectToDestroy);
                    break;
                case "Shield_Bonus":
                    activeShieldBonus(_player, objectToDestroy);
                    break;
                case "Speed_Bonus":
                    activeExtraSpeedBonus(_player, objectToDestroy);
                    break;
                default:
                    addExtraLifeBonus(_player, objectToDestroy);
                    break;
            }
        }
    }
}
