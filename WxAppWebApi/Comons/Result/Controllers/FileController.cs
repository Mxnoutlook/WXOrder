using WxAppWebApi.Comons.AOP.Filters;
using WxAppWebApi.Comons.Result;
using WxAppWebApi.service.FileService;
using WxAppWebApi.service.OrderService;
using WxAppWebApi.service.UserService;
using Masuit.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WxAppWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileservice;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public FileController(IFileService fileService, IUserService userService,IOrderService orderService)
        {
            _fileservice = fileService;
            _userService = userService;
            _orderService = orderService;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        public async Task<ResultJson> UploadFile(IFormFile file, [FromForm] string filename, [FromForm] string ordercode)
        {

            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            // 1、根据ordercode获得用户公司/邮箱地址——联表查询
            var extrapath = _userService.GetCompNameByOrderId(ordercode);

            // 2、拼接文件的路径：用户公司名+邮件地址+订单号+文件
            extrapath = extrapath + "\\" + ordercode;

            // 3、拼接文件的数据信息
            var baseDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
            var filepath = Path.Combine(baseDirectory, extrapath);
            filepath = Path.Combine(filepath, filename+ extension);
            try
            {
                // 调用异步上传文件方法
                var FileresultData = await _fileservice.UpLoadFile(file, filename, extrapath);
                var FilepathData =_orderService.UpdateOrderFilePath(filepath, ordercode);
                var resultdata = new
                {
                    FileUpload = FileresultData,
                    FilePathUpdate = FilepathData
                };

                // 处理 resultData，可能需要将其转换为 ResultJson 类型
                return ResultTool.Success(resultdata);
            }
            catch (Exception ex)
            {
                // 返回错误信息
                return ResultTool.Fail(ex.Message);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fileService"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var bytes = await _fileservice.GetFileBytesAsync(filename);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filename, out var contenttype))
                {
                    contenttype = "application/octet-stream";
                }

                return File(bytes, contenttype, Path.GetFileName(filename));
            }
            catch (FileNotFoundException ex)
            {
                // 处理文件未找到异常
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // 处理其他异常
                return BadRequest(ex.Message);
            }
        }
    }
}
