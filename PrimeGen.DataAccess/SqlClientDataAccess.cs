using PrimeGen.TransferObjects;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace PrimeGen.DataAccess
{
    public class SqlClientDataAccess : IDataAccess
    {
        public string ConnectionString { get; set; }

        public void AddBigIntegerPrime(BigPrime prime)
        {
            //todo: later
            throw new NotImplementedException("Later");
        }

        public void AddSmallPrime(SmallPrime prime)
        {
            var insertStatement = "INSERT SmallPrime (PrimeValue) values (@PrimeValue);";
            var theCommand = new System.Data.SqlClient.SqlCommand(insertStatement, new SqlConnection(ConnectionString));

            theCommand.Parameters.AddWithValue("PrimeValue", prime.PrimeValue);
            
            theCommand.Connection.Open();
            theCommand.ExecuteNonQuery();
            theCommand.Connection.Close();
        }

        public IEnumerable<BigPrime> GetBigPrimes()
        {
            //todo: later


            return Enumerable.Empty<BigPrime>();

            using (var dataTable = new System.Data.DataTable())
            {
                var selectCommandText = "select PrimeValue from BigPrime;";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommandText, ConnectionString);
                dataAdapter.SelectCommand.Connection.Open();
                dataAdapter.Fill(dataTable);
                dataAdapter.SelectCommand.Connection.Close();
            }

            
        }

        public IEnumerable<SmallPrime> GetSmallPrimes()
        {


            using (var dataTable = new System.Data.DataTable())
            {
                
                var selectCommandText = "select PrimeValue from SmallPrime;";
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommandText, ConnectionString))
                {
                    dataAdapter.SelectCommand.Connection.Open();
                    dataAdapter.Fill(dataTable);
                    dataAdapter.SelectCommand.Connection.Close();
                }

                List<SmallPrime> returnable = new List<SmallPrime>();
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        returnable.Add(ConvertSmallPrimeToTransferObject(row));
                    }
                }
                return returnable;
            }
        }

        private BigPrime ConvertBigPrimeToTransferObject(DataRow row)
        {
            
            throw new NotImplementedException("future release");
            //BigInteger primeValue = new BigInteger(row["PrimeValue"]);
            //return new BigPrime(primeValue);
        }

        private SmallPrime ConvertSmallPrimeToTransferObject(DataRow row)
        {
            long primeValue = (long)row["PrimeValue"];
            return new SmallPrime(primeValue);
        }

    }
}
