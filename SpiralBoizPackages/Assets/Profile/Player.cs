using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public int player_id;
    [SerializeField] bool testing = false;
    [SerializeField]AccountController account_manager;
    AccountData player_info;


    private void Update()
    {
        if(testing == true)
        {
            updateAccount();

            testing = false;
        }
    }

    void updateAccount()
    {
        account_manager.LoadAccountInfo(player_id);
        player_info = account_manager.AccountDetails(player_id);

        Debug.Log("Score = " + player_info.score);
    }
}
