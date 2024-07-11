using EncriptionAESWebservice.Helper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.Json.Nodes;

namespace EncriptionAESWebservice.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AESController : ControllerBase
    {
     
        [HttpPost]
        public ContentResult AES_EncryptionSingle(SingleRequest param)
        {
            ResultApi resultApi = new ResultApi();
            SingleResult result = new SingleResult();
            result.key = param.key;

            try
            {
                if (param.key.Length < 16)
                {
                    resultApi.statusCode = 501;
                    resultApi.message = "Key length must be at least 16 characters.";
                }
                else
                {
                    result.value_result = AesEncryption.Encrypt(param.value, param.key);
                    resultApi.data = result;
                }
            }
            catch (Exception ex)
            {
                resultApi.statusCode = 502;
                resultApi.message = ex.Message;
            }

            var dtJson = JsonConvert.SerializeObject(resultApi); ;
            return Content(dtJson, "application/json");
        }

        [HttpPost]
        public ContentResult AES_EncryptionMultiple(MultiRequest param)
        {
            ResultApi resultApi = new ResultApi();
            List<DataAbsensi> datas = new List<DataAbsensi>();
            MultiRequest multiRequest= new MultiRequest();
            multiRequest.key = param.key;

            try
            {
                if (param.key.Length < 16)
                {
                    resultApi.statusCode = 501;
                    resultApi.message = "Key length must be at least 16 characters.";
                }
                else
                {
                    foreach (var item in param.data)
                    {
                        DataAbsensi absensi = new DataAbsensi();
                        absensi.id = item.id;
                        absensi.jam_masuk = AesEncryption.Encrypt(item.jam_masuk, param.key);
                        absensi.jam_keluar = AesEncryption.Encrypt(item.jam_keluar, param.key);
                        absensi.nomor_pegawai = AesEncryption.Encrypt(item.nomor_pegawai, param.key);
                        absensi.nama_pegawai = AesEncryption.Encrypt(item.nama_pegawai, param.key);
                        absensi.tanggal_absen = AesEncryption.Encrypt(item.tanggal_absen, param.key);
                        absensi.address = AesEncryption.Encrypt(item.address, param.key);
                        absensi.ip_address = AesEncryption.Encrypt(item.ip_address, param.key);
                        absensi.geolocation = AesEncryption.Encrypt(item.geolocation, param.key);
                        datas.Add(absensi);
                    }
                    multiRequest.data = datas;
                    resultApi.data = multiRequest;
                }
            }
            catch (Exception ex)
            {
                resultApi.statusCode = 502;
                resultApi.message = ex.Message;
            }

            var dtJson = JsonConvert.SerializeObject(resultApi);
            return Content(dtJson, "application/json");
        }
        [HttpPost]
        public ContentResult AES_EncryptionMultiple2([FromBody] Object param)
        {

            JObject json_object_request = JObject.Parse(param.ToString());

            // Access the key and data dynamically
            var key = json_object_request["key"].ToString();
            var data_request = json_object_request["data"] as JArray;

            // Deklarasikan JObject
            JObject json_object_response = new JObject();
            json_object_response["key"] = key;

            // Deklarasikan JArray
            JArray json_object_response_array = new JArray();

            // Optionally, process the data here
            foreach (var item in data_request)
            {
                JObject itejson_object_response_array_item = new JObject();

                
                // Akses nilai berdasarkan indeks (indeks dimulai dari 0)
                var id = item[0].ToObject<int>();
                var nomorPegawai = item[1].ToString();

                //var namaPegawai = item["nama_pegawai"].ToString();
                //var jamMasuk = item["jam_masuk"].ToString();
                //var jamKeluar = item["jam_keluar"].ToString();
                //var tanggalAbsen = item["tanggal_absen"].ToString();
                //var geolocation = item["geolocation"].ToString();
                //var address = item["address"].ToString();
                //var ipAddress = item["ip_address"].ToString();

            }

            // Convert the dynamic object back to JSON string if needed
            string json = param.ToString();

            // Return the JSON string as a ContentResult
            return Content(json, "application/json");
        }

        [HttpPost]
        public ContentResult AES_DecryptionSingle(SingleRequest param)
        {
          
            ResultApi resultApi = new ResultApi();
            SingleResult result = new SingleResult();
            result.key = param.key;
            try
            {
                if (param.key.Length < 16)
                {
                    resultApi.statusCode = 501;
                    resultApi.message = "Key length must be at least 16 characters.";
                }
                else
                {
                        result.value_result = AesEncryption.Decrypt(param.value, param.key);
                        resultApi.data = result;
                }
            }
            catch (Exception)
            {
                resultApi.statusCode = 502;
                resultApi.message = "Key and Value doesn't match";
            }

            var dtJson = JsonConvert.SerializeObject(resultApi); ;
            return Content(dtJson, "application/json");
        }
        [HttpPost]
        public ContentResult AES_DecryptionMultiple(MultiRequest param)
        {
            ResultApi resultApi = new ResultApi();
            List<DataAbsensi> datas = new List<DataAbsensi>();
            MultiRequest multiRequest = new MultiRequest();
            multiRequest.key = param.key;
            try
            {
                if (param.key.Length < 16)
                {
                    resultApi.statusCode = 501;
                    resultApi.message = "Key length must be at least 16 characters.";
                }
                else
                {
                    foreach (var item in param.data)
                    {
                        DataAbsensi absensi = new DataAbsensi();
                        absensi.id = item.id;
                        absensi.jam_masuk = AesEncryption.Decrypt(item.jam_masuk, param.key);
                        absensi.jam_keluar = AesEncryption.Decrypt(item.jam_keluar, param.key);
                        absensi.nomor_pegawai = AesEncryption.Decrypt(item.nomor_pegawai, param.key);
                        absensi.nama_pegawai = AesEncryption.Decrypt(item.nama_pegawai, param.key);
                        absensi.tanggal_absen = AesEncryption.Decrypt(item.tanggal_absen, param.key);
                        absensi.address = AesEncryption.Decrypt(item.address, param.key);
                        absensi.ip_address = AesEncryption.Decrypt(item.ip_address, param.key);
                        absensi.geolocation = AesEncryption.Decrypt(item.geolocation, param.key);
                        datas.Add(absensi);
                    }
                    multiRequest.data = datas;
                    resultApi.data = multiRequest;
                }
            }
            catch (Exception)
            {
                resultApi.statusCode = 502;
                resultApi.message = "Key and Value doesn't match";
            }
            var dtJson = JsonConvert.SerializeObject(resultApi);
            return Content(dtJson, "application/json");
        }
    }
}
