using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountController : MonoBehaviour {

    List<AccountData> accounts = new List<AccountData>();

	// Use this for initialization
	void Start () {


        // Find all txt files
        string[] file_list = System.IO.Directory.GetFiles(Application.dataPath + "/Data/Account", "*.txt");
        int address_length = Application.dataPath.Length + 14;

        int account_id = 0;

        // Get all accounts
        foreach (string name in file_list)
        {
            AccountData candidate = new AccountData();

            // Name
            string cleaned_name = name.Substring(address_length, name.Length - address_length - 4);
            candidate.username = cleaned_name;

            accounts.Add(candidate);
            Debug.Log(account_id + ":" + candidate.username );
            account_id++;
        }

        Debug.Log(accounts.Count);

    }

    public AccountData AccountDetails(int player_number)
    {
        return accounts[player_number];
    }

    public void SaveAccount(int player_number)
    {
        System.IO.StreamWriter writer = new System.IO.StreamWriter(Application.dataPath + "/Data/Account/" + accounts[player_number].username + ".txt", false);

        // Used for each type (defined within 'AccountData')
        writer.WriteLine("Goal = " +  accounts[player_number].score + "\n");

        writer.Close();
    }

    public void LoadAccountInfo(int player_number)
    {
        Debug.Log(Application.dataPath + "/Data/Account/" + accounts[player_number].username + ".txt");

        // Open file of profile
        System.IO.StreamReader reader = new System.IO.StreamReader(Application.dataPath + "/Data/Account/" + accounts[player_number].username + ".txt");

        // Load file contents
        List<string> text_from_file = new List<string>();
        string whole_text_as_string = reader.ReadToEnd();
        text_from_file.AddRange(whole_text_as_string.Split("\n"[0]));
        

        // Used for each type (defined within 'AccountData')
        for(int i = 0; i < text_from_file.Count; i++)
        {
            string candidate = text_from_file[i];

            if (candidate.Contains("Goal"))
            {
                accounts[player_number].score = int.Parse(candidate.Substring(candidate.IndexOf('=') + 1));
            }
        }

        // Clean up file
        reader.Close();
    }

    // Update all data
    public void UpdateAccountInfo(int player_id, AccountData data)
    {
        accounts[player_id] = data;
    }

    // Update specfic data
    public void UpdateAccountInfo(int player_id, int score)
    {
        accounts[player_id].score = score;
    }
}
