using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMark : MonoSingleton<MovementMark>
{
    [SerializeField] private float time = 1.6f; 

    public void ActiveIcon(Vector3 _pos)
    {
        transform.position = _pos;
        ActiveIcon(true); 
    }
    private void ActiveIcon(bool _isActive)
    {
        gameObject.SetActive(_isActive);
    }
    private IEnumerator ActiveIcon()
    {
        yield return new WaitForSeconds(time);
        ActiveIcon(true);
    }
}
