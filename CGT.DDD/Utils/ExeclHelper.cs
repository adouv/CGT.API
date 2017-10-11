using CGT.DDD.Utils.File;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver.Core.Misc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CGT.DDD.Utils {

    /// <summary>
    /// execl接口
    /// </summary>
    public interface IExecl {
        ExeclTalbe ExcelImport(Stream filename);
        byte[] ExcelExport<T>(List<T> dataList, List<string> TitleList, List<string> KeyList);
    }

    /// <summary>
    /// 实现execl
    /// </summary>
    public class ExeclHelper : IExecl {
        private IHostingEnvironment _hostingEnvironment;

        static Object locker = new Object();

        public ExeclHelper(IHostingEnvironment hostingEnvironment) {
            _hostingEnvironment = hostingEnvironment;
        }

        #region 导入
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public ExeclTalbe ExcelImport(Stream filename) {
            var tables = new ExeclTalbe();
            var rows = new List<Rows>();

            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));

            try {
                using (FileStream fs = new FileStream(file.ToString(), FileMode.Create)) {
                    filename.CopyTo(fs);
                    fs.Flush();
                }

                using (ExcelPackage package = new ExcelPackage(file)) {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;
                    int ColCount = worksheet.Dimension.Columns;
                    for (int execlrow = 1; execlrow <= rowCount; execlrow++) {
                        var cols = new List<Columns>();
                        var row = new Rows();
                        //记录当前行号
                        row.RIndex = execlrow;
                        for (int execlcol = 1; execlcol <= ColCount; execlcol++) {
                            var col = new Columns() {
                                ColumnValue = worksheet.Cells[execlrow, execlcol].Value == null ? "" : worksheet.Cells[execlrow, execlcol].Value.ToString()
                            };

                            cols.Add(col);
                        }
                        row.columns = cols;
                        rows.Add(row);
                    }
                    //赋值table
                    tables.rows = rows;
                }

                file.Delete();
            }
            catch (Exception ex) {
                throw ex;
            }

            return tables;
        }

        #endregion


        #region 导出
        public byte[] ExcelExport<T>(List<T> dataList, List<string> TitleList, List<string> KeyList) {
            lock (locker) {
                string sWebRootFolder = _hostingEnvironment.WebRootPath + @"\ExcelOutput\";
                if (!Directory.Exists(sWebRootFolder)) {
                    Directory.CreateDirectory(sWebRootFolder);
                }
                string sFileName = $"{Guid.NewGuid()}.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                using (ExcelPackage package = new ExcelPackage(file)) {
                    // 添加worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                    //添加头
                    worksheet.Name = "workbook";
                    Type type = typeof(T);
                    PropertyInfo[] propertys = type.GetProperties();
                    List<PropertyInfo> propertys_newListorder = new List<PropertyInfo>();
                    foreach (var pi in KeyList) {
                        if (propertys.Where(p => p.Name == pi).Count() == 1) {
                            propertys_newListorder.Add(propertys.Where(p => p.Name == pi).FirstOrDefault());
                        }
                    }
                    int hangshu = dataList.Count;
                    int lieshu = propertys_newListorder.Count();
                    var propertys_newarry = propertys_newListorder.ToArray();

                    //填写标题
                    for (int t = 0; t < TitleList.Count; t++) {
                        worksheet.Cells[1, (t + 1)].Value = TitleList[t];
                    }

                    //添加值
                    for (int i = 0; i < hangshu; i++) {
                        for (int j = 0; j < lieshu; j++) {
                            //string值自动格式化 execl单元格
                            if (propertys_newarry[j].PropertyType == typeof(Nullable<Byte>)) {
                                worksheet.Cells[(i + 2), (j + 1)].Value = ConvertIsByteNuablle((byte?)propertys_newarry[j].GetValue(dataList[i], null));
                            } else if (propertys_newarry[j].PropertyType == typeof(DateTime)){
                                worksheet.Cells[(i + 2), (j + 1)].Value =  propertys_newarry[j].GetValue(dataList[i], null).ToString();
                            }
                            else {
                                worksheet.Cells[(i + 2), (j + 1)].Value = propertys_newarry[j].GetValue(dataList[i], null);
                            }
                        }
                    }
                    //保存文件
                    package.Save();

                    var stream = FileUtily.FileToStream(Path.Combine(sWebRootFolder, sFileName));
                    var ExcelBytes = FileUtily.StreamToBytes(stream);

                    //删除文件
                    file.Delete();

                    return ExcelBytes;
                }
            }
        }

        /// <summary>
        /// 是否null
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public string ConvertIsByteNuablle(byte? mod) {
            if (mod != null) {
                switch (mod) {
                    case 0:
                        return "否";
                    case 1:
                        return "是";
                    default:
                        return null;
                }
            }
            else {
                return null;
            }
        }


    }

    #endregion


    public class ExeclTalbe {
        public List<Rows> rows { get; set; }
    }

    public class Rows {
        public int RIndex { get; set; }
        public List<Columns> columns { get; set; }
    }

    public class Columns {
        public string ColumnValue { get; set; }
    }
}
