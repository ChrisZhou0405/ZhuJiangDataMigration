using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhuJiangDataMigration
{
   public  class SalaryItemMapper
    {
        /// <summary>
        /// 金蝶项目编码
        /// </summary>
        [ExcelColumn("金蝶编码")]
        public string KDSalaryNumber { get; set; }


        /// <summary>
        /// 金蝶项目名称
        /// </summary>
        [ExcelColumn("金蝶项目")]
        public string KDSalaryName { get; set; }

        /// <summary>
        /// 腾泉项目编码
        /// </summary>
        [ExcelColumn("腾泉编码")]
        public string TQSalaryNumber { get; set; }

        /// <summary>
        /// 腾泉项目名称
        /// </summary>
        [ExcelColumn("腾泉项目")]
        public string TQSalaryName { get; set; }

        /// <summary>
        /// 匹配方案
        /// </summary>
        [ExcelColumn("适用腾泉方案编码")]
        public string Plan { get; set; }

        //[ExcelColumn("备注")]
        //public string Remark { get; set; }
        /// <summary>
        /// 腾泉项目FID
        /// </summary>
        public int TQSalaryId { get; set; }

        public DataTypeEnum DataType { get; set; }

        
    }
}
