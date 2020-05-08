using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CzwartkKolos.Models;
using Newtonsoft.Json;

namespace CzwartkKolos.Services
{
    public class DbService : IDbservice
    {
        
        private string sqlConGlobal =
            "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=s18935; Integrated Security = true";


        
        public ProjectResult getProjects(String str)
        {
            ProjectResult projectResult = new ProjectResult();
            Console.WriteLine("From db service");

            using (SqlConnection sqlConnection = new SqlConnection(sqlConGlobal))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    String comStringTxt = "Select Project.IdProject, Project.Name, T.idTask, T.Name, T.Description, T.DeadLine from Project INNER JOIN Task T on Project.IdProject = T.IdProject where Project.idProject = " + str + " order by T.deadline";
                    command.CommandText = comStringTxt; 
                    sqlConnection.Open();

                    SqlDataReader sqlDr = command.ExecuteReader();
                    projectResult.tasks = new List<MyTask>();
                    while (sqlDr.Read())
                    {
                       
                        MyTask myTask = new MyTask()
                        {
                            idTask = Int32.Parse(sqlDr["idTask"].ToString()),
                            name = sqlDr[4].ToString(),
                            description = sqlDr["description"].ToString(),
                            deadline = sqlDr["Deadline"].ToString()
                        };
                        projectResult.name = sqlDr[1].ToString();
                        projectResult.id = Int32.Parse(sqlDr[0].ToString());
                        projectResult.tasks.Add(myTask);
                    }

                }
            }
            
            return projectResult; 
        }

        public Object postTask(Object Json)
        {
            var jResult = JsonConvert.DeserializeObject<dynamic>(Json.ToString());
            MyTask tResFinal = JsonConvert.DeserializeObject<MyTask>(Json.ToString());

            var tasktype = jResult.TaskType;

            TaskType taskResult = JsonConvert.DeserializeObject<TaskType>(tasktype.ToString());

            if (typeExists(taskResult.idTaskType))
            {
                Console.WriteLine("exists");
            }
            else
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlConGlobal))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sqlConnection;
                        String sqlStringTxt = "  INSERT INTO TaskType (IdTaskType, Name) values (" +
                                              taskResult.idTaskType + ", '" + taskResult.name + "')";

                        command.CommandText = sqlStringTxt; 
                        
                        
                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                    }
                }

            }

            using (SqlConnection sqlConnection = new SqlConnection(sqlConGlobal))
            {
                using (SqlCommand command = new SqlCommand())
                {
                
                    
                    command.Connection = sqlConnection;
                    String sqlStringTxt = " Insert into  Task (Name, Description, Deadline, IdProject, IdTaskType, IdAssignedTo, IdCreator) values(" +
                                          tResFinal.idTaskType + ", '" + tResFinal.name + ", '" + tResFinal.description + ", '" + tResFinal.deadline +", '" + tResFinal.idTeam + ", '" + tResFinal.idTaskType + ", '" + tResFinal.idAssignedTo + ", '" +tResFinal.idCreator + "')" ;

                    command.CommandText = sqlStringTxt; 
                    
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    
                    
                }


            }

            return Json;
        }

        public bool typeExists(string id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(sqlConGlobal))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = sqlConnection;
                    String commandStringTxt = "Select * from TaskType where idTaskType = " + id;
                    command.CommandText = commandStringTxt; 
                    
                    sqlConnection.Open();
                    SqlDataReader sqlDr = command.ExecuteReader();
                    
                    if (sqlDr.HasRows)
                    {
                        return true;
                    }else
                    {
                        return false; 
                    }
                }
                
            }
        }

    }
}