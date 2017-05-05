using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDataAccess
/// </summary>
public class UserDataAccess
{
    #region --- Fields ---
    //string connectionString = "Server=BILG-251-KURSU\\SQLEXPRESS;Database=CSE5001DB;uid=sa;pwd=4444;";
    //string connectionString = "Server=.;Database=CSE5001DB;Trusted_Connection=True";
    string connectionString = System.Configuration.ConfigurationManager.AppSettings["connString"];
    SqlConnection connection;
    #endregion

    #region --- Constructor ---
    public UserDataAccess()
    {
        OpenConnection(connectionString);
    }
    #endregion

    #region --- Private Methods ---
    private bool OpenConnection(string pConnString)
    {
        connection = new SqlConnection(pConnString);
        connection.Open();
        if (connection.State == ConnectionState.Open)
            return true;
        else return false;
    }
    private void CloseConnection()
    {
        if (connection.State != ConnectionState.Closed)
            connection.Close();
    }

    #endregion

    #region --- Public Methods ---

    
    public DataTable GetSymptoms(string partID)
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);

        DataTable dtUser = new DataTable();


        SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM diagnose.symptomes WHERE bodyPartID = "+ partID, connection);
        adapter.Fill(dtUser);

        CloseConnection();

        return dtUser;


    }

    public DataTable Getdisease(string query)
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);

        DataTable dtUser = new DataTable();
        List<string> newQuery = new List<string>();

        string sorgu = "select dd.diseaseName from [diagnose].[disease] as dd inner join [diagnose].[diseasesymptomes] as dds on dd.[diseaseID]=dds.[diseaseID] inner join [diagnose].[symptomes] as ds on dds.[symptomID]= ds.[symptomID] where ds.[symptomName]=";
    
        int start = 0;
        for (int i = 0; i<24; i++)
        {

            string toFind1 = "-";
           

            int end = query.IndexOf(toFind1, start+1);
            newQuery.Add(query.Substring(start, end - start));
            start = end;
            sorgu+= "'" + newQuery[i] + "'";
            if (end == query.Length-1)
                break;
            sorgu += " or [symptomName]=";
        }

        SqlDataAdapter adapter = new SqlDataAdapter(sorgu, connection);
        adapter.Fill(dtUser);

        CloseConnection();

        return dtUser;


    }

    public DataTable GetUsers()
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);

        DataTable dtUser = new DataTable();

        //SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblUser", connection);
        SqlDataAdapter adapter = new SqlDataAdapter("SELECT u.*, ut.UserTypeName, ut.TypeImage FROM tblUser u INNER JOIN tblUserType ut ON u.UserTypeID=ut.UserTypeID", connection);
        adapter.Fill(dtUser);

        CloseConnection();
        return dtUser;
    }

    public SqlDataReader GetUserByID(int pUserID)
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);

        DataTable dtUser = new DataTable();

        SqlCommand command = new SqlCommand("SELECT * FROM tblUser WHERE UserID=user_id", connection);
        command.Parameters.AddWithValue("@user_id", pUserID);

        return command.ExecuteReader();
    }

    /// <summary>
    /// Used for Insert/Update/Delete etc. operations
    /// Usage Example:  
    ///      UserDataAccess uDataAccess =new UserDataAccess();
    ///      uDataAccess.Execute("DELETE FROM tblUser WHERE UserID=5");
    ///   or   
    ///      uDataAccess.Execute("INSERT INTO tblUser(FirstName) VALUES("fatma")");
    /// </summary>
    public void Execute(string query)
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);
        SqlCommand comm = new SqlCommand(query, connection);
        comm.ExecuteNonQuery();

        CloseConnection();
    }

    /// <summary>
    /// Dynamic SQL statement:
    /// Dynamic Query creates an SQL query with the user input all together
    /// An application vulnerable to SQL injection attack
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pSurname"></param>
    /// <returns></returns>
    public int InsertUser(string pName, string pSurname)
    {
        if (connection.State != ConnectionState.Open)
            OpenConnection(connectionString);

        SqlCommand command = new SqlCommand("INSERT INTO tblUser(FirstName,LastName,UserTypeID) VALUES('" + pName + "','" + pSurname + "',2)", connection);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// Parameterized SQL statement:
    /// Working with parameters helps better performance, high efficiency and prevent SQL Injection attacks
    /// It makes your application much more secure.
    /// </summary>
    /// <param name="pName"></param>
    /// <param name="pSurname"></param>
    /// <returns></returns>
    public int InsertUserByParam(string pName, string pSurname)
    {
        try
        {
            if (connection.State != ConnectionState.Open)
                OpenConnection(connectionString);

            SqlCommand command = new SqlCommand("INSERT INTO tblUser(FirstName,LastName,UserTypeID) VALUES(@first_name,@last_name,@user_type)", connection);
            command.Parameters.AddWithValue("@first_name", pName);
            command.Parameters.AddWithValue("@last_name", pSurname);
            command.Parameters.AddWithValue("@user_type", 2);
            //...
            
            ////alternative approach 
            //command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = pName;
            //command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = pSurname;
            //command.Parameters.Add("@user_type", SqlDbType.SmallInt).Value = 2;

            return command.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
        finally
        {
            CloseConnection();
        }
    }

    /// <summary>
    /// Parameterized SQL statement
    /// </summary>
    /// <param name="pUserID"></param>
    /// <returns></returns>
    public int DeleteUser(int pUserID)
    {
        try
        {
            if (connection.State != ConnectionState.Open)
                OpenConnection(connectionString);

            SqlCommand command = new SqlCommand("DELETE FROM tblUser WHERE UserID=@user_id", connection);
            command.Parameters.AddWithValue("@user_id", pUserID);
            return command.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
        finally
        {
            
            CloseConnection();
        }
    }

    public int UpdateUser(string pFirstName, string pEmail, int pUserID)
    {
        try
        {
            if (connection.State != ConnectionState.Open)
                OpenConnection(connectionString);

            SqlCommand command = new SqlCommand("UPDATE tblUser SET FirstName=@first_name, Email=@user_email WHERE UserID=@user_id", connection);
            command.Parameters.AddWithValue("@first_name", pFirstName);
            command.Parameters.AddWithValue("@user_email", pEmail);
            command.Parameters.AddWithValue("@user_id", pUserID);
            //... you may add additional parameters based on your scenario

            return command.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
        finally
        {

            CloseConnection();
        }
    }
    #endregion
}