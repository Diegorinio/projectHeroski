using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _colliders = colliders;
    }
    public void resetColliders()
    {
        _colliders.Clear();
    }
    public void moveToRandomDirecion()
    {
        resetColliders();
       
        gameObject.transform.position = _colliders[(Random.Range(1, _colliders.Count-1))].transform.position;
    }
}
