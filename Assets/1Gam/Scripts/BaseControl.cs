using UnityEngine;
using System.Collections;

public abstract class BaseControl : MonoBehaviour {

    [HideInInspector]
    public GameManager gameManager;

    public abstract bool shootRequested();
}
