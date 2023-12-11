using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class enemyAI : MonoBehaviour
{
    [SerializeField]
    private List<Collider2D> _colliders = new List<Collider2D>();
    void Start()
    {
        //gameObject.transform.position = _colliders[(Random.Range(0, _colliders.Count))].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position = _colliders[(Random.Range(0, _colliders.Count))].transform.position;
    }
    public void changeColliders(List<Collider2D> colliders)
    {
        //colliders = colliders.GroupBy(c => c.name).Select(d => d.First()).ToList();
        resetColliders();
        _colliders = colliders.GroupBy(c => c.name).Select(d => d.First()).ToList();
    }
    public void resetColliders()
    {
        _colliders.Clear();
    }
    public void moveToRandomDirecion()
    {
        int id = Random.Range(0, _colliders.Count - 1);
        Debug.Log($"moves list size {_colliders.Count} wybor id: {id}");
        gameObject.transform.position = _colliders[(Random.Range(0, _colliders.Count - 1))].transform.position;
        gameObject.GetComponent<characterController>().disableClickable();
    }
}
