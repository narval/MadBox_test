using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuncherController : MonoBehaviour
{    
    [SerializeField] Animator animator;
    [SerializeField] float delay; // delay between animations

    private float timer = 2;

    void Start()
    {
        timer = delay;
    }

    void Update()
    {
        if (timer <= 0) {
            animator.SetTrigger("Hit");
            timer = delay;
        }
        timer -= Time.deltaTime;
    }
}
