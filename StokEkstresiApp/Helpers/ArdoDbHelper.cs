using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StokEkstresiApp.Helpers
{
    public class ArdoDbHelper
    {
        private readonly string _connectionString;

        // Constructor ile bağlantı stringini alıyoruz
        public ArdoDbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Stok ekstresi verisini almak için kullanılan method
        public DataTable GetirStokEkstresi(string malKodu, DateTime baslangic, DateTime bitis)
        {
            var tablo = new DataTable();
            try
            {
                using (var baglanti = new SqlConnection(_connectionString))
                {
                    using (var komut = new SqlCommand("sp_StokEkstresi", baglanti))
                    {
                        komut.CommandType = CommandType.StoredProcedure;
                        komut.Parameters.AddWithValue("@MalKodu", malKodu);
                        komut.Parameters.AddWithValue("@BaslangicTarihi", baslangic);
                        komut.Parameters.AddWithValue("@BitisTarihi", bitis);

                        var adaptor = new SqlDataAdapter(komut);
                        adaptor.Fill(tablo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Stok ekstresi verisi alınırken bir hata oluştu: " + ex.Message);
            }

            return tablo;
        }
    }
}
