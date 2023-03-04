using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _triple_Shoot_Prefab;
    [SerializeField]
    private GameObject _life_Up_Prefab;
    [SerializeField]
    private GameObject _speed_Prefab;
    [SerializeField]
    private GameObject _shield_Prefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _bonusContainer;

    private bool _isBonus = false;
    private Player _player;
    private bool _stopSpawnint = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            updateBonus();
        }
    }

    void updateBonus()
    {
        int score = _player.getScore();
        if ( score == 3)
        {
            _isBonus = true;
        }   
    }

    public void OnPlayerDeath()
    {
        _stopSpawnint = true;
        Time.timeScale = 0f;
    }

    void activateBonus(GameObject bonus)
    {
        bonus.transform.parent = _bonusContainer.transform;
        _isBonus = false;
        _player.resetScore();
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawnint) {
            Vector3 positionToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 4.0f, 0);
            if (!_isBonus)
            {
                GameObject enemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
                enemy.transform.parent = _enemyContainer.transform;
            } else
            {
                GameObject bonus = null;
                int num = Random.Range(1, 5);
                switch (num)
                {
                    case 1:
                        bonus = Instantiate(_triple_Shoot_Prefab, positionToSpawn, Quaternion.identity);
                        break;
                    case 2:
                        bonus = Instantiate(_speed_Prefab, positionToSpawn, Quaternion.identity);
                        break;
                    case 3:
                        bonus = Instantiate(_shield_Prefab, positionToSpawn, Quaternion.identity);
                        break;
                    default:
                        bonus = Instantiate(_life_Up_Prefab, positionToSpawn, Quaternion.identity);
                        break;
                }
                activateBonus(bonus);

            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
