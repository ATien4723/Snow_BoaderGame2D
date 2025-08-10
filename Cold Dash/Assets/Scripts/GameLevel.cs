using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameLevel : MonoBehaviour
{
    public void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex; 

        if ( currentSceneIndex == 5 ) {
            GameSession.Instance.SetCurrentLevel (2); // Thiết lập cấp độ 2 cho scene 5
        } else if ( currentSceneIndex == 6 ) {
            GameSession.Instance.SetCurrentLevel (3); // Thiết lập cấp độ 3 cho scene 6
        } else {
            GameSession.Instance.SetCurrentLevel (1); // Mặc định là cấp độ 1 cho các scene khác
        }

        GameSession.Instance.UpdateLevelText (); // Cập nhật hiển thị cấp độ ngay sau khi thiết lập
    }
}
