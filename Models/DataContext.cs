using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DETHI20.Models
{
    public class DataContext
    {
        public string ConnectionString { get; set; }

        public DataContext(string connectionstring)
        {
            this.ConnectionString = connectionstring;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        /*Thêm điểm cách ly*/
        public int sqlInsertDiemCachLy(DiemCachLyModel diemcachly)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = "insert into diemcachly values(@madiemcachly, @tendiemcachly,@diachi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("madiemcachly", diemcachly.MaDiemCachLy);
                cmd.Parameters.AddWithValue("tendiemcachly", diemcachly.TenDiemCachLy);
                cmd.Parameters.AddWithValue("diachi", diemcachly.DiaChi);
                return (cmd.ExecuteNonQuery());
            }
        }
        public List<Object> sqlListByTrieuChungCongNhan(int soTrieuChung)
        {
            List<Object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = @"select cn.tencongnhan, cn.namsinh,cn.nuocve,count(*) as SoTrieuChung
                              from congnhan cn join cn_tc on cn.macongnhan = cn_tc.macongnhan
                              group by cn.tencongnhan, cn.namsinh,cn.nuocve
                              having count(*) >= @InputTrieuChung";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("InputTrieuChung", soTrieuChung);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            TenCN = reader["tencongnhan"].ToString(),
                            NamSinh = Convert.ToInt32(reader["namsinh"]),
                            NuocVe = reader["nuocve"].ToString(),
                            SoTrieuChung = Convert.ToInt32(reader["SoTrieuChung"])
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
   
        }

        public List<DiemCachLyModel> sqlListDiemCachLy()
        {
            List<DiemCachLyModel> list = new List<DiemCachLyModel>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = @"select * from diemcachly";
                SqlCommand cmd = new SqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new DiemCachLyModel()
                        {
                            MaDiemCachLy = reader["madiemcachly"].ToString(),
                            TenDiemCachLy = reader["tendiemcachly"].ToString(),
                            DiaChi = reader["diachi"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public List<Object> sqlListByCongNhanDCL(string MaDiemCachLy)
        {
            List<object> list = new List<object>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = @"select macongnhan,tencongnhan
                              from congnhan
                              where madiemcachly = @inputdiemcachly";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("inputdiemcachly", MaDiemCachLy);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new
                        {
                            MaCN = reader["macongnhan"].ToString(),
                            TenCN = reader["tencongnhan"].ToString()
                        });
                    }
                    reader.Close();
                }
                conn.Close();
            }
            return list;
        }

        public int sqlDeteleCN(string macn)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = @"  Delete from congnhan
                               where macongnhan = @inputmacn";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("inputmacn", macn);
                return (cmd.ExecuteNonQuery());
            }
        }

        public CongNhanModel sqlGetCN(string macn)
        {
            CongNhanModel cn = new CongNhanModel();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                var query = @"select * from congnhan
                            where macongnhan = @inputmacn";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("inputmacn", macn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cn.MaCongNhan = reader["macongnhan"].ToString();
                        cn.TenCongNhan = reader["tencongnhan"].ToString();
                        cn.MaDiemCachLy = reader["madiemcachly"].ToString();
                        cn.GioiTinh = Convert.ToInt32(reader["gioitinh"].ToString());
                        cn.NuocVe = reader["nuocve"].ToString();
                        cn.NamSinh = Convert.ToInt32(reader["namsinh"].ToString());

                    }
                }
                conn.Close();
            }
            return cn;

        }
    }
}
