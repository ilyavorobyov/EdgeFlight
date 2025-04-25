using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Zenject;
using YG;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(YG2.envir.isMobile)
        {
            Debug.Log("da");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
