using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;

namespace WxAppWebApi.service.FileService.Impl
{
    public class FileServiceImpl : IFileService
    {


        /// <summary>
        /// 保存文件的具体实现，返回文件名。
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> UpLoadFile(IFormFile file, string filename, string pathextra)
        {
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = filename + extension;

                var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
                var filepath = Path.Combine(baseDirectory, pathextra);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(filepath, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return filename;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
        /// 根据文件名下载文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        async Task<byte[]> IFileService.GetFileBytesAsync(string filename)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("File not found", filename);
            }

            return await System.IO.File.ReadAllBytesAsync(filepath);
        }
    }
}
