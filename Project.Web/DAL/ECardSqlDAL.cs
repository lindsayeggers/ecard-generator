using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Web.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Project.Web.DAL
{
    public class ECardSqlDAL : IECardDAL
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["ecards"].ConnectionString;
        const string SQL_GetAllCards = "SELECT * FROM card_templates";
        const string SQL_GetATemplate = "SELECT * FROM card_templates WHERE card_templates.id = @input";
        const string SQL_SaveCard = "INSERT INTO cards VALUES(@template_id, @to_email, @to_name, @message, @from_name, @from_email)";


        public List<CardsModel> GetAllCards()
        {
            List<CardsModel> ecards = new List<CardsModel>();

            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllCards, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CardsModel c = new CardsModel();
                        c.CardId = Convert.ToInt32(reader["Id"]);
                        c.CardName = Convert.ToString(reader["name"]);
                        c.ImageName = Convert.ToString(reader["imageName"]);
                        c.FontColor = Convert.ToString(reader["fontColor"]);

                        ecards.Add(c);

                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return ecards;
        }

        public CardsModel GetATemplate(int id)
        {
            CardsModel card = new CardsModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetATemplate, conn);
                    cmd.Parameters.AddWithValue("@input", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        CardsModel c = new CardsModel();
                        c.CardName = Convert.ToString(reader["name"]);
                        c.ImageName = Convert.ToString(reader["imageName"]);
                        c.FontColor = Convert.ToString(reader["fontColor"]);
                        c.TemplateId = id;
                        card = c;
                    }
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
            return card;
        }

        public void SaveNewCard(CardsModel c)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    CardsModel e = new CardsModel();
                    SqlCommand cmd = new SqlCommand(SQL_SaveCard, conn);
                    cmd.Parameters.AddWithValue("@template_id", e.TemplateId);
                    cmd.Parameters.AddWithValue("@to_email", e.ToEmail);
                    cmd.Parameters.AddWithValue("@to_name", e.ToName);
                    cmd.Parameters.AddWithValue("@message", e.Message);
                    cmd.Parameters.AddWithValue("@from_name", e.FromName);
                    cmd.Parameters.AddWithValue("@from_email", e.FromEmail);

                    cmd.ExecuteNonQuery();

                    return;
                   
                }
            }
            catch(SqlException ex)
            {
                throw;
            }
        }
    }
}