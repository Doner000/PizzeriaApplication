using Npgsql;


namespace DataBaseClass
{
    public class DB
    {
        private string connString = "Server=localhost;Username=postgres;Database=postgres;Port=5432;Password=lfvbh2000";

        public DB ()
        {
            
        }

        public void DataInput (string sqlString)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(sqlString, conn);
                command.ExecuteNonQuery();
                System.Console.WriteLine("Данные внесены в базу");
            }

            catch(Exception ex)
            {
                System.Console.WriteLine(ex);
            }   
        }
    }
}
