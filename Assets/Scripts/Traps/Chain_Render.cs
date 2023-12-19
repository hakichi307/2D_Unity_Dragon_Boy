using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    LineRenderer line;
    public Transform entrypoint;
    public Transform exitpoint;
    [Header("SFX")]
    [SerializeField] private AudioClip airwooshtrapSound;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, entrypoint.position);
        line.SetPosition(1, exitpoint.position);
        SoundManager.instance.PlaySound(airwooshtrapSound);
    }
}
