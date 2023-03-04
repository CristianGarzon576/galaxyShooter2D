using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private GameObject _tripleShoot;
    private float _nextFire = 0.0f;
    [SerializeField]
    private float _lives = 3f;
    [SerializeField]
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShot = false;
    [SerializeField]
    private bool _isShield = false;

    private int _score = 0;

    private bool _needFireRate = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -4.2f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_needFireRate && Time.time > _nextFire)
            {
                shoot();
            } else if (!_needFireRate)
            {
                shoot();
            }
        }
    }

    void shoot()
    {
        _nextFire = Time.time + _fireRate;
        float positionX = transform.position.x;
        float positionY = transform.position.y;
        if (_isTripleShot)
        {
            GameObject bullet = Instantiate(_tripleShoot, transform.position + new Vector3(-0.9f, 0, 0), Quaternion.identity);
        }
        else {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity);
        }
    }

    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float positionX = transform.position.x;
        float positionY = transform.position.y;
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if (positionX > 8.5f)
        {
            transform.position = new Vector3(8.5f, positionY, 0);
        }
        else if (positionX < -8.5f)
        {
            transform.position = new Vector3(-8.5f, positionY, 0);
        }
        else if (positionY > 4.0f)
        {
            transform.position = new Vector3(positionX, 4.0f, 0);
        }
        else if (positionY < -4.2f)
        {
            transform.position = new Vector3(positionX, -4.2f, 0);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
    }

    public void IncreseScore()
    {
        _score ++;
    }

    public int getScore()
    {
        return _score;
    }

    public void resetScore()
    {
        _score = 0;
    }

    public void gainLife()
    {
        _lives += 1f;
    }

    public void activeTripleShoot()
    {
        _isTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void activeShiled()
    {
        _isShield = true;
        StartCoroutine(ShieldPowerDownRoutine());
    }

    public void activeSpeed()
    {
        _speed = 15f;
        _needFireRate = false;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void Damage(float damage)
    {
        if (!_isShield)
        {
            _lives -= damage;
            if (_lives <= 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _speed = 5f;
        _needFireRate = true;
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isShield = false;
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        _isTripleShot = false;
    }
}
