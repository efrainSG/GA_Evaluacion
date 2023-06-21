using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;

namespace BLService
{
    public class BLService : IBLService<TicketDto>
    {
        public BLService()
        {
                
        }

        public string connstr { get; set; }

        public async Task<IEnumerable<TicketDto>> GetAll()
        {
            List<TicketDto> result = new List<TicketDto>();
            SqlDataReader dr = null;
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand("getTickets", conn))
                {
                    conn.Open();
                    dr = cmd.ExecuteReader();
                }
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        result.Add(new TicketDto()
                        {
                            FechaHora = (DateTime)dr["FechaHora"],
                            IdRegistradora = dr["Id_Registradora"].ToString(),
                            IdTienda = dr["Id_Tienda"].ToString(),
                            Impuesto = (decimal)dr["Impuesto"],
                            Ticket = (int)dr["Ticket"],
                            Total = (decimal)dr["Total"],
                            FechaHora_Creacion = (DateTime)dr["FechaHora_Creacion"]
                        });
                    }
                }
                conn.Close();
            }
            return result.AsEnumerable();
        }

        public async Task<int> Save(TicketDto dto)
        {
            int result = 0;
            string msg = string.Empty;
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                using (SqlCommand cmd = new SqlCommand("ins_Ticket", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@Ticket", Value = dto.Ticket, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@idTienda", Value = dto.IdTienda, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@idRegistradora", Value = dto.IdRegistradora, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@FechaHora", Value = dto.FechaHora, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@Impuesto", Value = dto.Impuesto, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@Total", Value = dto.Total, Direction = System.Data.ParameterDirection.Input });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@msg", Value = msg, Direction = System.Data.ParameterDirection.Output});
                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            
            return result;
        }
    }
}