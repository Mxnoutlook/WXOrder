using Newtonsoft.Json.Linq;

namespace WxAppWebApi.service.FileService
{
    /// <summary>
    /// 文件接口，用来写上传和下载文件的方法
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<String> UpLoadFile(IFormFile file, string filename, string pathextra);


        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<byte[]> GetFileBytesAsync(string filename);

    }
}
