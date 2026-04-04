using UnityEngine;

public class TempSceneChanger : MonoBehaviour
{
    [SerializeField] string nextScene;

    // Update is called once per frame
    public void ChangeScene()
    {
        GameManager.instance.ChangeSceneTo(nextScene);
    }
}
