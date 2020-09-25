using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhuJiangDataMigration
{
   public  class CostAssign
    {
        [ExcelColumn("*单据头(序号)")] public string BillID { get; set; }
        [ExcelColumn("*(单据头)年度")]public string Year { get; set; }
        [ExcelColumn("*(单据头)期间")] public string Month { get; set; }
       [ExcelColumn("(单据头)备注")]public string Remark { get; set; }
       [ExcelColumn("(单据头)所属组织#编码")]public string OrgID { get; set; }
        [ExcelColumn("(单据头)所属组织#名称")] public string OrgName { get; set; }
       [ExcelColumn("(单据头)方案#编码")]public string PlanNum { get; set; }
       [ExcelColumn("(单据头)方案#名称")]public string PlanName { get; set; }
        [ExcelColumn("(单据头)业务日期")] public string BusinessDate { get; set; }
        [ExcelColumn("间隔列")] public string Split { get; set; }
        [ExcelColumn("*薪酬项目(序号)")] public string Seq { get; set; }
        [ExcelColumn("*(薪酬项目)员工代码#编码")]public string StaffNum { get; set; }
       [ExcelColumn("*(薪酬项目)员工代码#名称")]public string StaffName { get; set; }
       
       [ExcelColumn("(薪酬项目)所属部门#编码")]public string DepartmentNum { get; set; }
       [ExcelColumn("(薪酬项目)所属部门#名称")]public string DepartmentName { get; set; }
       
       [ExcelColumn("(薪酬项目)费用承担部门#编码")]public string CostDepartmentNum { get; set; }
       [ExcelColumn("(薪酬项目)费用承担部门#名称")]public string CostDepartmentName { get; set; }

        [ExcelColumn("(薪酬项目)职位#编码")] public string PostNum { get; set; }
        [ExcelColumn("薪酬项目)职位#名称")] public string PostName { get; set; }
        [ExcelColumn("(薪酬项目)项目代码#编码")]public string SalaryItemNum { get; set; }
       [ExcelColumn("(薪酬项目)项目代码#名称")]public string SalaryItemName { get; set; }

        [ExcelColumn("(薪酬项目)辅助资料值#编码")] public string FASSIANTTYPE { get; set; }
        [ExcelColumn("(薪酬项目)辅助资料值#名称")] public string FASSIANTTYPEName { get; set; }
        

        [ExcelColumn("(薪酬项目)项目值（数值）")]public string  SalaryItemValue { get; set; }
       
       [ExcelColumn("(薪酬项目)项目值（文本）")]public string SalaryItemTextValue { get; set; }
       
       




    }
}
