using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZhuJiangDataMigration
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 包含薪酬项的列名
        /// </summary>
        List<string> salaryColumnNameList = new List<string>();

        /// <summary>
        /// 薪酬项ids
        /// </summary>
        List<int> salaryItemIdList = new List<int>();

        /// <summary>
        /// 薪酬项目对照表
        /// </summary>
        List<SalaryItemMapper> salaryItemMappers;

        /// <summary>
        /// 数据源表Datatable
        /// </summary>
        DataTable sourcesData;
        /// <summary>
        /// 薪酬方案对照表
        /// 滕泉编码--金蝶编码
        /// </summary>
        Dictionary<string, string> planMapper = new Dictionary<string, string> {
            {"XCFA0002","GZ002" },
            {"XCFA0003","GZ004" },
            {"XCFA0004","GZ001" },
            {"XCFA0005","GZ005" },
            {"XCFA0006","GZ003" },
            {"FZFA0001","JJ002" },
            {"FZFA0002","JJ001" },
        };

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.cmbStartYear.SelectedIndex = 0;
            this.cmbStartMonth.SelectedIndex = 0;
            this.cmbEndYear.SelectedIndex = 0;
            this.cmbEndMonth.SelectedIndex = 0;
            this.cmbDataSource.SelectedIndex = 0;
        }

        private void btn_SelectMapper_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = "C:\\",
                    Filter = "Excel表格|*.xlsx;*.xls",
                    RestoreDirectory = false,
                    FilterIndex = 1,
                    Title = "数据迁移_酬项目对照表选择",
                    ValidateNames = true
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    salaryItemMappers = GetSalaryMapper(openFileDialog.FileName).Where(c => !string.IsNullOrWhiteSpace(c.KDSalaryNumber)).ToList();
                    if (salaryItemMappers != null)
                    {
                        this.txt_mapperExclePath.Text = openFileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<CostAssign> costAssigns = new List<CostAssign>();
            string startYear = cmbStartYear.SelectedItem.ToString();
            string startMonth = cmbStartMonth.SelectedItem.ToString();
            string endYear = cmbEndYear.SelectedItem.ToString();
            string endMonth = cmbEndMonth.SelectedItem.ToString();
            string sourceType = cmbDataSource.SelectedItem.ToString();
            string mapperPath = this.txt_mapperExclePath.Text;
            if (string.IsNullOrWhiteSpace(this.txt_mapperExclePath.Text))
            {
                MessageBox.Show("请先选择薪酬项目对照表。");
                return;
            }
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            sourcesData = GetMigrationSource(startYear, startMonth, endYear,endMonth,sourceType);
            stopwatch1.Stop();
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            #region 转成要导出Excel的数据集合
           
            foreach (DataRow row in sourcesData.Rows)
            {
              
                foreach (var colName in salaryColumnNameList)
                {
                    var mapper = salaryItemMappers.FirstOrDefault(c => c.TQSalaryId == Convert.ToInt32(colName.Substring(7, 6)));
                    if (mapper != null && !string.IsNullOrWhiteSpace(row[colName].ToString()))//对照表有 且当前列的值不为null或者空
                    {
                        var costAssign = new CostAssign
                        {
                            Year = row["FYEAR"].ToString(),
                            Month = row["FPERIOD"].ToString().PadLeft(2,'0'),
                            PlanNum = planMapper[row["PlanNum"].ToString()],
                            PlanName = row["PlanName"].ToString(),
                            StaffNum = row["StaffNum"].ToString(),
                            StaffName = row["StaffName"].ToString(),
                            DepartmentNum = row["DeptNumber"].ToString(),
                            DepartmentName = row["DeptName"].ToString(),
                            CostDepartmentNum = row["DeptNumber"].ToString(),
                            CostDepartmentName = row["DeptName"].ToString(),
                            SalaryItemNum = mapper.KDSalaryNumber,
                            SalaryItemName = mapper.KDSalaryName,
                            SalaryItemValue = mapper.DataType.Equals(DataTypeEnum.金额型) ? row[colName].ToString() : string.Empty,
                            SalaryItemTextValue = (mapper.DataType.Equals(DataTypeEnum.文本型) || mapper.DataType.Equals(DataTypeEnum.关联型)) ? row[colName].ToString() : string.Empty,
                            PostNum = row["PostNum"].ToString(),
                            PostName = row["PostName"].ToString()
                        };
                        bool isDecimalItem = decimal.TryParse(row[colName].ToString(), out decimal salaryValue);
                        if (isDecimalItem)
                        {
                            if (salaryValue != 0m)
                            {
                                costAssigns.Add(costAssign);
                            }
                        }
                        else
                        {
                            costAssigns.Add(costAssign);
                        }
                    }
                }
            }
            #endregion
            stopwatch2.Stop();

            if (costAssigns.Any())
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx;*.xls",
                    FilterIndex = 1,
                    FileName = $@"{startYear}{startMonth.PadLeft(2, '0')}-{endYear}{endMonth.PadLeft(2,'0')}{sourceType}_费用分配单引入数据.xlsx"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    Stopwatch stopwatch3 = new Stopwatch();
                    stopwatch3.Start();
                    #region 集合处理 转成对应DataTable
                    costAssigns = costAssigns.OrderBy(c => c.Year).ThenBy(c => c.Month).ThenBy(c => c.PlanNum).ThenBy(c => c.StaffNum).ToList();
                    costAssigns.ForEach(c => c.Seq = (costAssigns.IndexOf(c) + 1).ToString());
                    var groupbyResult = costAssigns.GroupBy(c => new { c.Year, c.Month, c.PlanNum }).ToList();
                    int billID = 100001;
                    foreach (var item in groupbyResult)
                    {
                        //var model = item.FirstOrDefault();
                        var model = costAssigns.FirstOrDefault(c => c.Year == item.Key.Year && c.Month == item.Key.Month && c.PlanNum == item.Key.PlanNum && c.StaffNum == item.FirstOrDefault().StaffNum);
                        model.BillID = billID.ToString();
                        model.OrgID = 100.ToString();
                        model.BusinessDate = $"{model.Year}/{model.Month}/{DateTime.DaysInMonth(Convert.ToInt32(model.Year), Convert.ToInt32(model.Month))}";
                        item.Skip(1).ToList().ForEach(c=> {
                            c.Year = string.Empty;
                            c.Month = string.Empty;
                            c.PlanNum = string.Empty;
                            c.PlanName = string.Empty;
                            });
                        billID++;
                    }
                    var dt = ToDataTable<CostAssign>(costAssigns);
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(dt);
                    stopwatch3.Stop();
                    #endregion
                    Stopwatch stopwatch4 = new Stopwatch();
                    stopwatch4.Start();
                    DataSetToExcel(dataSet, path);
                    //bool isCompleted = CreateExcel(costAssigns, path);
                    stopwatch4.Stop();
                  bool  isCompleted = true;
                  
                   
                    if (isCompleted)
                    {
                        MessageBox.Show($" 生成Excel成功。 从数据库读取数据耗时：{stopwatch1.ElapsedMilliseconds/1000}秒；  转成List集合 耗时：{stopwatch2.ElapsedMilliseconds/1000}秒；  集合处理 转成对应DataTable 耗时：{stopwatch3.ElapsedMilliseconds/1000}秒；datatable生成Excel文件 耗时：{stopwatch4.ElapsedMilliseconds/1000}秒");
                        //System.Diagnostics.Process.Start("Explorer.exe", path.Substring(0, path.LastIndexOf("\\")));
                    }
                }
            }


        }

        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            salaryItemMappers = new List<SalaryItemMapper>();
            string sourceType = this.cmbDataSource.Text;
            string path = this.txt_mapperExclePath.Text;
            if (!string.IsNullOrWhiteSpace(path))
            {
                salaryItemMappers = GetSalaryMapper(path).Where(c => !string.IsNullOrWhiteSpace(c.KDSalaryNumber)).ToList();
            }

        }
        #region 获取需要迁移的数据源
        /// <summary>
        /// 获取需要迁移的数据源
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private DataTable GetMigrationSource(string startYear, string startMonth, string endYear,string endMonth, string sourceType)
        {
            string table = string.Empty;
            if (sourceType.Trim() == "辅助核算发放表") table = "top_PA_AuxSum";
            if (sourceType.Trim() == "员工工资发放表") table = "top_PA_AcctBill_Pay";


            string sourceSQL = $@"Select t3.FYEAR, t3.FPERIOD,t4.FNUMBER as StaffNum,t4_L.FNAME as StaffName,
                            t5.FNUMBER as DeptNumber, t5_L.FNAME as DeptName,
                            t6.FNUMBER as PostNum, t6_L.FNAME as PostName,
                            t1.FPAYPLANID, t7.FNUMBER as PlanNum, t7_L.FNAME as PlanName,
                            {string.Join(",", salaryColumnNameList)}  
                            FROM {table}  t1
                            INNER JOIN {table}Entry t2 ON t1.FID=t2.FID
                            INNER JOIN top_PA_Period t3 ON t1.FPERIODID=t3.FENTRYID 
                                AND  (t3.FYEAR BETWEEN @StartYear AND @EndYear) 
                                AND (t3.FPERIOD BETWEEN @StartMonth AND @EndMonth)
                            INNER JOIN T_HR_EMPINFO t4 ON  t2.FCLOUDEMPID=t4.FID
                            INNER JOIN T_HR_EMPINFO_L t4_L ON  t2.FCLOUDEMPID=t4_L.FID
                            INNER JOIN T_BD_DEPARTMENT t5 ON  t2.FPERSONDEPTID=t5.FDEPTID
                            INNER JOIN T_BD_DEPARTMENT_L t5_L ON  t2.FPERSONDEPTID=t5_L.FDEPTID
                            INNER JOIN T_ORG_POST t6 ON t2.FPERSONPOSTID=t6.FPOSTID
                            INNER JOIN T_ORG_POST_L t6_L ON t2.FPERSONPOSTID=t6_L.FPOSTID
                            INNER JOIN Top_PA_PayPlan t7 ON t1.FPAYPLANID=t7.FID
                            INNER JOIN Top_PA_PayPlan_L t7_L ON t1.FPAYPLANID=t7_L.FID
                            ORDER BY t4.FNUMBER ASC"; 
            
            var sqlParameters = new SqlParameter[] {
                new SqlParameter("@StartYear",startYear),
                new SqlParameter("@EndYear",endYear),
                new SqlParameter("@StartMonth",startMonth),
                new SqlParameter("@EndMonth",endMonth)
            };

            var sourcesData = SQLHepler.ExecuteDataTable(sourceSQL, sqlParameters);
            return sourcesData;
        }

        #endregion

        #region DataTable to Excel
        private void DataSetToExcel(DataSet dataSet, string filePath)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage())
            {
                foreach (DataTable dataTable in dataSet.Tables)
                {
                    ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add(dataTable.TableName);
                    workSheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                    workSheet.Cells["A1:Y1"].AutoFilter = true;
                    workSheet.Cells.AutoFitColumns();
                }
                pck.Workbook.Properties.Title = "费用分配单引入数据";
                pck.Workbook.Properties.Author = "周房城";
                pck.Workbook.Properties.Company = "金蝶医疗";
                pck.SaveAsAsync(new FileInfo(filePath));
            }
        }
        #endregion

        #region 获取薪酬项目对照表
        /// <summary>
        /// 获取薪酬项目对照表
        /// </summary>
        /// <param name="salaryItemIdList">列里包含的里包含的薪酬项目id集合</param>
        /// <returns></returns>
        private List<SalaryItemMapper> GetSalaryMapper(string mapperPath)
        {
            var mapper = new List<SalaryItemMapper>();
            salaryItemIdList = new List<int>();
            salaryColumnNameList = new List<string>();
            string sourceType = cmbDataSource.SelectedItem.ToString();
            string table = string.Empty;
            if (sourceType.Trim() == "辅助核算发放表") table = "top_PA_AuxSum";
            if (sourceType.Trim() == "员工工资发放表") table = "top_PA_AcctBill_Pay";

            string colnumSQL = $" SELECT TOP 1 * FROM {table}Entry WHERE 1=@filter";
            var colDataTable = SQLHepler.ExecuteDataTable(colnumSQL, new SqlParameter("@filter", 1));
            foreach (DataColumn col in colDataTable.Columns)
            {
                if (col.ColumnName.Contains("FFIELD_") && int.TryParse(col.ColumnName.Substring(7, 6).ToString(), out int result))
                {
                    salaryColumnNameList.Add(col.ColumnName);
                    salaryItemIdList.Add(result);
                }
            }
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage(new FileStream(mapperPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
            {
                var worksheet = pck.Workbook.Worksheets.First();
                if (worksheet != null)
                {
                    mapper = worksheet.ConvertSheetToObjects<SalaryItemMapper>().ToList();
                }
            }

            string salaryItemsSQL = $@"SELECT FID,FNUMBER,FDATATYPE FROM top_pa_payitem WHERE FID IN ({string.Join(",", salaryItemIdList)})";
            var salaryItems = SQLHepler.ExecuteDataTable(salaryItemsSQL, new SqlParameter("@ids", ""));

            foreach (DataRow row in salaryItems.Rows)
            {
                var model = mapper.FirstOrDefault(c => c.TQSalaryNumber == row["FNUMBER"].ToString());
                if (model != null)
                {
                    mapper.FirstOrDefault(c => c.TQSalaryNumber == row["FNUMBER"].ToString()).TQSalaryId = Convert.ToInt32(row["FID"]);
                    mapper.FirstOrDefault(c => c.TQSalaryNumber == row["FNUMBER"].ToString()).DataType = (DataTypeEnum)Convert.ToInt32(row["FDATATYPE"]);
                }
            }
            return mapper;
        }
        #endregion

        #region 将数据生成Excel
        /// <summary>
        /// 将数据生成Excel
        /// </summary>
        /// <param name="costAssigns"></param>
        /// <param name="excelName"></param>
        private bool CreateExcel(List<CostAssign> costAssigns, string excelName)
        {
            FileInfo excelFile = new FileInfo(excelName);
            if (excelFile.Exists)
            {
                excelFile.Delete();
            }
            using (ExcelPackage excelPackage = new ExcelPackage(new FileInfo(excelName)))
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("sheet1");

                var recordIndex = 1;
                foreach (var item in costAssigns)
                {
                    worksheet.Row(recordIndex);
                    if (recordIndex == 1)
                    {
                        #region 表头
                        worksheet.Cells[recordIndex, 1].Value = "单据头序号";
                        worksheet.Cells[recordIndex, 2].Value = "年度";
                        worksheet.Cells[recordIndex, 3].Value = "期间";
                        worksheet.Cells[recordIndex, 4].Value = "备注";
                        worksheet.Cells[recordIndex, 5].Value = "组织编码";
                        worksheet.Cells[recordIndex, 6].Value = "方案#编码";
                        worksheet.Cells[recordIndex, 7].Value = "方案#名称";
                        worksheet.Cells[recordIndex, 8].Value = "业务日期";
                        worksheet.Cells[recordIndex, 9].Value = "序号";
                        worksheet.Cells[recordIndex, 10].Value = "员工代码#编码";
                        worksheet.Cells[recordIndex, 11].Value = "员工代码#名称";
                        worksheet.Cells[recordIndex, 12].Value = "所属部门#编码";
                        worksheet.Cells[recordIndex, 13].Value = "所属部门#名称";
                        worksheet.Cells[recordIndex, 14].Value = "费用承担部门#编码";
                        worksheet.Cells[recordIndex, 15].Value = "费用承担部门#名称";
                        worksheet.Cells[recordIndex, 16].Value = "项目代码#编码";
                        worksheet.Cells[recordIndex, 17].Value = "项目代码#名称";
                        worksheet.Cells[recordIndex, 18].Value = "项目值（数值）";
                        worksheet.Cells[recordIndex, 19].Value = "项目值（文本值）";
                        worksheet.Cells[recordIndex, 20].Value = "岗位#编码";
                        worksheet.Cells[recordIndex, 21].Value = "岗位#名称";
                        #endregion
                    }
                    
                    recordIndex++;
                    worksheet.Cells[recordIndex, 1].Value = item.BillID;
                    worksheet.Cells[recordIndex, 2].Value = item.Year;
                    worksheet.Cells[recordIndex, 3].Value = item.Month;
                    worksheet.Cells[recordIndex, 4].Value = item.Remark;
                    worksheet.Cells[recordIndex, 5].Value = item.OrgID;
                    worksheet.Cells[recordIndex, 6].Value = item.PlanNum;
                    worksheet.Cells[recordIndex, 7].Value = item.PlanName;
                    worksheet.Cells[recordIndex, 8].Value = item.BusinessDate;
                    worksheet.Cells[recordIndex, 9].Value = recordIndex-1;

                    worksheet.Cells[recordIndex, 10].Value = item.StaffNum;
                    worksheet.Cells[recordIndex, 11].Value = item.StaffName;
                    worksheet.Cells[recordIndex, 12].Value = item.DepartmentNum;
                    worksheet.Cells[recordIndex, 13].Value = item.DepartmentName;
                    worksheet.Cells[recordIndex, 14].Value = item.CostDepartmentNum;
                    worksheet.Cells[recordIndex, 15].Value = item.CostDepartmentName;
                    worksheet.Cells[recordIndex, 16].Value = item.SalaryItemNum;
                    worksheet.Cells[recordIndex, 17].Value = item.SalaryItemName;
                    worksheet.Cells[recordIndex, 18].Value = "\t" + item.SalaryItemValue;
                    worksheet.Cells[recordIndex, 19].Value = "\t" + item.SalaryItemTextValue;
                    worksheet.Cells[recordIndex, 20].Value = item.PostNum;
                    worksheet.Cells[recordIndex, 21].Value = item.PostName;
                }
                excelPackage.Workbook.Properties.Title = "费用分配单引入数据";
                excelPackage.Workbook.Properties.Author = "周房城";
                excelPackage.Workbook.Properties.Company = "金蝶医疗";
                worksheet.Cells.AutoFitColumns();
                worksheet.Cells["A1:K1"].AutoFilter = true;
                Task task = excelPackage.SaveAsync();
                task.Wait();
                return task.IsCompleted;

            }
        }

        #endregion

        #region  转成DataTable
        /// <summary>
        /// 转成DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.GetCustomAttributes<ExcelColumn>().First().ColumnName, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        #endregion
    }
}
