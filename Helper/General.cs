using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace EncriptionAESWebservice.Helper
{
    public class General
    {
    }

    public class DataAbsensi
    {
        public int id { get; set; }
        public string nomor_pegawai { get; set; }
        public string nama_pegawai { get; set; }
        public string jam_masuk { get; set; }
        public string jam_keluar { get; set; }
        public string tanggal_absen { get; set; }
        public string geolocation { get; set; }
        public string address { get; set; }
        public string ip_address { get; set; }
    }

    public class SingleRequest 
    { 
        public string key { get; set; }
        public string value { get; set; }
    }

    public class MultiRequest
    {
        public string key { get; set; }
        public List<DataAbsensi> data { get; set; }
    }
    public class SingleResult
    {
        public string key { get; set; }
        public string value_result { get; set; }
    }

    public class ResultApi
    {
        public int  statusCode { get; set; } = 200;
        public string message { get; set; } = "success";
        public  object data { get; set; }
    }

}
