using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuy : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
        _animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Disappear()
    {
        _animator.SetBool("isDone", true);
    }
    public void Reappear()
    {
        
        _animator.SetBool("isDone", false);
    }
}
